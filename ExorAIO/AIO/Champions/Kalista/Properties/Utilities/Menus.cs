using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Kalista
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
                    Vars.QMenu.Add(new MenuSlider("mana", "Clear: Mana >= %", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                Vars.WMenu = new Menu("w", "Use W to:");
                //.SetFontStyle(FontStyle.Regular, Color.Purple);
                {
                     Vars.WMenu.Add(new MenuBool("logical", "Logical", true));
                     Vars.WMenu.Add(new MenuSlider("mana", "Logical: Mana >= %", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                Vars.EMenu = new Menu("e", "Use E to:");
                //.SetFontStyle(FontStyle.Regular, Color.Cyan);
                {
                    Vars.EMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.EMenu.Add(new MenuBool("jungleclear", "JungleClear", true));;
                    Vars.EMenu.Add(new MenuBool("death", "Before Death", true));
                    Vars.EMenu.Add(new MenuBool("harass", "Minion->Harass", true));
                    Vars.EMenu.Add(new MenuBool("fh", "FarmHelper", true));
                    Vars.EMenu.Add(new MenuBool("farm", "LaneClear", true));
                    Vars.EMenu.Add(
                        new MenuSlider("mana", "FarmHelper/LaneClear: Mana >= %", 50, 0, 99));

                    {
                        Vars.WhiteListMenu = new Menu(
                            "Minion->Harass: Whitelist", "whitelist", true);
                        {
                            foreach (var champ in GameObjects.EnemyHeroes)
                            {
                                Vars.WhiteListMenu.Add(
                                    new MenuBool(
                                        champ.ChampionName.ToLower(), $"Harass: {champ.ChampionName}"));
                            }
                        }
                        Vars.EMenu.Add(Vars.WhiteListMenu);
                    }
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                Vars.RMenu = new Menu("r", "Use R to:");
                //.SetFontStyle(FontStyle.Regular, Color.Red);
                {
                    Vars.RMenu.Add(new MenuBool("lifesaver", "Lifesaver", true));
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

                Vars.DrawingsMenu.Add(new MenuBool("e", "E Range"));
                ;
                //.SetFontStyle(FontStyle.Regular, Color.Cyan);

                Vars.DrawingsMenu.Add(new MenuBool("edmg", "E Damage", true));

                //.SetFontStyle(FontStyle.Regular, Color.Orange);

                Vars.DrawingsMenu.Add(new MenuBool("r", "R Range"));
                //.SetFontStyle(FontStyle.Regular, Color.Red);
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}