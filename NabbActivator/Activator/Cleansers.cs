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
        public static void Cleansers(EventArgs args)
        {
            if (!Vars.Menu["activator"]["cleansers"].GetValue<MenuBool>().Value)
            {
                return;
            }

            foreach (var item in ItemData.Entries.Where(i => Items.CanUseItem((int)i.Id)))
            {
                /// <summary>
                ///     The Mikaels Crucible Logic.
                /// </summary>
                if ((int)item.Id == 3222)
                {
                    foreach (var ally in GameObjects.AllyHeroes.Where(
                        a =>
                            Bools.ShouldCleanse(a) &&
                            a.IsValidTarget(750f, false)))
                    {
                        DelayAction.Add(Vars.Delay, () =>
                        {
                            Items.UseItem((int)item.Id, ally);
                        });
                    }
                }

                if (!SpellSlots.Cleanse.IsReady() &&
                    SpellSlots.Cleanse == SpellSlot.Unknown)
                {
                    if (Bools.ShouldUseCleanser() ||
                        Bools.ShouldCleanse(ObjectManager.Player))
                    {
                        /// <summary>
                        ///     The Quicksilver Sash Logic.
                        /// </summary>
                        if ((int)item.Id == 3140)
                        {
                            DelayAction.Add(Vars.Delay, () =>
                            {
                                Items.UseItem((int)item.Id);
                                return;
                            });
                        }

                        /// <summary>
                        ///     The Dervish Blade Logic.
                        /// </summary>
                        if ((int)item.Id == 3137)
                        {
                            DelayAction.Add(Vars.Delay, () =>
                            {
                                Items.UseItem((int)item.Id);
                                return;
                            });
                        }

                        /// <summary>
                        ///     The Mercurial Scimitar Logic.
                        /// </summary>
                        if ((int)item.Id == 3139)
                        {
                            DelayAction.Add(Vars.Delay, () =>
                            {
                                Items.UseItem((int)item.Id);
                            });
                        }
                    }
                }
            }
        }
    }
}