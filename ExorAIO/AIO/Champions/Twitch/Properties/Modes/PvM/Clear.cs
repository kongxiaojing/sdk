using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.Data.Enumerations;

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
            if (Vars.W.IsReady() &&
                !GameObjects.Player.HasBuff("TwitchFullAutomatic") &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["clear"]) &&
                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuSliderButton>().BValue)
            {
                /// <summary>
                ///     The W LaneClear Logic.
                /// </summary>
                if (Vars.W.GetCircularFarmLocation(Targets.Minions.Where(m => m.GetBuffCount("twitchdeadlyvenom") <= 4).ToList(), Vars.W.Width).MinionsHit >= 3)
                {
                    Vars.W.Cast(Vars.W.GetCircularFarmLocation(Targets.Minions.Where(m => m.GetBuffCount("twitchdeadlyvenom") <= 4).ToList(), Vars.W.Width).Position);
                }

                /// <summary>
                ///     The W JungleClear Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any(m => m.GetBuffCount("twitchdeadlyvenom") <= 4))
                {
                    Vars.W.Cast(Targets.JungleMinions.FirstOrDefault(m => m.GetBuffCount("twitchdeadlyvenom") <= 4).ServerPosition);
                }
            }

            /// <summary>
            ///     The LaneClear E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.Minions.Any() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["laneclear"]) &&
                Vars.Menu["spells"]["e"]["laneclear"].GetValue<MenuSliderButton>().BValue)
            {
                if (Targets.Minions.Count(
                    m =>
                        m.IsValidTarget(Vars.E.Range) &&
                        m.Health <
                            (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.E) +
                            (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.E, DamageStage.Buff)) >= 3)
                {
                    Vars.E.Cast();
                }
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void JungleClear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (Variables.Orbwalker.GetTarget() as Obj_AI_Minion == null ||
                !Targets.JungleMinions.Contains(Variables.Orbwalker.GetTarget() as Obj_AI_Minion))
            {
                return;
            }

            /// <summary>
            ///     The Q JungleClear Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["jungleclear"]) &&
                Vars.Menu["spells"]["q"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.Q.Cast();
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void BuildingClear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(Variables.Orbwalker.GetTarget() is Obj_HQ) &&
                !(Variables.Orbwalker.GetTarget() is Obj_AI_Turret) &&
                !(Variables.Orbwalker.GetTarget() is Obj_BarracksDampener))
            {
                return;
            }

            /// <summary>
            ///     The Q BuildingClear Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["buildings"]) &&
                Vars.Menu["spells"]["q"]["buildings"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.Q.Cast();
            }
        }
    }
}