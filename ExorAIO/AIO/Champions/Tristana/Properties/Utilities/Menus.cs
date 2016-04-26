using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Tristana
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
                    Vars.QMenu.Add(new MenuBool("logical", "Logical", true));
                    //.SetTooltip("If target has E charges or E is not ready.", true);
                    Vars.QMenu.Add(new MenuBool("clear", "Clear", true));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                Vars.EMenu = new Menu("e", "Use E to:");
                ;
                //.SetFontStyle(FontStyle.Regular, Color.Cyan);
                {
                    Vars.EMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.EMenu.Add(new MenuBool("build", "Buildings", true));
                    Vars.EMenu.Add(new MenuBool("harass", "Harass", true));
                    Vars.EMenu.Add(new MenuBool("clear",     "Clear",     true));
                    Vars.EMenu.Add(
                        new MenuSlider("manamanager", "Harass/Clear: Mana >= x%", 50, 0, 99));

                    {
                        Vars.WhiteListMenu = new Menu(
                            "E: Whitelist Menu", "whitelistmenu", true);
                        {
                            foreach (var champ in GameObjects.EnemyHeroes)
                            {
                                Vars.WhiteListMenu.Add(
                                    new MenuBool(
                                        champ.ChampionName.ToLower(),
                                        $"E: Only: {champ.ChampionName}"));
                            }
                        }
                        Vars.EMenu.Add(Vars.WhiteListMenu);
                    }
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                Vars.RMenu = new Menu("r", "Use R to:");
                //.SetFontStyle(FontStyle.Regular, Color.Red);
                {
                    Vars.RMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    //.SetTooltip("With E Damage Caclearulation Support.", true);
                }
                Vars.SpellsMenu.Add(Vars.RMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            /// Sets the drawings menu.
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