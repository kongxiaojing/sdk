using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Tristana
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
        public static void Clear(EventArgs args)
        {
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The Buildings E Logics.
            /// </summary>
            if (Vars.E.IsReady() && GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                Vars.Menu["spells"]["e"]["build"].GetValue<MenuBool>().Value)
            {
                foreach (var turret in GameObjects.EnemyTurrets.Where(t => t.IsValidTarget(Vars.E.Range)))
                {
                    Vars.E.CastOnUnit(turret);
                }
            }

            /// <summary>
            ///     The Clear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() && (Targets.Minions.Any() || Targets.JungleMinions.Any()) &&
                !GameObjects.EnemyMinions.Any(t => t.IsValidTarget(Vars.W.Range)) &&
                Vars.Menu["spells"]["q"]["clear"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast();
            }

            /// <summary>
            ///     The Clear E Logics.
            /// </summary>
            if (Vars.E.IsReady() && GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                Vars.Menu["spells"]["e"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The JungleClear E Logic.
                /// </summary>
                if (Targets.JungleMinions.Any())
                {
                    Vars.E.CastOnUnit(Targets.JungleMinions[0]);
                }

                /// <summary>
                ///     The LaneClear E Logics.
                /// </summary>
                else
                {
                    /// <summary>
                    ///     The Aggressive LaneClear E Logic.
                    /// </summary>
                    if (GameObjects.EnemyHeroes.Any(t => !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.W.Range)))
                    {
                        foreach (var minion in Targets.Minions.Where(m => m.CountEnemyHeroesInRange(200f) > 0))
                        {
                            Vars.E.CastOnUnit(minion);
                        }
                    }
                    else
                    {
                        /// <summary>
                        ///     The Normal LaneClear E Logic.
                        /// </summary>
                        if (Targets.Minions.Any())
                        {
                            if (Targets.Minions.Count() >= 3)
                            {
                                Vars.E.CastOnUnit(Targets.Minions[0]);
                            }
                        }
                    }
                }
            }
        }
    }
}