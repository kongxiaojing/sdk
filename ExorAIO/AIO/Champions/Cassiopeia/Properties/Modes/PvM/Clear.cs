using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Cassiopeia
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
            ///     The E Clear Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                Vars.Menu["spells"]["e"]["clear"].GetValue<MenuBool>().Value)
            {
                if (Targets.JungleMinions.Any())
                {
                    foreach (var minion in Targets.JungleMinions.Where(m => m.HasBuffOfType(BuffType.Poison)))
                    {
                        DelayAction.Add(
                            Vars.Menu["spells"]["e"]["delay"].GetValue<MenuSlider>().Value, () =>
                        {
                            Vars.E.CastOnUnit(minion);
                        });
                    }
                }
                else if (Targets.Minions.Any())
                {
                    foreach (var minion in Targets.Minions.Where(m => m.HasBuffOfType(BuffType.Poison)))
                    {
                        DelayAction.Add(
                            Vars.Menu["spells"]["e"]["delay"].GetValue<MenuSlider>().Value, () =>
                        {
                            Vars.E.CastOnUnit(minion);
                        });
                    }
                }
            }

            /// <summary>
            ///     The W Clear Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
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
                    Vars.W.Cast(Targets.JungleMinions[0].ServerPosition);
                }
            }

            /// <summary>
            ///     The Q Clear Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededQMana &&
                Vars.Menu["spells"]["q"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The Q LaneClear Logic.
                /// </summary>
                if (Vars.Q.GetCircularFarmLocation(Targets.Minions, Vars.Q.Width).MinionsHit >= 3)
                {
                    Vars.Q.Cast(Vars.Q.GetCircularFarmLocation(Targets.Minions, Vars.Q.Width).Position);
                }

                /// <summary>
                ///     The Q JungleClear Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any())
                {
                    Vars.Q.Cast(Targets.JungleMinions[0].ServerPosition);
                }
            }
        }
    }
}