using ExorAIO.Utilities;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Jinx
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
                    Vars.QMenu.Add(new MenuBool("logical", "Logical", true));
                    //.SetTooltip("Combo Logic + Smart Harass on Mixed.", true);
                    Vars.QMenu.Add(new MenuBool("laneclear", "LaneClear", true));
                    Vars.QMenu.Add(
                        new MenuSlider("manamanager", "LaneClear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                Vars.WMenu = new Menu("w", "Use W to:");
                //.SetFontStyle(FontStyle.Regular, Color.Purple);
                {
                    Vars.WMenu.Add(new MenuBool("combo", "Combo", true));
                    Vars.WMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.WMenu.Add(new MenuBool("harass", "Harass", true));
                     Vars.WMenu.Add(new MenuBool("logical", "Logical", true));
                    //.SetTooltip("Smart Crowd-Control Follow-Up.", true);
                    Vars.WMenu.Add(new MenuSlider("manamanager", "Harass: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                Vars.EMenu = new Menu("e", "Use E to:");
                ;
                //.SetFontStyle(FontStyle.Regular, Color.Cyan);
                {
                    Vars.EMenu.Add(new MenuBool("combo",     "Combo",     true));
                    //.SetTooltip("If it can Hit 2+ Enemies.", true);
                    Vars.EMenu.Add(new MenuBool("gapcloser", "Anti-Gapcloser", true));
                    Vars.EMenu.Add(new MenuBool("logical", "Logical", true));
                    //.SetTooltip("Smart Crowd-Control Follow-Up.", true);
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                Vars.RMenu = new Menu("r", "Use R to:");
                //.SetFontStyle(FontStyle.Regular, Color.Red);
                {
                    Vars.RMenu.Add(new MenuBool("combo",     "Combo",     true));
                    //.SetTooltip("If it can Hit 2+ Enemies with less than 50% HP.", true);
                    Vars.RMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                }
                Vars.SpellsMenu.Add(Vars.RMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            ///     Sets the miscellaneous menu.
            /// </summary>
            Vars.MiscMenu = new Menu("miscellaneous", "Miscellaneous Menu", true);
            {
                Vars.MiscMenu.Add(
                    new MenuBool(
                        "blockq",
                        "Disable/Block Manual PowPow->FishBones Casting in LaneClear"));
                //.SetTooltip("If it doesn't respect the ManaManager MenuSlider condition.", true);
            }
            Vars.Menu.Add(Vars.MiscMenu);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            Vars.DrawingsMenu = new Menu("drawings", "Drawings");
            {
                Vars.DrawingsMenu.Add(new MenuBool("q", "Q Range"));
                //.SetFontStyle(FontStyle.Regular, Color.Green);

                Vars.DrawingsMenu.Add(new MenuBool("w", "W Range"));
                //.SetFontStyle(FontStyle.Regular, Color.Purple);

                Vars.DrawingsMenu.Add(new MenuBool("e", "E Range"));
                //.SetFontStyle(FontStyle.Regular, Color.Cyan);
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}