using System.Linq;
using System.Windows.Forms;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using Menu = LeagueSharp.SDK.UI.Menu;

namespace ExorAIO.Champions.Nunu
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
                    Vars.QMenu.Add(new MenuBool("jungleclear", "JungleSteal", true));
                    Vars.QMenu.Add(new MenuBool("logical",     "Logical",     true));
                    Vars.QMenu.Add(new MenuBool("laneclear",   "LaneClear",   true));
                    Vars.QMenu.Add(
                        new MenuSlider("manamanager", "LaneClear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                Vars.WMenu = new Menu("w", "Use W to:");
                {
                    Vars.WMenu.Add(new MenuBool("logical", "Logical", true));
                    Vars.WMenu.Add(
                        new MenuSlider("manamanager", "Logical: Mana >= x%", 50, 0, 99));

                    {
                    /// <summary>
                    ///     Sets the whitelist menu for the W.
                    /// </summary>
                        Vars.WhiteListMenu = new Menu("whitelist", "W: Whitelist Menu", true);
                        {
                            foreach (var target in GameObjects.AllyHeroes.Where(h => !h.IsMe))
                            {
                                Vars.WhiteListMenu.Add(
                                    new MenuBool(
                                        target.ChampionName.ToLower(),
                                        $"Use on: {target.ChampionName}",
                                        true));
                            }
                        }
                        Vars.WMenu.Add(Vars.WhiteListMenu);
                    }
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                Vars.EMenu = new Menu("e", "Use E to:");
                {
                    Vars.EMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.EMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.EMenu.Add(new MenuBool("harass",    "Harass",    true));
                    Vars.EMenu.Add(new MenuBool("clear",     "Clear",     true));
                    Vars.EMenu.Add(
                        new MenuSlider("manamanager", "Harass/Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                /// <summary>
                ///     Sets the menu for the R.
                /// </summary>
                Vars.RMenu = new Menu("r", "Use R to:");
                {
                    Vars.RMenu.Add(new MenuBool("bool", "Semi-Automatic R", true));
                    Vars.RMenu.Add(
                        new MenuKeyBind("key", "Key:", Keys.T, KeyBindType.Press));
                }
                Vars.SpellsMenu.Add(Vars.RMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

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