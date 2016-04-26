using ExorAIO.Utilities;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Udyr
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
                Vars.QMenu = new Menu("q", "Use Q to:");
                //.SetFontStyle(FontStyle.Regular, Color.Green);
                {
                    Vars.QMenu.Add(new MenuBool("combo",       "Combo",       true));
                    //.SetTooltip("If The player hasn't Luden's Echo/Runic Echoes.", true);
                    Vars.QMenu.Add(new MenuBool("build", "Buildings", true));
                    Vars.QMenu.Add(new MenuBool("clear", "Clear", true));
                    Vars.QMenu.Add(new MenuSlider("mana", "Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                Vars.WMenu = new Menu("w", "Use W to:");
                //.SetFontStyle(FontStyle.Regular, Color.Purple);
                {
                    Vars.WMenu.Add(new MenuBool("clear", "Clear", true));
                    Vars.WMenu.Add(new MenuSlider("mana", "Clear: Health <= x%", 50, 0, 99));
                    ; //.SetTooltip("If the Player drops below x% of health, in order to LifeSteal.", true);
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                Vars.EMenu = new Menu("e", "Use E to:");
                ;
                //.SetFontStyle(FontStyle.Regular, Color.Cyan);
                {
                    Vars.EMenu.Add(new MenuBool("combo",     "Combo",     true));
                    //.SetTooltip("For Stunnable Targets.", true);
                    Vars.EMenu.Add(new MenuBool("jungleclear", "Clear", true));
                    //.SetTooltip("For Stunnable Jungle Monsters.", true);
                    Vars.EMenu.Add(new MenuSlider("mana", "Clear: Mana >= x%", 59, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                Vars.RMenu = new Menu("r", "Use R to:");
                //.SetFontStyle(FontStyle.Regular, Color.Red);
                {
                    Vars.RMenu.Add(new MenuBool("combo",     "Combo",     true));
                    //.SetTooltip("If The player has Luden's Echo/Runic Echoes.", true);
                    Vars.RMenu.Add(new MenuBool("clear", "Clear", true));
                    Vars.RMenu.Add(new MenuSlider("mana", "Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.RMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            Vars.DrawingsMenu = new Menu("drawings", "Drawings");
            {
                Vars.DrawingsMenu.Add(new MenuBool("r", "R Range"));
                //.SetFontStyle(FontStyle.Regular, Color.Red);
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}