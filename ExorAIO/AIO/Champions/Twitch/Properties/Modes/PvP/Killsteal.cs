using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using LeagueSharp.Data.Enumerations;

namespace ExorAIO.Champions.Twitch
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
            ///     The KillSteal E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Menu["spells"]["e"]["killsteal"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.EnemyHeroes.Any(
                    t =>
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.E.Range) &&
                        Vars.GetRealHealth(t) <
                            (float)GameObjects.Player.GetSpellDamage(t, SpellSlot.E) +
                            (float)GameObjects.Player.GetSpellDamage(t, SpellSlot.E, DamageStage.Buff)))
                {
                    Vars.E.Cast();
                }
            }
        }
    }
}