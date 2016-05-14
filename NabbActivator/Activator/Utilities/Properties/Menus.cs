using System.Windows.Forms;
using LeagueSharp.SDKEx.Enumerations;
using LeagueSharp.SDKEx.UI;
using Menu = LeagueSharp.SDKEx.UI.Menu;

namespace NabbActivator
{
    /// <summary>
    ///     The menu class.
    /// </summary>
    internal class Menus
    {
        /// <summary>
        ///     Sets the menus.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            ///     Sets the main menu.
            /// </summary>
            Vars.Menu = new Menu("activator", "NabbActivator", true);
            {
                Vars.Menu.Add(new MenuBool("offensives",  "Offensives",           true));
                Vars.Menu.Add(new MenuBool("defensives",  "Defensives",           true));
                Vars.Menu.Add(new MenuBool("spells",      "Spells",               true));
                Vars.Menu.Add(new MenuBool("cleansers",   "Cleansers",            true));
                Vars.Menu.Add(new MenuBool("consumables", "Potions",              true));
                Vars.Menu.Add(new MenuBool("resetters",   "Tiamat/Hydra/Titanic", true));
                Vars.Menu.Add(new MenuBool("randomizer",  "Humanizer"));
                Vars.Menu.Add(
                    new MenuKeyBind("combokey", "Combo:", Keys.Space, KeyBindType.Press));
                Vars.Menu.Add(
                    new MenuKeyBind("laneclearkey", "LaneClear:", Keys.V, KeyBindType.Press));


                /// <summary>
                ///     Sets consumable sliders menu.
                /// </summary>
                Vars.SliderMenu = new Menu("consumablesmenu", "Consumables Menu");
                {
                    Vars.SliderMenu.Add(new MenuSlider("healthslider", "Consumables: Health < x%", 50, 0, 100));
                    Vars.SliderMenu.Add(new MenuSlider("manaslider",   "Consumables: Mana < x%",   50, 0, 100));
                }
                Vars.Menu.Add(Vars.SliderMenu);

                /// <summary>
                ///     Sets smite menu.
                /// </summary>
                Vars.SmiteMenu = new Menu("smite", "Smite Menu");
                {
                    Vars.SmiteMenu.Add(new MenuBool("combo",     "Combo",                                true));
                    Vars.SmiteMenu.Add(new MenuBool("killsteal", "KillSteal",                            true));
                    Vars.SmiteMenu.Add(new MenuBool("limit",     "Only on Dragon/Baron/Herald"               ));
                    Vars.SmiteMenu.Add(new MenuBool("stacks",    "Keep 1 Stack for Dragon/Baron/Herald", true));
                }
                Vars.Menu.Add(Vars.SmiteMenu);
            }
            Vars.Menu.Attach();
        }
    }
}