using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Quinn
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
            if (Vars.R.Instance.Name.Equals("QuinnRFinale"))
            {
                return;
            }

            /// <summary>
            ///     The KillSteal Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Vars.Menu["spells"]["q"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) && t.Health < Vars.Q.GetDamage(t) &&
                            !t.IsValidTarget(Vars.AARange) && t.IsValidTarget(Vars.Q.Range - 100f)))
                {
                    if (!Vars.Q.GetPrediction(Targets.Target).CollisionObjects.Any())
                    {
                        Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).CastPosition);
                    }
                }
            }

            /// <summary>
            ///     The KillSteal E Logic.
            /// </summary>
            if (Vars.E.IsReady() && Vars.Menu["spells"]["e"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.E.Range) && !t.IsValidTarget(Vars.AARange) &&
                            t.Health < Vars.E.GetDamage(t) + GameObjects.Player.GetAutoAttackDamage(t) * 2))
                {
                    Vars.E.CastOnUnit(target);
                }
            }
        }
    }
}