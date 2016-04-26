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
                !Targets.JungleMinions.Any(x => x.IsValidTarget(Vars.E.Range)) &&
                Vars.Menu["spells"]["q"]["junglegrab"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast(Targets.JungleMinions[0].ServerPosition);
            }

            /// <summary>
            ///     The E LaneClear Logic,
            ///     The E JungleClear Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                (Targets.Minions.Count() >= 3 || Targets.JungleMinions.Any()) &&
                Vars.Menu["spells"]["e"]["clear"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast();
            }
        }
    }
}