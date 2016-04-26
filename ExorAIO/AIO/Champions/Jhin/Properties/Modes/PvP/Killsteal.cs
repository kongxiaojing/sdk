using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Jhin
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
            if (Vars.Q.IsReady() && Vars.Menu["spells"]["q"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.Q.Range) && !t.IsValidTarget(Vars.AARange) &&
                            t.Health < Vars.Q.GetDamage(t) && !Vars.R.Instance.Name.Equals("JhinRShot")))
                {
                    Vars.Q.CastOnUnit(target);
                    return;
                }
            }

            /// <summary>
            ///     The KillSteal W Logic.
            /// </summary>
            if (Vars.W.IsReady() && Vars.Menu["spells"]["w"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.W.Range) && !t.IsValidTarget(Vars.AARange) &&
                            t.Health < Vars.W.GetDamage(t) && !Vars.R.Instance.Name.Equals("JhinRShot")))
                {
                    Vars.W.Cast(Vars.W.GetPrediction(target).CastPosition);
                    return;
                }
            }

            /// <summary>
            ///     The KillSteal R Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                Vars.R.Instance.Name.Equals("JhinRShot", StringComparison.InvariantCultureIgnoreCase) &&
                Vars.Menu["spells"]["r"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.R.Range) && !t.IsValidTarget(Vars.AARange) &&
                            t.Health < Vars.R.GetDamage(t) * 2))
                {
                    Vars.R.Cast(Vars.R.GetPrediction(target).CastPosition);
                }
            }
        }
    }
}