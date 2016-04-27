using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using LeagueSharp.SDK.Enumerations;

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
        public static void Defensives(EventArgs args)
        {
            if (!Vars.Menu["activator"]["combo"].GetValue<MenuKeyBind>().Active ||
                !Vars.Menu["activator"]["defensives"].GetValue<MenuBool>().Value)
            {
                return;
            }

            foreach (var item in ItemData.Entries.Where(i => Items.CanUseItem((int)i.Id)))
            {
                /// <summary>
                ///     The Zeke's Herald Logic.
                /// </summary>
                if ((int)item.Id == 3153)
                {
                    if (GameObjects.AllyHeroes.Any(
                        a =>
                            a.HasBuff("itemstarksbindingbufferproc") ||
                            (!a.IsDead && a.HasBuff("rallyingbanneraurafriend"))))
                    {
                        return;
                    }

                    if (GameObjects.AllyHeroes.OrderBy(t => t.FlatCritChanceMod).First().IsValidTarget(800f, false))
                    {
                        Items.UseItem((int)item.Id, GameObjects.AllyHeroes.OrderBy(t => t.FlatCritChanceMod).First());
                    }
                }

                /// <summary>
                ///     The Banner of Command Logic.
                /// </summary>
                if ((int)item.Id == 3060)
                {
                    if (GameObjects.AllyMinions.Any(m => m.GetMinionType() == MinionTypes.Super))
                    {
                        foreach (var super in GameObjects.AllyMinions.Where(
                            m =>
                                m.IsValidTarget(1200f, false) &&
                                m.GetMinionType() == MinionTypes.Super))
                        {
                            Items.UseItem((int)item.Id, super);
                        }
                    }
                    else if (GameObjects.AllyMinions.Any(m => m.GetMinionType() == MinionTypes.Siege))
                    {
                        foreach (var siege in GameObjects.AllyMinions.Where(
                            m =>
                                m.IsValidTarget(1200f, false) &&
                                m.GetMinionType() == MinionTypes.Siege))
                        {
                            Items.UseItem((int)item.Id, siege);
                        }
                    }
                }

                /// <summary>
                ///     The Face of the Mountain Logic.
                /// </summary>
                if ((int)item.Id == 3401)
                {
                    foreach (var ally in GameObjects.AllyHeroes.Where(
                        a =>
                            a.IsValidTarget(500f, false) &&
                            Health.GetPrediction(a, (int)(250 + Game.Ping/2f)) <= a.MaxHealth/4))
                    {
                        Items.UseItem((int)item.Id, ally);
                        return;
                    }
                }

                /// <summary>
                ///     The Locket of the Iron Solari Logic.
                /// </summary>
                if ((int)item.Id == 3190 &&
                    !Items.CanUseItem(3401))
                {
                    if (GameObjects.AllyHeroes.Count(
                        a =>
                            a.IsValidTarget(600f, false) &&
                            Health.GetPrediction(a, (int)(250 + Game.Ping/2f)) <= a.MaxHealth/1.5) >= 3)
                    {
                        Items.UseItem((int)item.Id);
                        return;
                    }
                }

                /// <summary>
                ///     The Zhonya's Hourglass Logic.
                /// </summary>
                if ((int)item.Id == 3157)
                {
                    if (Health.GetPrediction(ObjectManager.Player, (int)(250 + Game.Ping/2f)) <= ObjectManager.Player.MaxHealth/4)
                    {
                        Items.UseItem((int)item.Id);
                        return;
                    }
                }

                /// <summary>
                ///     The Wooglet's Witchcap Logic.
                /// </summary>
                if ((int)item.Id == 3090)
                {
                    if (Health.GetPrediction(ObjectManager.Player, (int)(250 + Game.Ping/2f)) <= ObjectManager.Player.MaxHealth/4)
                    {
                        Items.UseItem((int) item.Id);
                        return;
                    }
                }

                /// <summary>
                ///     The Seraph's Embrace Logic.
                /// </summary>
                if ((int)item.Id == 3040)
                {
                    if (Health.GetPrediction(ObjectManager.Player, (int)(250 + Game.Ping/2f)) <= ObjectManager.Player.MaxHealth/4)
                    {
                        Items.UseItem((int)item.Id);
                        return;
                    }
                }

                /// <summary>
                ///     The Guardian's Horn Logic.
                /// </summary>
                if ((int)item.Id == 2051)
                {
                    if (GameObjects.EnemyHeroes.Count(t => t.IsValidTarget(1000f)) >= 3)
                    {
                        Items.UseItem((int)item.Id);
                        return;
                    }
                }

                /// <summary>
                ///     The Talisman of Ascension Logic.
                /// </summary>
                if ((int)item.Id == 3059)
                {
                    if (GameObjects.EnemyHeroes.Count(
                            t =>
                                t.IsValidTarget(2000f) &&
                                t.CountEnemyHeroesInRange(1500f) <=
                                    ObjectManager.Player.CountAllyHeroesInRange(1500f) + t.CountAllyHeroesInRange(1500f) - 1) > 1)
                    {
                        Items.UseItem((int)item.Id);
                        return;
                    }
                }

                /// <summary>
                ///     The Righteous Glory Logic.
                /// </summary>
                if ((int)item.Id == 3800)
                {
                    if (!ObjectManager.Player.HasBuff("ItemRighteousGlory"))
                    {
                        if (GameObjects.EnemyHeroes.Count(
                                t =>
                                    t.IsValidTarget(2000f) &&
                                    t.CountEnemyHeroesInRange(1500f) <=
                                        ObjectManager.Player.CountAllyHeroesInRange(1500f) + t.CountAllyHeroesInRange(1500f) - 1) > 1)
                        {
                            Items.UseItem((int)item.Id);
                            return;
                        }
                    }
                    else
                    {
                        if (ObjectManager.Player.CountEnemyHeroesInRange(450f) >= 2)
                        {
                            Items.UseItem((int)item.Id);
                        }
                    }
                    return;
                }

                /// <summary>
                ///     The Randuin's Omen Logic.
                /// </summary>
                if ((int)item.Id == 3143)
                {
                    if (ObjectManager.Player.CountEnemyHeroesInRange(500f) >= 2)
                    {
                        Items.UseItem((int)item.Id);
                    }
                }
            }
        }
    }
}