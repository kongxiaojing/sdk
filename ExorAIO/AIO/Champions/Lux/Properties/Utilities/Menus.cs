using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Lux
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
            /// Sets the prediction menu.
            /// </summary>


            /// <summary>
            /// Sets the spells menu.
            /// </summary>
            Vars.SpellsMenu = new Menu("spells", "Spells");
            {
                Vars.QMenu = new Menu("q", "Use Q to:");
                //.SetFontStyle(FontStyle.Regular, Color.Green);
                {
                    Vars.QMenu.Add(new MenuBool("combo",       "Combo",       true));
                    Vars.QMenu.Add(new MenuBool("killsteal",   "KillSteal",   true));
                    Vars.QMenu.Add(new MenuBool("clear", "Clear", true));
                    Vars.QMenu.Add(new MenuSlider("mana", "Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                Vars.WMenu = new Menu("w", "Use W to:");
                //.SetFontStyle(FontStyle.Regular, Color.Purple);
                {
                     Vars.WMenu.Add(new MenuBool("logical", "Logical", true));
                    {
                        Vars.WhiteListMenu = new Menu("whitelistmenu", "Shield: Whitelist Menu", true);
                        {
                            foreach (var champ in GameObjects.AllyHeroes)
                            {
                                Vars.WhiteListMenu.Add(
                                    new MenuBool(
                                        champ.ChampionName.ToLower(), $"Use for: {champ.ChampionName}"));
                            }
                        }
                        Vars.WMenu.Add(Vars.WhiteListMenu);
                    }
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                Vars.EMenu = new Menu("e", "Use E to:");
                ;
                //.SetFontStyle(FontStyle.Regular, Color.Cyan);
                {
                    Vars.EMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.EMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.EMenu.Add(new MenuBool("clear",     "Clear",     true));
                    Vars.EMenu.Add(new MenuSlider("manamanager", "Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                Vars.RMenu = new Menu("r", "Use R to:");
                //.SetFontStyle(FontStyle.Regular, Color.Red);
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
                //.SetFontStyle(FontStyle.Regular, Color.Green);

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