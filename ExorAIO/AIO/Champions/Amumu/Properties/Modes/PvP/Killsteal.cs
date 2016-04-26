using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Amumu
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
            ///     The E KillSteal Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Menu["spells"]["e"]["killsteal"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.EnemyHeroes.Any(
                        t =>
                            !Bools.HasAnyImmunity(t) &&
                            t.IsValidTarget(Vars.E.Range) &&
                            t.Health < Vars.E.GetDamage(t)))
                {
                    Vars.E.Cast();
                    return;
                }
            }

            /// <summary>
            ///     The Q KillSteal Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["spells"]["q"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) &&
                            t.IsValidTarget(Vars.Q.Range) &&
                            t.Health < Vars.Q.GetDamage(t) &&
                            !Vars.Q.GetPrediction(t).CollisionObjects.Any(
                                c =>
                                    c is Obj_AI_Hero ||
                                    c is Obj_AI_Minion)))
                {
                    Vars.Q.Cast(Vars.Q.GetPrediction(target).UnitPosition);
                    return;
                }
            }

            /// <summary>
            ///     The R KillSteal Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                Vars.Menu["spells"]["r"]["killsteal"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.EnemyHeroes.Any(
                        t =>
                            !Bools.HasAnyImmunity(t) &&
                            t.IsValidTarget(Vars.R.Range) &&
                            t.Health < Vars.R.GetDamage(t)))
                {
                    Vars.R.Cast();
                }
            }
        }
    }
}