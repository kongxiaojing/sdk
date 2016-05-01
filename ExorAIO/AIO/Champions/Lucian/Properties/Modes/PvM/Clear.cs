using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using Geometry = ExorAIO.Utilities.Geometry;

namespace ExorAIO.Champions.Lucian
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void Clear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            ///     The Q Harass Logic.
            /// </summary>
            if (Vars.Q.IsReady())
            {
                if (Vars.Menu["spells"]["q"]["extended"]["harass"].GetValue<MenuSliderButton>().BValue &&
                    Vars.Menu["spells"]["q"]["extended"]["harass"].GetValue<MenuSliderButton>().SValue <
                        GameObjects.Player.ManaPercent)
                {
                    /// <summary>
                    ///     Through enemy minions.
                    /// </summary>
                    foreach (var minion 
                        in from minion
                        in Targets.Minions.Where(m => m.IsValidTarget(Vars.Q.Range))

                        let polygon = new Geometry.Rectangle(
                            GameObjects.Player.ServerPosition,
                            GameObjects.Player.ServerPosition.Extend(minion.ServerPosition, Vars.Q2.Range-50f),
                            Vars.Q2.Width)

                        where !polygon.IsOutside(
                            (Vector2)Vars.Q2.GetPrediction(GameObjects.EnemyHeroes.FirstOrDefault(
                            t =>
                                !Invulnerable.Check(t) &&
                                !t.IsValidTarget(Vars.Q.Range) &&
                                t.IsValidTarget(Vars.Q2.Range-50f) &&
                                Vars.Menu["spells"]["q"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value)).UnitPosition)

                        select minion)
                    {
                        Vars.Q.CastOnUnit(minion);
                    }
                }

                /// <summary>
                ///     The Clear Q Logic.
                /// </summary>
                else if (GameObjects.Player.ManaPercent > ManaManager.NeededQMana &&
                    Vars.Menu["spells"]["q"]["clear"].GetValue<MenuBool>().Value)
                {
                    /// <summary>
                    ///     The JungleClear Q Logic.
                    /// </summary>
                    if (Targets.JungleMinions.Any())
                    {
                        Vars.Q.CastOnUnit(Targets.JungleMinions[0]);
                    }

                    /// <summary>
                    ///     The LaneClear Q Logic.
                    /// </summary>
                    else if (!GameObjects.EnemyHeroes.Any(
                        t =>
                            !Invulnerable.Check(t) &&
                            t.IsValidTarget(Vars.Q2.Range + 100f)))
                    {
                        if (Vars.Q2.GetLineFarmLocation(Targets.Minions, Vars.Q2.Width).MinionsHit >= 3)
                        {
                            Vars.Q.CastOnUnit(Targets.Minions[0]);
                        }
                    }
                    return;
                }
            }

            /// <summary>
            ///     The Clear W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededWMana &&
                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The LaneClear W Logic.
                /// </summary>
                if (Vars.Q2.GetLineFarmLocation(Targets.Minions, Vars.Q2.Width).MinionsHit >= 2)
                {
                    Vars.W.Cast(Vars.Q2.GetLineFarmLocation(Targets.Minions, Vars.Q2.Width).Position);
                }

                /// <summary>
                ///     The JungleClear W Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any())
                {
                    Vars.W.Cast(Targets.JungleMinions[0].ServerPosition);
                }
                return;
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void JungleClear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (Variables.Orbwalker.GetTarget() as Obj_AI_Minion == null)
            {
                return;
            }

            /// <summary>
            ///     The E JungleClear Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.JungleMinions.Any() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(Game.CursorPos);
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void BuildingClear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (Variables.Orbwalker.GetTarget() as Obj_HQ == null &&
                Variables.Orbwalker.GetTarget() as Obj_AI_Turret  == null &&
                Variables.Orbwalker.GetTarget() as Obj_BarracksDampener == null)
            {
                return;
            }

            /// <summary>
            ///     The E BuildingClear Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                Vars.Menu["spells"]["e"]["buildings"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(GameObjects.Player.ServerPosition.Extend(Game.CursorPos, 5));
                return;
            }

            /// <summary>
            ///     The W BuildingClear Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededWMana &&
                Vars.Menu["spells"]["w"]["buildings"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast(Game.CursorPos);
            }
        }
    }
}