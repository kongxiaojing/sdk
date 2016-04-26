using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Jhin
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
                    Vars.QMenu.Add(new MenuBool("killsteal",   "KillSteal",   true));
                    Vars.QMenu.Add(new MenuBool("lasthit", "LastHit", true));
                    //.SetTooltip("It will automatically LastHit while Reloading.", true);
                    Vars.QMenu.Add(new MenuBool("harass", "Harass", true));
                    Vars.QMenu.Add(new MenuBool("clear", "Clear", true));
                    Vars.QMenu.Add(new MenuSlider("manamanager", "Harass/Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                Vars.WMenu = new Menu("w", "Use W to:");
                //.SetFontStyle(FontStyle.Regular, Color.Purple);
                {
                     Vars.WMenu.Add(new MenuBool("logical", "Logical", true));
                    //.SetTooltip("On marked enemies in range.", true);
                    Vars.WMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.WMenu.Add(new MenuBool("clear", "Clear", true));
                    Vars.WMenu.Add(new MenuSlider("manamanager", "Clear: Mana >= x%", 50, 0, 99));
                    {
                        Vars.WhiteListMenu = new Menu("whitelistmenu", "W: Whitelist Menu", true);
                        {
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                Vars.WhiteListMenu.Add(
                                    new MenuBool(
                                        target.ChampionName.ToLower(), $"Use against: {target.ChampionName}"));
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
                    Vars.EMenu.Add(new MenuBool("logical", "Logical", true));
                    //.SetTooltip("Smart Crowd-Control Follow-Up.", true);
                    Vars.EMenu.Add(new MenuBool("gapcloser", "Anti-Gapcloser", true));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                Vars.RMenu = new Menu("r", "Use R to:");
                //.SetFontStyle(FontStyle.Regular, Color.Red);
                {
                    Vars.RMenu.Add(
                        new MenuBool("disc", "*--- YOU NEED TO MANUALLY START IT FIRST ---*", true));
                    Vars.RMenu.Add(new MenuBool("combo",     "Combo",     true));
                    //.SetTooltip("It will automatically fire bullets.", true);
                    Vars.RMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    //.SetTooltip("Change targets if you find a killable one instead.", true);
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