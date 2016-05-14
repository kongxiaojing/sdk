using System;
using ExorAIO.Utilities;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.UI;

namespace ExorAIO.Champions.Olaf
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
            /// <summary>
            ///     The R Automatic Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                Bools.ShouldCleanse(GameObjects.Player) &&
                Vars.Menu["spells"]["r"]["logical"].GetValue<MenuBool>().Value)
            {
                Vars.R.Cast();
            }
        }
    }
}