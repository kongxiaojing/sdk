using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Veigar
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
            ///     The Q Clear Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Minions.Any() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["clear"]) &&
                Vars.Menu["spells"]["q"]["clear"].GetValue<MenuSliderButton>().BValue)
            {
                if (Vars.Q.GetLineFarmLocation(Targets.Minions.Where(
                    m =>
                        Vars.GetRealHealth(m) <
                            (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)).ToList(), Vars.Q.Width).MinionsHit == 2)
                {
                    Vars.Q.Cast(Vars.Q.GetLineFarmLocation(Targets.Minions.Where(
                        m =>
                            Vars.GetRealHealth(m) <
                                (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)).ToList(), Vars.Q.Width).Position);
                }
            }

            /// <summary>
            ///     The W JungleClear Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Targets.JungleMinions.Any() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["clear"]) &&
                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuSliderButton>().BValue)
            {
                if (!Targets.JungleMinions.Any(m => m.Health < (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.W)))
                {
                    Vars.W.Cast(Targets.JungleMinions[0].ServerPosition);
                }
            }
        }
    }
}