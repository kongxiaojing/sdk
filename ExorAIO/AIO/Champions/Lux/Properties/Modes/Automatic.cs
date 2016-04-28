using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using LeagueSharp.SDK.Enumerations;

namespace ExorAIO.Champions.Lux
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Automatic(EventArgs args)
        {
            if (GameObjects.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Focus Logic (Passive Mark).
            /// </summary>
            foreach (var target in GameObjects.EnemyHeroes.Where(
                t =>
                    t.IsValidTarget(Vars.AARange) &&
                    t.HasBuff("luxilluminatingfraulein")))
            {
                Variables.Orbwalker.ForceTarget = target;
            }

            /// <summary>
            ///     The E Missile Manager.
            /// </summary>
            if (Vars.E.IsReady() &&
                GameObjects.Player.Spellbook.GetSpell(SpellSlot.E).ToggleState != 1)
            {
                switch (Variables.Orbwalker.ActiveMode)
                {
                    /// <summary>
                    ///     The E Combo Logic.
                    /// </summary>
                    case OrbwalkingMode.Combo:

                        if (!Vars.R.IsReady() &&
                            Lux.EMissile != null)
                        {
                            foreach (var target in GameObjects.EnemyHeroes.Where(
                                t =>
                                    !t.HasBuff("luxilluminatingfraulein") &&
                                    t.Distance(Lux.EMissile.Position) < Vars.W.Width-10f))
                            {
                                Vars.E.Cast();
                                break;
                            }
                        }
                        break;

                    /// <summary>
                    ///     The E Clear Logic.
                    /// </summary>
                    case OrbwalkingMode.LaneClear:
                        
                        if (Lux.EMissile != null)
                        {
                            if (Targets.EMinions.Any() &&
                                Targets.EMinions.Count() >= 3)
                            {
                                Vars.E.Cast();
                            }
                        }
                        break;

                    default:
                        break;
                }
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var ally in GameObjects.AllyHeroes.Where(
                    a =>
                        a.IsValidTarget(Vars.W.Range, false) &&
                        a.CountEnemyHeroesInRange(1000f) > 0 &&
                        Health.GetPrediction(a, (int) (1000f + Game.Ping/2f)) <= a.MaxHealth/2))
                {
                    if (Vars.Menu["spells"]["w"]["whitelist"][ally.ChampionName.ToLower()].GetValue<MenuBool>().Value)
                    {
                        Vars.W.Cast(Vars.W.GetPrediction(ally).UnitPosition);
                    }
                    else if (Vars.W.GetPrediction(ally).CollisionObjects.Count(c => c is Obj_AI_Hero) > 2)
                    {
                        Vars.W.Cast(Vars.W.GetPrediction(ally).UnitPosition);
                    }
                }
            }
        }
    }
}