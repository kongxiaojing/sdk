using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.KogMaw
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
                    Vars.QMenu.Add(new MenuBool("logical", "Logical", true));
                    //.SetTooltip("Smart Crowd-Control Follow-Up.", true);
                    Vars.QMenu.Add(new MenuBool("killsteal",   "KillSteal",   true));
                    Vars.QMenu.Add(new MenuBool("gapcloser", "Anti-Gapcloser", true));
                    Vars.QMenu.Add(new MenuBool("jungleclear", "JungleClear", true));
                    Vars.QMenu.Add(
                        new MenuSlider("mana", "JungleClear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                Vars.WMenu = new Menu("w", "Use W to:");
                //.SetFontStyle(FontStyle.Regular, Color.Purple);
                {
                    Vars.WMenu.Add(new MenuBool("combo", "Combo", true));
                    Vars.WMenu.Add(new MenuBool("clear", "Clear", true));
                    //.SetTooltip("Only if the player has Runaan's Hurricane or the target is a Jungle Minion.", true);
                    Vars.WMenu.Add(new MenuSlider("manamanager", "Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                Vars.EMenu = new Menu("e", "Use E to:");
                ;
                //.SetFontStyle(FontStyle.Regular, Color.Cyan);
                {
                    Vars.EMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.EMenu.Add(new MenuBool("logical", "Logical", true));
                    //.SetTooltip("Smart Crowd-Control Follow-Up.", true);
                    Vars.EMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.EMenu.Add(new MenuBool("gapcloser", "Anti-Gapcloser", true));
                    Vars.EMenu.Add(new MenuBool("clear",     "Clear",     true));
                    Vars.EMenu.Add(new MenuSlider("manamanager", "Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                Vars.RMenu = new Menu("r", "Use R to:");
                //.SetFontStyle(FontStyle.Regular, Color.Red);
                {
                    Vars.RMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.RMenu.Add(new MenuBool("logical", "Logical", true));
                    //.SetTooltip("Smart Crowd-Control Follow-Up.", true);
                    Vars.RMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.RMenu.Add(new MenuSlider("mana", "Combo: Mana >= x", 60, 1, 99));
                    Vars.RMenu.Add(new MenuSlider("stacks", "Combo: Stacks <= x", 2, 1, 10));
                    {
                        Vars.WhiteListMenu = new Menu("whitelist", "Ultimate: Whitelist Menu");
                        {
                            foreach (var champ in GameObjects.EnemyHeroes)
                            {
                                Vars.WhiteListMenu.Add(
                                    new MenuBool(
                                        champ.ChampionName.ToLower(), $"Use against: {champ.ChampionName}", true));
                            }
                        }
                        Vars.RMenu.Add(Vars.WhiteListMenu);
                    }
                }
                Vars.SpellsMenu.Add(Vars.RMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            ///     Sets the miscellaneous menu.
            /// </summary>
            Vars.MiscMenu = new Menu("miscellaneous", "Miscellaneous Menu", true);
            {
                Vars.MiscMenu.Add(new MenuBool("urf", "Enable URF Mode", false));
                ; //.SetTooltip("Remove Mana/Stacks Checks for R.", true);
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

                Vars.DrawingsMenu.Add(new MenuBool("r", "R Range"));
                //.SetFontStyle(FontStyle.Regular, Color.Red);
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}