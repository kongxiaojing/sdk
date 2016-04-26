using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Ezreal
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
            /// <summary>
            ///     The KillSteal Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["spells"]["q"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        !Bools.HasAnyImmunity(t) &&
                        t.IsValidTarget(Vars.Q.Range) &&
                        !t.IsValidTarget(Vars.AARange) &&
                        t.Health < Vars.Q.GetDamage(t)))
                {
                    if (!Vars.Q.GetPrediction(target).CollisionObjects.Any(c => c is Obj_AI_Minion))
                    {
                        Vars.Q.Cast(Vars.Q.GetPrediction(target).UnitPosition);
                        return;
                    }
                }
            }

            /// <summary>
            ///     The KillSteal W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Vars.Menu["spells"]["w"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        t.IsValidTarget(Vars.W.Range) &&
                        !Bools.HasAnyImmunity(t, true) &&
                        !t.IsValidTarget(Vars.AARange) &&
                        t.Health < Vars.W.GetDamage(t)))
                {
                    Vars.W.Cast(Vars.W.GetPrediction(target).UnitPosition);
                    return;
                }
            }

            /// <summary>
            ///     The KillSteal R Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                Vars.Menu["spells"]["r"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        t.IsValidTarget(Vars.R.Range) &&
                        !Bools.HasAnyImmunity(t, true) &&
                        !t.IsValidTarget(Vars.Q.Range) &&
                        t.Health < Vars.R.GetDamage(t)))
                {
                    Vars.R.Cast(Vars.R.GetPrediction(target).UnitPosition);
                }
            }
        }
    }
}