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
            ///     The LaneClear W Logic.
            /// </summary>
            if (Vars.W.IsReady() && !GameObjects.Player.HasBuff("TwitchFullAutomatic") &&
                GameObjects.Player.ManaPercent > ManaManager.NeededWMana &&
                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The W LaneClear Logic.
                /// </summary>
                if (Vars.W.GetCircularFarmLocation(Targets.Minions, Vars.W.Width).MinionsHit >= 3)
                {
                    Vars.W.Cast(Vars.W.GetCircularFarmLocation(Targets.Minions, Vars.W.Width).Position);
                }

                /// <summary>
                ///     The W JungleClear Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any())
                {
                    Vars.W.Cast(Targets.JungleMinions[0].Position);
                }
            }

            /// <summary>
            ///     The LaneClear E Logic.
            /// </summary>
            if (Vars.E.IsReady() && Targets.Minions.Any() && GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                Vars.Menu["spells"]["e"]["laneclear"].GetValue<MenuBool>().Value)
            {
                if (Targets.Minions.Count(m => m.IsValidTarget(Vars.E.Range) && m.Health < Vars.E.GetDamage(m)) >= 3)
                {
                    Vars.E.Cast();
                }
            }
        }
    }
}