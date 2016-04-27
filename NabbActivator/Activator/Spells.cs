using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace NabbActivator
{
    /// <summary>
    ///     The activator class.
    /// </summary>
    internal partial class Activator
    {
        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Spells(EventArgs args)
        {
            if (!Vars.Menu["activator"]["spells"].GetValue<MenuBool>().Value)
            {
                return;
            }

            /// <summary>
            ///     The Remove Scurvy Logic.
            /// </summary>
            if (GameObjects.Player.ChampionName.Equals("Gangplank"))
            {
                if (Vars.W.IsReady() &&
                    Bools.ShouldCleanse(GameObjects.Player))
                {
                    DelayAction.Add(Vars.Delay, () =>
                    {
                        Vars.W.Cast();
                    });
                }
            }

            /// <summary>
            ///     The Cleanse Logic.
            /// </summary>
            if (SpellSlots.Cleanse.IsReady())
            {
                if (Bools.ShouldCleanse(GameObjects.Player))
                {
                    DelayAction.Add(Vars.Delay, () =>
                    {
                        GameObjects.Player.Spellbook.CastSpell(SpellSlots.Cleanse);
                    });
                }
            }

            /// <summary>
            ///     The Clarity Logic.
            /// </summary>
            if (SpellSlots.Clarity.IsReady())
            {
                if (GameObjects.AllyHeroes.Count(a => a.ManaPercent <= 60) >= 3)
                {
                    GameObjects.Player.Spellbook.CastSpell(SpellSlots.Clarity);
                }
            }

            /// <summary>
            ///     The Ignite Logic.
            /// </summary>
            if (SpellSlots.Ignite.IsReady())
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(600f)))
                {
                    if (Vars.GetIgniteDamage > target.Health ||
                        Health.GetPrediction(target, (int) (1000 + Game.Ping/2f)) <= 0)
                    {
                        GameObjects.Player.Spellbook.CastSpell(SpellSlots.Ignite, target);
                    }
                }
            }

            /// <summary>
            ///     The Barrier Logic.
            /// </summary>
            if (SpellSlots.Barrier.IsReady())
            {
                if (GameObjects.Player.CountEnemyHeroesInRange(700f) > 0 &&
                    Health.GetPrediction(GameObjects.Player, (int) (1000 + Game.Ping/2f)) <= GameObjects.Player.MaxHealth/6)
                {
                    GameObjects.Player.Spellbook.CastSpell(SpellSlots.Barrier);
                    return;
                }
            }

            /// <summary>
            ///     The Heal Logic.
            /// </summary>
            if (SpellSlots.Heal.IsReady())
            {
                foreach (var ally in GameObjects.AllyHeroes.Where(
                    a =>
                        a.IsValidTarget(850f, false) &&
                        a.CountEnemyHeroesInRange(700f) > 0 &&
                        Health.GetPrediction(a, (int) (1000 + Game.Ping/2f)) <= a.MaxHealth/6))
                {
                    GameObjects.Player.Spellbook.CastSpell(SpellSlots.Heal, ally);
                }
            }


            /// <summary>
            ///     The Smite Logics.
            /// </summary>
            if (SpellSlots.GetSmiteSlot().IsReady() &&
                SpellSlots.GetSmiteSlot() != SpellSlot.Unknown)
            {
                /// <summary>
                ///     The Killsteal Smite Logic.
                /// </summary>
                if (Vars.Menu["activator"]["smite"]["killsteal"].GetValue<MenuBool>().Value)
                {
                    if (GameObjects.Player.HasBuff("smitedamagetrackerstalker") ||
                        GameObjects.Player.HasBuff("smitedamagetrackerskirmisher"))
                    {
                        if (Vars.Menu["activator"]["stacks"]["smite"].GetValue<MenuBool>().Value)
                        {
                            if (GameObjects.Player.Spellbook.GetSpell(SpellSlots.GetSmiteSlot()).Ammo == 1)
                            {
                                return;
                            }
                        }

                        foreach (var target in GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(500f)))
                        {
                            if (Vars.GetChallengingSmiteDamage > target.Health && GameObjects.Player.HasBuff("smitedamagetrackerstalker"))
                            {
                                GameObjects.Player.Spellbook.CastSpell(SpellSlots.GetSmiteSlot(), target);
                            }
                            else if (Vars.GetChallengingSmiteDamage > target.Health && GameObjects.Player.HasBuff("smitedamagetrackerskirmisher"))
                            {
                                GameObjects.Player.Spellbook.CastSpell(SpellSlots.GetSmiteSlot(), target);
                            }
                        }
                    }
                }

                /// <summary>
                ///     The Jungle Smite Logic.
                /// </summary>
                foreach (var minion in Targets.JungleMinions.Where(
                    m =>
                        m.IsValidTarget(500f) &&
                        m.Health < GameObjects.Player.GetBuffCount(
                            GameObjects.Player.Buffs.FirstOrDefault(b => b.Name.ToLower().Contains("smitedamagetracker")).Name)))
                {
                    if (Vars.Menu["activator"]["smite"]["limit"].GetValue<MenuBool>().Value)
                    {
                        if (!minion.CharData.BaseSkinName.Equals("SRU_Baron") &&
                            !minion.CharData.BaseSkinName.Equals("SRU_Dragon") &&
                            !minion.CharData.BaseSkinName.Equals("SRU_RiftHerald"))
                        {
                            return;
                        }
                    }

                    if (Vars.Menu["activator"]["smite"]["stacks"].GetValue<MenuBool>().Value)
                    {
                        if (GameObjects.Player.Spellbook.GetSpell(SpellSlots.GetSmiteSlot()).Ammo == 1)
                        {
                            if (!minion.CharData.BaseSkinName.Equals("SRU_Baron") &&
                                !minion.CharData.BaseSkinName.Equals("SRU_Dragon") &&
                                !minion.CharData.BaseSkinName.Equals("SRU_RiftHerald"))
                            {
                                return;
                            }
                        }
                    }

                    GameObjects.Player.Spellbook.CastSpell(SpellSlots.GetSmiteSlot(), minion);
                }
            }

            if (!Targets.Target.IsValidTarget())
            {
                return;
            }

            /// <summary>
            ///     The Exhaust Logic.
            /// </summary>
            if (SpellSlots.Exhaust.IsReady())
            {
                foreach (var ally in GameObjects.AllyHeroes.Where(
                    a =>
                        a.Distance(Targets.Target) <= 650f &&
                        Health.GetPrediction(a, (int) (1000 + Game.Ping/2f)) <= a.MaxHealth/6))
                {
                    GameObjects.Player.Spellbook.CastSpell(SpellSlots.Exhaust, Targets.Target);
                }
            }
        }
    }
}