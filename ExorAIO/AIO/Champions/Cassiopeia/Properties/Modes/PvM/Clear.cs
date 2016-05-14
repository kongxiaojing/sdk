using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using LeagueSharp.Data.Enumerations;

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
            if (Vars.E.IsReady())
            {
                if (Targets.JungleMinions.Any() &&
                    GameObjects.Player.ManaPercent >
                        ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["clear"]) &&
                    Vars.Menu["spells"]["e"]["clear"].GetValue<MenuSliderButton>().BValue)
                {
                    DelayAction.Add(Vars.Menu["spells"]["e"]["delay"].GetValue<MenuSlider>().Value, () =>
                    {
                        foreach (var minion in Targets.JungleMinions.Where(m => m.HasBuffOfType(BuffType.Poison)))
                        {
                            Vars.E.CastOnUnit(minion);
                        }
                    });
                }
                else if (Targets.Minions.Any())
                {
                    if (GameObjects.Player.ManaPercent <
                            Vars.Menu["spells"]["e"]["lasthit"].GetValue<MenuSliderButton>().SValue &&
                        Vars.Menu["spells"]["e"]["lasthit"].GetValue<MenuSliderButton>().BValue)
                    {
                        DelayAction.Add(Vars.Menu["spells"]["e"]["delay"].GetValue<MenuSlider>().Value, () =>
                        {
                            foreach (var minion in Targets.Minions.Where(
                                m =>
                                    Vars.GetRealHealth(m) <
                                        (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.E) + 
                                            (m.HasBuffOfType(BuffType.Poison)
                                                ? (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.E, DamageStage.Empowered)
                                                : 0)))
                            {
                                Vars.E.CastOnUnit(minion);
                            }
                        });
                    }
                    else if (GameObjects.Player.ManaPercent >=
                            Vars.Menu["spells"]["e"]["clear"].GetValue<MenuSliderButton>().SValue &&
                        Vars.Menu["spells"]["e"]["clear"].GetValue<MenuSliderButton>().BValue)
                    {
                        DelayAction.Add(Vars.Menu["spells"]["e"]["delay"].GetValue<MenuSlider>().Value, () =>
                        {
                            foreach (var minion in Targets.Minions.Where(m => m.HasBuffOfType(BuffType.Poison)))
                            {
                                Vars.E.CastOnUnit(minion);
                            }
                        });
                    }
                }
            }

            /// <summary>
            ///     The Q Clear Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["clear"]) &&
                Vars.Menu["spells"]["q"]["clear"].GetValue<MenuSliderButton>().BValue)
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

            /// <summary>
            ///     The W Clear Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["clear"]) &&
                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuSliderButton>().BValue)
            {
                /// <summary>
                ///     The W LaneClear Logic.
                /// </summary>
                if (Vars.W.GetCircularFarmLocation(Targets.Minions, Vars.W.Width).MinionsHit >= 3)
                {
                    Vars.W.Cast(Vars.W.GetCircularFarmLocation(Targets.Minions.Where(m => !m.HasBuffOfType(BuffType.Poison)).ToList(), Vars.W.Width).Position);
                }

                /// <summary>
                ///     The W JungleClear Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any(m => !m.HasBuffOfType(BuffType.Poison)))
                {
                    Vars.W.Cast(Targets.JungleMinions.Where(m => !m.HasBuffOfType(BuffType.Poison)).FirstOrDefault().ServerPosition);
                }
            }
        }
    }
}