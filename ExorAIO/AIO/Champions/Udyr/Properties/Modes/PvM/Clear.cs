using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Udyr
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
        public static void Clear(EventArgs args)
        {
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The Q on Buildings Logic.
            /// </summary>
            if (Vars.Q.IsReady() && ((Obj_AI_Turret) Variables.Orbwalker.GetTarget()).IsValidTarget() &&
                Vars.Menu["spells"]["q"]["build"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast();
            }

            if (!((Obj_AI_Minion) Variables.Orbwalker.GetTarget()).IsValidTarget())
            {
                return;
            }

            /// <summary>
            ///     The W Clear Logic.
            /// </summary>
            if (Vars.W.IsReady() && GameObjects.Player.HealthPercent <= ManaManager.NeededWMana &&
                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast();
                return;
            }

            /// <summary>
            ///     The E JungleClear Logic.
            /// </summary>
            if (Vars.E.IsReady() && GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuBool>().Value)
            {
                if (Targets.JungleMinions.Any(m => !m.HasBuff("udyrbearstuncheck") && m.IsValidTarget(Vars.R.Range)))
                {
                    Vars.E.Cast();
                }
            }

            /// <summary>
            ///     The JungleClear Logic.
            /// </summary>
            if (Targets.JungleMinions.Any())
            {
                /// <summary>
                ///     If the player has Luden's Echo/Runic Echoes.
                /// </summary>
                if (GameObjects.Player.HasBuff("itemmagicshankcharge") ||
                    GameObjects.Player.Spellbook.GetSpell(SpellSlot.Q).Level == 0)
                {
                    /// <summary>
                    ///     The R JungleClear Logic.
                    /// </summary>
                    if (Vars.R.IsReady() && GameObjects.Player.ManaPercent > ManaManager.NeededRMana &&
                        GameObjects.Player.GetBuffCount("UdyrPhoenixStance") != 3 &&
                        Vars.Menu["spells"]["r"]["clear"].GetValue<MenuBool>().Value)
                    {
                        Vars.R.Cast();
                    }
                }

                /// <summary>
                ///     If the player hasn't Luden's Echo/Runic Echoes.
                /// </summary>
                else
                {
                    /// <summary>
                    ///     The Q JungleClear Logic.
                    /// </summary>
                    if (Vars.Q.IsReady() && GameObjects.Player.ManaPercent > ManaManager.NeededQMana &&
                        Vars.Menu["spells"]["q"]["clear"].GetValue<MenuBool>().Value)
                    {
                        Vars.Q.Cast();
                    }
                }
            }

            /// <summary>
            ///     The LaneClear R Logic.
            /// </summary>
            else if (Targets.Minions.Any())
            {
                if (Vars.R.IsReady() && GameObjects.Player.ManaPercent > ManaManager.NeededRMana &&
                    GameObjects.Player.GetBuffCount("UdyrPhoenixStance") != 3 &&
                    Vars.Menu["spells"]["r"]["clear"].GetValue<MenuBool>().Value)
                {
                    if (Targets.Minions.Count() >= 3)
                    {
                        Vars.R.Cast();
                    }
                }
            }
        }
    }
}