using System.Windows.Forms;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using Menu = LeagueSharp.SDK.UI.Menu;

namespace ExorAIO.Champions.Lucian
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
                    Vars.QMenu.Add(new MenuBool("killsteal",   "KillSteal",   true));
                    Vars.QMenu.Add(new MenuBool("harass", "Minion -> Harass", true));
                    Vars.QMenu.Add(new MenuBool("clear", "Clear", true));
                    Vars.QMenu.Add(new MenuSlider("manamanager", "Harass/Clear: Energy >= x%", 50, 0, 99));
                    {
                        Vars.WhiteListMenu = new Menu(
                            "whitelist", "Minion->Harass: Whitelist", true);
                        {
                            foreach (var champ in GameObjects.EnemyHeroes)
                            {
                                Vars.WhiteListMenu.Add(
                                    new MenuBool(
                                        champ.ChampionName.ToLower(),
                                        $"Harass: {champ.ChampionName}"));
                            }
                        }
                        Vars.QMenu.Add(Vars.WhiteListMenu);
                    }
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                Vars.WMenu = new Menu("w", "Use W to:");
                //.SetFontStyle(FontStyle.Regular, Color.Purple);
                {
                    Vars.WMenu.Add(new MenuBool("combo", "Combo", true));
                    Vars.WMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.WMenu.Add(new MenuBool("clear", "Clear", true));
                    Vars.WMenu.Add(new MenuSlider("manamanager", "Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                Vars.EMenu = new Menu("e", "Use E to:");
                ;
                //.SetFontStyle(FontStyle.Regular, Color.Cyan);
                {
                    Vars.EMenu.Add(new MenuBool("combo",     "Combo",     true));
                    //.SetTooltip("Small Dash if Cursor in AA Range, else long dash.", true);
                    Vars.EMenu.Add(new MenuBool("jungleclear", "JungleClear", true));;
                    Vars.EMenu.Add(
                        new MenuSlider("mana", "JungleClear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                Vars.RMenu = new Menu("r", "Use R to:");
                //.SetFontStyle(FontStyle.Regular, Color.Red);
                {
                    Vars.RMenu.Add(new MenuBool("boolrsa", "Semi-Automatic R", true));
                    Vars.RMenu.Add(
                        new MenuKeyBind("keyrsa", "Key:", Keys.T, KeyBindType.Press));
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
                //.SetFontStyle(FontStyle.Regular, Color.Green);

                Vars.DrawingsMenu.Add(new MenuBool("qe", "Q Extended Range"));
                //.SetFontStyle(FontStyle.Regular, Color.LightGreen);

                Vars.DrawingsMenu.Add(new MenuBool("w", "W Range"));
                //.SetFontStyle(FontStyle.Regular, Color.Purple);

                Vars.DrawingsMenu.Add(new MenuBool("e", "E Range"));
                //.SetFontStyle(FontStyle.Regular, Color.Cyan);
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}