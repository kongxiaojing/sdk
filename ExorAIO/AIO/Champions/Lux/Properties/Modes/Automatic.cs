using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
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
            /// <summary>
            ///     The Focus Logic (Passive Mark).
            /// </summary>
            foreach (var target in
                GameObjects.EnemyHeroes.Where(
                    t => t.HasBuff("luxilluminatingfraulein") && t.IsValidTarget(Vars.AARange)))
            {
                Variables.Orbwalker.ForceTarget = target;
            }

            if (GameObjects.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The E Missile Manager.
            /// </summary>
            if (Vars.E.IsReady() && !Vars.Q.IsReady() &&
                GameObjects.Player.Spellbook.GetSpell(SpellSlot.E).ToggleState != 1)
            {
                switch (Variables.Orbwalker.ActiveMode)
                {
                    /// <summary>
                    ///     The E Combo Logic.
                    /// </summary>
                    case OrbwalkingMode.Combo:

                        if (!Vars.R.IsReady() && !Targets.Target.HasBuffOfType(BuffType.Snare) &&
                            Lux.EMissile?.Position.CountEnemyHeroesInRange(Vars.E.Width) > 0)
                        {
                            Vars.E.Cast();
                        }
                        break;

                    /// <summary>
                    ///     The E Clear Logic.
                    /// </summary>
                    case OrbwalkingMode.LaneClear:

                        if (Targets.EMinions.Any() && Targets.EMinions.Count() >= 3)
                        {
                            Vars.E.Cast();
                        }
                        break;

                    default:
                        break;
                }
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            /*if (Vars.W.IsReady() &&
                Vars.Menu["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var ally in GameObjects.AllyHeroes.Where(
                    a =>
                        Extensions.IsValidTarget(a, Vars.W.Range) &&
                        HealthPrediction.(a, (int) (1000f + Game.Ping/2f)) <= a.MaxHealth/2))
                {
                    if (
                        Vars.Menu["whitelist.{ally.ChampionName.ToLower()}"]
                            .GetValue<MenuBool>().Value)
                    {
                        Vars.W.Cast(ally.Position);
                    }
                }
            }*/
        }
    }
}