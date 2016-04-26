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
        public static void Killsteal(EventArgs args)
        {
            /// <summary>
            ///     The KillSteal R Logic.
            /// </summary>
            if (Vars.R.IsReady() && Vars.Menu["spells"]["r"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.R.Range) &&
                            t.Health > GameObjects.Player.GetAutoAttackDamage(t) * 2))
                {
                    if (target.HasBuff("TristanaECharge"))
                    {
                        if (target.Health > KillSteal.GetEDamage(target) &&
                            target.Health < Vars.R.GetDamage(target) + KillSteal.GetEDamage(target))
                        {
                            Vars.R.CastOnUnit(target);
                        }
                    }
                    else
                    {
                        if (target.Health < Vars.R.GetDamage(target))
                        {
                            Vars.R.CastOnUnit(target);
                        }
                    }
                }
            }
        }
    }
}