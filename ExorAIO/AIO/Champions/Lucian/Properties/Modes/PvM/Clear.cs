using System;
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
        ///     Fired when the game is updated.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Clear(EventArgs args)
        {
            /// <summary>
            ///     The Extended Q LaneClear Harass Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent > 
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["extended"]["exlaneclear"]) &&
                Vars.Menu["spells"]["q"]["extended"]["exlaneclear"].GetValue<MenuSliderButton>().BValue)
            {
                if (!GameObjects.EnemyHeroes.Any(
                    t =>
                        !Invulnerable.Check(t) &&
                        !t.IsValidTarget(Vars.Q.Range) &&
                        t.IsValidTarget(Vars.Q2.Range-50f) &&
                        Vars.Menu["spells"]["q"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value))
                {
                    return;
                }

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

            if (GameObjects.Player.HasBuff("LucianPassiveBuff"))
            {
                return;
            }

            /// <summary>
            ///     The Clear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady())
            {
                /// <summary>
                ///     The JungleClear Q Logic.
                /// </summary>
                if (Targets.JungleMinions.Any() &&
                    GameObjects.Player.ManaPercent >
                        ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["jungleclear"]) &&
                    Vars.Menu["spells"]["q"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
                {
                    Vars.Q.CastOnUnit(Targets.JungleMinions[0]);
                }

                /// <summary>
                ///     The LaneClear Q Logic.
                /// </summary>
                else if (GameObjects.Player.ManaPercent >
                        ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["laneclear"]) &&
                    Vars.Menu["spells"]["q"]["laneclear"].GetValue<MenuSliderButton>().BValue)
                {
                    if (!GameObjects.EnemyHeroes.Any(
                        t =>
                            !Invulnerable.Check(t) &&
                            t.IsValidTarget(Vars.Q2.Range + 100f)))
                    {
                        Vars.Q.CastOnUnit(Targets.Minions[0]);
                    }
                }
                return;
            }

            /// <summary>
            ///     The Clear W Logic.
            /// </summary>
            if (Vars.W.IsReady())
            {
                /// <summary>
                ///     The JungleClear W Logic.
                /// </summary>
                if (Targets.JungleMinions.Any() &&
                    GameObjects.Player.ManaPercent >
                        ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["jungleclear"]) &&
                    Vars.Menu["spells"]["w"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
                {
                    Vars.W.Cast(Targets.JungleMinions[0].ServerPosition);
                }

                /// <summary>
                ///     The LaneClear W Logic.
                /// </summary>
                else if (Targets.Minions.Any() &&
                    GameObjects.Player.ManaPercent >
                        ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["laneclear"]) &&
                    Vars.Menu["spells"]["w"]["laneclear"].GetValue<MenuSliderButton>().BValue)
                {
                    Vars.W.Cast(Targets.Minions[0].ServerPosition);
                    return;
                }
            }

            /// <summary>
            ///     The E LaneClear Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.Minions.Any(
                m =>
                    m.Distance(GameObjects.Player.ServerPosition.Extend(Game.CursorPos, Vars.E.Range)) < Vars.AARange) &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["laneclear"]) &&
                Vars.Menu["spells"]["e"]["laneclear"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.E.Cast(Game.CursorPos);
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
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["jungleclear"]) &&
                Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
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
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["buildings"]) &&
                Vars.Menu["spells"]["e"]["buildings"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.E.Cast(GameObjects.Player.ServerPosition.Extend(Game.CursorPos, 5));
                return;
            }

            /// <summary>
            ///     The W BuildingClear Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["buildings"]) &&
                Vars.Menu["spells"]["w"]["buildings"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.W.Cast(Game.CursorPos);
            }
        }
    }
}