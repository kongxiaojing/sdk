using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Draven
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
            ///     The Clear E Logics.
            /// </summary>
            if (Vars.E.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                Vars.Menu["spells"]["e"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The LaneClear E Logic.
                /// </summary>
                if (Vars.E.GetLineFarmLocation(Targets.Minions, Vars.E.Width).MinionsHit >= 5)
                {
                    Vars.E.Cast(Vars.E.GetLineFarmLocation(Targets.Minions, Vars.E.Width).Position);
                }

                /// <summary>
                ///     The JungleClear E Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any())
                {
                    Vars.E.Cast(Targets.JungleMinions[0].ServerPosition);
                }
            }
        }
    }
}