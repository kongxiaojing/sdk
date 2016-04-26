using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Pantheon
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Clear(EventArgs args)
        {
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The Q JungleClear Logics.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.JungleMinions.Any() && !GameObjects.Player.HasBuff("pantheonpassiveshield") &&
                GameObjects.Player.ManaPercent > ManaManager.NeededQMana &&
                Vars.Menu["spells"]["q"]["jungleclear"].GetValue<MenuBool>().Value)
            {
                Vars.Q.CastOnUnit(Targets.JungleMinions[0]);
            }

            /// <summary>
            ///     The E Clear Logics.
            /// </summary>
            if (Vars.E.IsReady() && GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                Vars.Menu["spells"]["e"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The LaneClear E Logic.
                /// </summary>
                if (Targets.Minions.Any())
                {
                    Vars.E.Cast(Targets.Minions[0].Position);
                }

                /// <summary>
                ///     The JungleClear E Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any() && !GameObjects.Player.HasBuff("pantheonpassiveshield"))
                {
                    Vars.E.Cast(Targets.JungleMinions[0].Position);
                }
            }
        }
    }
}