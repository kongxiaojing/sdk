using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Kalista
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
                        !Invulnerable.Check(t) &&
                        !Bools.IsPerfectRendTarget(t) &&
                        t.IsValidTarget(Vars.Q.Range) &&
                        !t.IsValidTarget(Vars.AARange) &&
                        t.Health < Vars.Q.GetDamage(t)))
                {
                    if (!Vars.Q.GetPrediction(target).CollisionObjects.Any())
                    {
                        Vars.Q.Cast(Vars.Q.GetPrediction(target).UnitPosition);
                    }
                    else if (Vars.Q.GetPrediction(target).CollisionObjects.Count(
                        c =>
                            c is Obj_AI_Minion &&
                            c.Health < KillSteal.GetPerfectRendDamage(c)) == Vars.Q.GetPrediction(target).CollisionObjects.Count())
                    {
                        Vars.Q.Cast(Vars.Q.GetPrediction(target).UnitPosition);
                    }
                }
            }

            /// <summary>
            ///     The KillSteal E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Menu["spells"]["e"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsPerfectRendTarget(t) &&
                        t.Health < KillSteal.GetPerfectRendDamage(t)))
                {
                    Vars.E.Cast();
                }
            }
        }
    }
}