using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Ashe
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
            ///     The KillSteal W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Vars.Menu["spells"]["w"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) &&
                            t.Health < Vars.W.GetDamage(t) &&
                            !t.IsValidTarget(Vars.AARange) &&
                            t.IsValidTarget(Vars.W.Range - 100f)))
                {
                    if (!Vars.W.GetPrediction(Targets.Target).CollisionObjects.Any(c => c is Obj_AI_Minion))
                    {
                        Vars.W.Cast(Vars.W.GetPrediction(target).UnitPosition);
                        return;
                    }
                }
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
                            !t.IsValidTarget(Vars.W.Range) &&
                            t.Health < Vars.R.GetDamage(t)))
                {
                    if (!Vars.R.GetPrediction(target).CollisionObjects.Any(c => c is Obj_AI_Hero))
                    {
                        Vars.R.Cast(Vars.R.GetPrediction(target).UnitPosition);
                    }
                }
            }
        }
    }
}