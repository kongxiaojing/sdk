using ExorAIO.Utilities;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Cassiopeia
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
            ///     Sets the spells menu.
            /// </summary>
            Vars.SpellsMenu = new Menu("spells", "Spells");
            {
                /// <summary>
                ///     Sets the menu for the Q.
                /// </summary>
                Vars.QMenu = new Menu("q", "Use Q to:");
                {
                    Vars.QMenu.Add(new MenuBool("combo",   "Combo",   true));
                    Vars.QMenu.Add(new MenuBool("logical", "Logical", true));
                    Vars.QMenu.Add(new MenuBool("harass",  "Harass",  true));
                    Vars.QMenu.Add(new MenuBool("clear",   "Clear",   true));
                    Vars.QMenu.Add(
                        new MenuSlider("manamanager", "Harass/Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                Vars.WMenu = new Menu("w", "Use W to:");
                {
                    Vars.WMenu.Add(new MenuBool("combo",   "Combo",   true));
                    Vars.WMenu.Add(new MenuBool("logical", "Logical", true));
                    Vars.WMenu.Add(new MenuBool("harass",  "Harass",  true));
                    Vars.WMenu.Add(new MenuBool("clear",   "Clear",   true));
                    Vars.WMenu.Add(
                        new MenuSlider("manamanager", "Harass/Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                Vars.EMenu = new Menu("e", "Use E to:");
                {
                    Vars.EMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.EMenu.Add(new MenuBool("lasthit",   "LastHit", true));
                    Vars.EMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.EMenu.Add(new MenuBool("clear",     "Clear",     true));
                    Vars.EMenu.Add(
                        new MenuSlider("manamanager", "Clear: Mana >= x%", 50, 0, 99));
                    Vars.EMenu.Add(new MenuSlider("delay", "E Delay (ms)", 0, 0, 250));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                /// <summary>
                ///     Sets the menu for the R.
                /// </summary>
                Vars.RMenu = new Menu("r", "Use R to:");
                //.SetFontStyle(FontStyle.Regular, Color.Red);
                {
                    Vars.RMenu.Add(new MenuBool("killsteal",   "KillSteal",      true));
                    Vars.RMenu.Add(new MenuBool("combo",       "Combo",          true));
                    Vars.RMenu.Add(new MenuBool("gapcloser",   "Anti-Gapcloser", true));
                    Vars.RMenu.Add(new MenuBool("interrupter", "Interrupter",    true));
                    Vars.RMenu.Add(
                        new MenuSlider("enemies", "Combo: if facing Enemies >=", 1, 1, 5));
                }
                Vars.SpellsMenu.Add(Vars.RMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            ///     Sets the miscellaneous menu.
            /// </summary>
            Vars.MiscMenu = new Menu("miscellaneous", "Miscellaneous");
            {
                Vars.MiscMenu.Add(new MenuBool("noaa", "Don't AA in Combo"));
                Vars.MiscMenu.Add(new MenuBool("tear", "Stack Tear", true));
                Vars.MiscMenu.Add(
                    new MenuSlider("manamanager", "Stack Tear: Mana >= x%", 80, 1, 95));
            }
            Vars.Menu.Add(Vars.MiscMenu);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            Vars.DrawingsMenu = new Menu("drawings", "Drawings");
            {
                Vars.DrawingsMenu.Add(new MenuBool("q", "Q Range"));
                Vars.DrawingsMenu.Add(new MenuBool("w", "W Range"));
                Vars.DrawingsMenu.Add(new MenuBool("e", "E Range"));
                Vars.DrawingsMenu.Add(new MenuBool("r", "R Range"));
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}