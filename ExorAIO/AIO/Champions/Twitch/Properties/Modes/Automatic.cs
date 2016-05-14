using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.UI;
using LeagueSharp.SDKEx.Utils;
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
        public static void Automatic(EventArgs args)
        {
            if (GameObjects.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic E Logics.
            /// </summary>
            if (Vars.E.IsReady())
            {
                /// <summary>
                ///     The Automatic Enemy E Logic.
                /// </summary>
                if (Vars.Menu["spells"]["e"]["logical"].GetValue<MenuBool>().Value)
                {
                    foreach (var target in GameObjects.EnemyHeroes.Where(
                        t =>
                            !Invulnerable.Check(t) &&
                            t.IsValidTarget(Vars.E.Range) &&
                            t.GetBuffCount("twitchdeadlyvenom") == 6))
                    {
                        Vars.E.Cast();
                    }
                }

                /// <summary>
                ///     The Automatic JungleSteal E Logic.
                /// </summary>
                if (Vars.Menu["spells"]["e"]["junglesteal"].GetValue<MenuBool>().Value)
                {
                    foreach (var minion in Targets.JungleMinions.Where(
                        m =>
                            m.IsValidTarget(Vars.E.Range) &&
                            Vars.GetRealHealth(m) <
                                (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.E) +
                                (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.E, DamageStage.Buff)))
                    {
                        Vars.E.Cast();
                    }
                }
            }
        }
    }
}