using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Caitlyn
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
        public static void Killsteal(EventArgs args)
        {
            if (GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(Vars.AARange)))
            {
                return;
            }

            /// <summary>
            ///     The KillSteal Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["spells"]["q"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) &&
                            t.Health < Vars.Q.GetDamage(t) &&
                            GameObjects.Player.Distance(Vars.Q.GetPrediction(t).UnitPosition) < Vars.Q.Range - 50))
                {
                    Vars.Q.Cast(Vars.Q.GetPrediction(target).UnitPosition);
                    return;
                }
            }

            if (GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(Vars.Q.Range)))
            {
                return;
            }

            /// <summary>
            ///     The KillSteal R Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                Vars.Menu["spells"]["r"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) &&
                            t.IsValidTarget(Vars.R.Range) &&
                            t.Health < Vars.R.GetDamage(t)))
                {
                    Vars.R.CastOnUnit(target);
                }
            }
        }
    }
}