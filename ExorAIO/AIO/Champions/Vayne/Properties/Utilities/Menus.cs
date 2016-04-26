using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Vayne
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
                    Vars.QMenu.Add(new MenuBool("combo",       "Combo",       true));
                    Vars.QMenu.Add(new MenuBool("killsteal",   "KillSteal",   true));
                    Vars.QMenu.Add(new MenuBool("harass",      "Harass",      true));
                    Vars.QMenu.Add(new MenuBool("farmhelper",  "FarmHelper",  true));
                    Vars.QMenu.Add(new MenuBool("jungleclear", "JungleClear", true));
                    Vars.QMenu.Add(
                        new MenuSlider("manamanager", "FarmHelper/JungleClear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                Vars.EMenu = new Menu("e", "Use E to:");
                {
                    Vars.EMenu.Add(new MenuBool("logical", "Logical", true));
                    Vars.EMenu.Add(new MenuBool("gapcloser", "Anti-Gapcloser"));
                    Vars.EMenu.Add(new MenuBool("interrupter", "Interrupt Enemy Channels"));
                    Vars.EMenu.Add(new MenuBool("killsteal", "KillSteal"));
                    {
                        /// <summary>
                        ///     Sets the menu for the E Whitelist.
                        /// </summary>
                        Vars.WhiteListMenu = new Menu("whitelist", "Condemn: Whitelist Menu");
                        {
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                Vars.WhiteListMenu.Add(
                                    new MenuBool(
                                        target.ChampionName.ToLower(),
                                        $"Condemn Only: {target.ChampionName}",
                                        true));
                            }
                        }
                        Vars.EMenu.Add(Vars.WhiteListMenu);
                    }
                }
                Vars.SpellsMenu.Add(Vars.EMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            ///     Sets the miscellaneous menu.
            /// </summary>
            Vars.MiscMenu = new Menu("miscellaneous", "Miscellaneous");
            {
                Vars.MiscMenu.Add(new MenuBool("alwaysq", "Always Q after AA", true));
                Vars.MiscMenu.Add(new MenuBool("stealth", "Don't AA when Stealthed"));
                Vars.MiscMenu.Add(new MenuBool("wstacks", "Use Q only to proc 3rd W Ring"));
            }
            Vars.Menu.Add(Vars.MiscMenu);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            Vars.DrawingsMenu = new Menu("drawings", "Drawings");
            {
                Vars.DrawingsMenu.Add(new MenuBool("q", "Q Range"));
                Vars.DrawingsMenu.Add(new MenuBool("e", "E Range"));
                Vars.DrawingsMenu.Add(new MenuBool("epred", "E Prediction"));
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}