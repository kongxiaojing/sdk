using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
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
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The Clear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() && GameObjects.Player.ManaPercent > ManaManager.NeededQMana &&
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
                ///     The LaneClear Q Logics.
                /// </summary>
                else
                {
                    /// <summary>
                    ///     The Aggressive LaneClear Q Logic.
                    /// </summary>
                    if (GameObjects.EnemyHeroes.Any(t => !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.Q2.Range)))
                    {
                        if (Vars.Q2.GetLineFarmLocation(Targets.Minions, Vars.Q2.Width).MinionsHit >= 3 &&
                            !new Geometry.Rectangle(
                                GameObjects.Player.ServerPosition,
                                GameObjects.Player.ServerPosition.Extend(
                                    Targets.Minions[0].ServerPosition, Vars.Q2.Range), Vars.Q2.Width).IsOutside(
                                        (Vector2)
                                            Vars.Q2.GetPrediction(
                                                GameObjects.EnemyHeroes.FirstOrDefault(
                                                    t => !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.Q2.Range)))
                                                .CastPosition))
                        {
                            Vars.Q.Cast(Vars.Q.GetLineFarmLocation(Targets.Minions, Vars.Q2.Width).Position);
                        }
                    }

                    /// <summary>
                    ///     The LaneClear Q Logic.
                    /// </summary>
                    else if (
                        !GameObjects.EnemyHeroes.Any(
                            t => !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.Q2.Range + 100f)))
                    {
                        if (Vars.Q2.GetLineFarmLocation(Targets.Minions, Vars.Q2.Width).MinionsHit >= 3)
                        {
                            Vars.Q.CastOnUnit(Targets.Minions[0]);
                        }
                    }
                }
            }

            /// <summary>
            ///     The Clear W Logic.
            /// </summary>
            if (Vars.W.IsReady() && !GameObjects.Player.HasBuff("lucianpassivebuff") &&
                GameObjects.Player.ManaPercent > ManaManager.NeededWMana &&
                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The LaneClear W Logic.
                /// </summary>
                if (Vars.W.GetCircularFarmLocation(Targets.Minions, Vars.W.Width).MinionsHit >= 3)
                {
                    Vars.W.Cast(Vars.W.GetCircularFarmLocation(Targets.Minions, Vars.W.Width).Position);
                }

                /// <summary>
                ///     The JungleClear W Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any())
                {
                    Vars.W.Cast(Targets.JungleMinions[0].Position);
                }
            }

            /// <summary>
            ///     The E JungleClear Logic.
            /// </summary>
            if (Vars.E.IsReady() && Targets.JungleMinions.Any() && !GameObjects.Player.HasBuff("lucianpassivebuff") &&
                GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(Game.CursorPos);
            }
        }
    }
}