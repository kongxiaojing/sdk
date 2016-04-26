using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.DrMundo
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
                    if (!Vars.Q.GetPrediction(target).CollisionObjects.Any(
                        c =>
                            c is Obj_AI_Hero ||
                            c is Obj_AI_Minion))
                    {
                        Vars.Q.Cast(Vars.Q.GetPrediction(target).UnitPosition);
                    }
                }
            }
        }
    }
}