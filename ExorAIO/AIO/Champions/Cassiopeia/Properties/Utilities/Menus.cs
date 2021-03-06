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
                    Vars.QMenu.Add(new MenuSliderButton("harass", "Harass / if Mana >= x%", 50, 0, 99, true));
                    Vars.QMenu.Add(new MenuSliderButton("clear",  "Clear / if Mana >= x%",  50, 0, 99, true));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                Vars.WMenu = new Menu("w", "Use W to:");
                {
                    Vars.WMenu.Add(new MenuBool("combo",     "Combo",          true));
                    Vars.WMenu.Add(new MenuBool("logical",   "Logical",        true));
                    Vars.WMenu.Add(new MenuBool("gapcloser", "Anti-Gapcloser", true));
                    Vars.WMenu.Add(new MenuSliderButton("harass", "Harass / if Mana >= x%", 50, 0, 99, true));
                    Vars.WMenu.Add(new MenuSliderButton("clear",  "Clear / if Mana >= x%",  50, 0, 99, true));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                Vars.EMenu = new Menu("e", "Use E to:");
                {
                    Vars.EMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.EMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.EMenu.Add(new MenuSliderButton("clear",   "Clear / if Mana >= x%", 50, 0, 99, true));
                    Vars.EMenu.Add(new MenuSliderButton("lasthit", "LastHit only / if Mana < x%", 50, 1, 100, true));
                    Vars.EMenu.Add(new MenuSlider("delay", "E Delay (ms)", 0, 0, 250));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                /// <summary>
                ///     Sets the menu for the R.
                /// </summary>
                Vars.RMenu = new Menu("r", "Use R to:");
                {
                    Vars.RMenu.Add(new MenuSliderButton("combo", "Combo / if facing Enemies >=", 1, 1, 5, true));
                    Vars.RMenu.Add(new MenuBool("killsteal",   "KillSteal",      true));
                    Vars.RMenu.Add(new MenuBool("gapcloser",   "Anti-Gapcloser", true));
                    Vars.RMenu.Add(new MenuBool("interrupter", "Interrupter",    true));
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
                Vars.MiscMenu.Add(new MenuSliderButton("tear", "Stack Tear / if Mana >= x%", 80, 0, 95, true));
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