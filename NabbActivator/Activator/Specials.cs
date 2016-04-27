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
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void Specials(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!Vars.Menu["activator"]["defensives"].GetValue<MenuBool>().Value)
            {
                return;
            }

            foreach (var item in ItemData.Entries.Where(i => Items.CanUseItem((int)i.Id)))
            {
                if (sender != null &&
                    args.Target != null)
                {
                    /// <summary>
                    ///     The Ohmwrecker logic.
                    /// </summary>
                    if ((int)item.Id == 3056)
                    {
                        if (args.Target.IsAlly &&
                            sender is Obj_AI_Turret &&
                            args.Target is Obj_AI_Hero &&
                            sender.IsValidTarget(750f))
                        {
                            Items.UseItem((int)item.Id, sender);
                        }
                    }
                }
            }
        }
    }
}