using System;
using System.Linq;
using ExorAIO.Utilities;
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
            ///     The Q JungleGrab Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededQMana &&
                Targets.JungleMinions.Any(m => !m.IsValidTarget(Vars.E.Range)) &&
                Vars.Menu["spells"]["q"]["junglegrab"].GetValue<MenuBool>().Value)
            {
                Vars.Q.CastOnUnit(Targets.JungleMinions.FirstOrDefault(m => !m.IsValidTarget(Vars.E.Range)));
            }

            /// <summary>
            ///     The E Clear Logics.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Menu["spells"]["e"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The E LaneClear Logic.
                /// </summary>
                if (Targets.Minions.Count() >= 3)
                {
                    Vars.E.Cast();
                }

                /// <summary>
                ///     The E JungleClear Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any())
                {
                    Vars.E.Cast();
                }
            }
        }
    }
}