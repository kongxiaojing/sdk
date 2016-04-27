using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;

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
        public static void Consumables(EventArgs args)
        {
            if (ObjectManager.Player.InFountain() ||
                ObjectManager.Player.IsRecalling())
            {
                return;
            }

            foreach (var item in ItemData.Entries.Where(i => Items.CanUseItem((int)i.Id)))
            {
                if (!Bools.IsHealthPotRunning())
                {
                    /// <summary>
                    ///     The Refillable Potion Logic.
                    /// </summary>
                    if ((int)item.Id == 2031 &&
                        ObjectManager.Player.HealthPercent < Managers.MinHealthPercent)
                    {
                        Items.UseItem((int)item.Id);
                        return;
                    }

                    /// <summary>
                    ///     The Total Biscuit of Rejuvenation Logic.
                    /// </summary>
                    if ((int)item.Id == 2010 &&
                        ObjectManager.Player.HealthPercent < Managers.MinHealthPercent)
                    {
                        Items.UseItem((int)item.Id);
                        return;
                    }

                    /// <summary>
                    ///     The Health Potion Logic.
                    /// </summary>
                    if ((int)item.Id == 2003 &&
                        ObjectManager.Player.HealthPercent < Managers.MinHealthPercent)
                    {
                        Items.UseItem((int)item.Id);
                    }
                }

                if (ObjectManager.Player.MaxMana < 200)
                {
                    return;
                }

                /// <summary>
                ///     The Hunter's Potion Logic.
                /// </summary>
                if ((int)item.Id == 2032)
                {
                    if (!Bools.IsHealthPotRunning() &&
                        ObjectManager.Player.HealthPercent < Managers.MinHealthPercent)
                    {
                        Items.UseItem((int)item.Id);
                    }
                    else if (!Bools.IsManaPotRunning() &&
                        ObjectManager.Player.ManaPercent < Managers.MinManaPercent)
                    {
                        Items.UseItem((int)item.Id);
                    }
                }

                /// <summary>
                ///     The Corrupting Potion Logic.
                /// </summary>
                if ((int)item.Id == 2033)
                {
                    if (!Bools.IsHealthPotRunning() &&
                        ObjectManager.Player.HealthPercent < Managers.MinHealthPercent)
                    {
                        Items.UseItem((int)item.Id);
                    }
                    else if (!Bools.IsManaPotRunning() &&
                        ObjectManager.Player.ManaPercent < Managers.MinManaPercent)
                    {
                        Items.UseItem((int)item.Id);
                    }
                }
            }
        }
    }
}