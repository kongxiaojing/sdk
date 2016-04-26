using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

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
                    foreach (var target in
                        GameObjects.EnemyHeroes.Where(
                            t => t.IsValidTarget(Vars.E.Range) && t.GetBuffCount("twitchdeadlyvenom") == 6))
                    {
                        Vars.E.Cast();
                    }
                }

                /// <summary>
                ///     The Automatic JungleSteal E Logic.
                /// </summary>
                if (Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuBool>().Value)
                {
                    foreach (var minion in
                        Targets.JungleMinions.Where(
                            m =>
                                m.IsValidTarget(Vars.E.Range) && m.Health < Vars.E.GetDamage(m) &&
                                !m.CharData.BaseSkinName.Contains("Mini")))
                    {
                        Vars.E.Cast();
                    }
                }
            }
        }
    }
}