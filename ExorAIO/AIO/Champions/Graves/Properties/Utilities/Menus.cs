using ExorAIO.Utilities;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Graves
{
    /// <summary>
    ///     The menu class.
    /// </summary>
    internal class Menus
    {
        /// <summary>
        ///     Sets the menu.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            /// Sets the spells menu.
            /// </summary>
            Vars.SpellsMenu = new Menu("spells", "Spells");
            {
                Vars.QMenu = new Menu("q", "Use Q to:");
                {
                    Vars.QMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.QMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.QMenu.Add(new MenuBool("harass",    "Harass",    true));
                    Vars.QMenu.Add(new MenuBool("logical",   "Logical",   true));
                    Vars.QMenu.Add(new MenuBool("clear",     "Clear",     true));
                    Vars.QMenu.Add(
                        new MenuSlider("manamanager", "Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                Vars.WMenu = new Menu("w", "Use W to:");
                {
                    Vars.WMenu.Add(new MenuBool("combo",     "Combo",          true));
                    Vars.WMenu.Add(new MenuBool("killsteal", "KillSteal",      true));
                    Vars.WMenu.Add(new MenuBool("logical",   "Logical",        true));
                    Vars.WMenu.Add(new MenuBool("gapcloser", "Anti-Gapcloser", true));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                Vars.EMenu = new Menu("e", "Use E to:");
                {
                    Vars.EMenu.Add(new MenuBool("combo", "Combo", true));
                    Vars.EMenu.Add(new MenuBool("clear", "JungleClear", true));
                    Vars.EMenu.Add(
                        new MenuSlider("manamanager", "JungleClear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                Vars.RMenu = new Menu("r", "Use R to:");
                {
                    Vars.RMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.RMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                }
                Vars.SpellsMenu.Add(Vars.RMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            /// Sets the drawings menu.
            /// </summary>
            Vars.DrawingsMenu = new Menu("drawings", "Drawings");
            {
                Vars.DrawingsMenu.Add(new MenuBool("q", "Q Range"));
                Vars.DrawingsMenu.Add(new MenuBool("w", "W Range"));
                Vars.DrawingsMenu.Add(new MenuBool("r", "R Range"));
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}