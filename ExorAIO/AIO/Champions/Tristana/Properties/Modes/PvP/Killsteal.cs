using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

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
            if (Vars.R.IsReady() &&
                Vars.Menu["spells"]["r"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.R.Range) &&
                        t.Health > GameObjects.Player.GetAutoAttackDamage(t)*2))
                {
                    if (target.Health > KillSteal.GetEDamage(target) &&
                        target.Health < (float)GameObjects.Player.GetSpellDamage(target, SpellSlot.R) + KillSteal.GetEDamage(target))
                    {
                        Vars.R.CastOnUnit(target);
                    }
                }
            }
        }
    }
}