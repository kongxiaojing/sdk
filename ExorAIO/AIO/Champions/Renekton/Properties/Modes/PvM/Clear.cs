using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Renekton
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
            ///     The Clear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Vars.Menu["spells"]["q"]["clear"].GetValue<MenuBool>().Value)
            {
                if (Targets.Minions.Any() && Targets.Minions.Count() >= 3)
                {
                    Vars.Q.Cast();
                }
                else if (Targets.JungleMinions.Any())
                {
                    if (!Vars.W.IsReady() && !GameObjects.Player.HasBuff("RenektonPreExecute"))
                    {
                        Vars.Q.Cast();
                    }
                }
            }
        }
    }
}