using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

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
        public static void Offensives(EventArgs args)
        {
            if (!Targets.Target.IsValidTarget() ||
                !Vars.Menu["combo"].GetValue<MenuKeyBind>().Active ||
                !Vars.Menu["offensives"].GetValue<MenuBool>().Value)
            {
                return;
            }

            foreach (var item in ItemData.Entries.Where(i => Items.CanUseItem((int)i.Id)))
            {
                /// <summary>
                ///     The Bilgewater Cutlass Logic.
                /// </summary>
                if ((int)item.Id == 3144 &&
                    Targets.Target.IsValidTarget(550f))
                {
                    Items.UseItem((int)item.Id, Targets.Target);
                }

                /// <summary>
                ///     The Blade of the Ruined King Logic.
                /// </summary>
                if ((int)item.Id == 3153 &&
                    Targets.Target.IsValidTarget(550f))
                {
                    Items.UseItem((int)item.Id, Targets.Target);
                }

                /// <summary>
                ///     The Entropy Logic.
                /// </summary>     
                if ((int)item.Id == 3184 &&
                    GameObjects.Player.IsWindingUp)
                {
                    Items.UseItem((int)item.Id);
                }

                /// <summary>
                ///     The Frost Queen's Claim Logic.
                /// </summary>
                if ((int)item.Id == 3092)
                {
                    if (GameObjects.EnemyHeroes.Count(
                            t =>
                                t.IsValidTarget(4000f) &&
                                t.CountEnemyHeroesInRange(1500f) <=
                                    GameObjects.Player.CountAllyHeroesInRange(1500f) + t.CountAllyHeroesInRange(1500f) - 1) >= 1)
                    {
                        Items.UseItem((int)item.Id);
                    }
                }

                /// <summary>
                ///     The Hextech Gunblade Logic.
                /// </summary>
                if ((int)item.Id == 3146 &&
                    Targets.Target.IsValidTarget(700f))
                {
                    Items.UseItem((int)item.Id, Targets.Target);
                }

                /// <summary>
                ///     The Youmuu's Ghostblade Logic.
                /// </summary>
                if ((int)item.Id == 3142)
                {
                    if (GameObjects.Player.IsWindingUp ||
                        GameObjects.Player.IsCastingInterruptableSpell())
                    {
                        Items.UseItem((int)item.Id);
                    }
                }
            }
        }
    }
}