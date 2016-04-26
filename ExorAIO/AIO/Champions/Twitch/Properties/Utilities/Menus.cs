using ExorAIO.Utilities;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Twitch
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
            ///     Sets the prediction menu.
            /// </summary>


            /// <summary>
            ///     Sets the spells menu.
            /// </summary>
            Vars.SpellsMenu = new Menu("spells", "Spells");
            {
                Vars.QMenu = new Menu("q", "Use Q to:");
                //.SetFontStyle(FontStyle.Regular, Color.Green);
                {
                    Vars.QMenu.Add(new MenuBool("combo",       "Combo",       true));
                    //.SetTooltip("After AA, then Reset every Kill/Assist.", true);
                    Vars.QMenu.Add(new MenuBool("logical", "Logical", true));
                    //.SetTooltip("On Recall.", true);
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                Vars.WMenu = new Menu("w", "Use W to:");
                //.SetFontStyle(FontStyle.Regular, Color.Purple);
                {
                    Vars.WMenu.Add(new MenuBool("combo", "Combo", true));
                    //.SetTooltip("If Enemy has less than 4 Venom stacks, and Player isn't in R stance.", true);
                    Vars.WMenu.Add(new MenuBool("harass", "Harass", true));
                    //.SetTooltip("If Enemy has less than 4 Venom stacks, and Player isn't in R stance.", true);
                    Vars.WMenu.Add(new MenuBool("clear", "Clear", true));
                    Vars.WMenu.Add(
                        new MenuSlider("manamanager", "Harass/Clear: Mana >= x", 50, 10, 99));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                Vars.EMenu = new Menu("e", "Use E to:");
                ;
                //.SetFontStyle(FontStyle.Regular, Color.Cyan);
                {
                    Vars.EMenu.Add(new MenuBool("logical", "Logical", true));
                    //.SetTooltip("If enemy with 6 Venom Stacks in Range.", true);
                    Vars.EMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.EMenu.Add(new MenuBool("jungleclear", "JungleSteal", true));
                    Vars.EMenu.Add(new MenuBool("laneclear", "LaneClear", true));
                    Vars.EMenu.Add(
                        new MenuSlider("mana", "LaneClear: Mana >= x", 50, 10, 99));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            Vars.DrawingsMenu = new Menu("drawings", "Drawings");
            {
                Vars.DrawingsMenu.Add(new MenuBool("w", "W Range"));
                //.SetFontStyle(FontStyle.Regular, Color.Purple);

                Vars.DrawingsMenu.Add(new MenuBool("e", "E Range"));
                //.SetFontStyle(FontStyle.Regular, Color.Cyan);

                Vars.DrawingsMenu.Add(new MenuBool("r", "R Range"));
                //.SetFontStyle(FontStyle.Regular, Color.Red);
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}