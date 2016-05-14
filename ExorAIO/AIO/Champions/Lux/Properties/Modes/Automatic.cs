using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.UI;
using LeagueSharp.SDKEx.Utils;
using LeagueSharp.SDKEx.Enumerations;

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
            ///     The Automatic Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.Q.Range)))
                {
                    if (Vars.Q.GetPrediction(target).CollisionObjects.Count(c => Targets.Minions.Contains(c)) <= 1)
                    {
                        Vars.Q.Cast(target.ServerPosition);
                    }
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
                        a.CountEnemyHeroesInRange(1000f) > 0 &&
                        a.IsValidTarget(Vars.W.Range, false) &&
                        Health.GetPrediction(a, (int) (1000f + Game.Ping/2f)) <= a.MaxHealth/2))
                {
                    if (Vars.Menu["spells"]["w"]["whitelist"][ally.ChampionName.ToLower()].GetValue<MenuBool>().Value)
                    {
                        Vars.W.Cast(Vars.W.GetPrediction(ally).UnitPosition);
                    }
                    else if (Vars.W.GetPrediction(ally).CollisionObjects.Count(c => GameObjects.AllyHeroes.Contains(c)) >= 2)
                    {
                        Vars.W.Cast(Vars.W.GetPrediction(ally).UnitPosition);
                    }
                }
            }
        }
    }
}