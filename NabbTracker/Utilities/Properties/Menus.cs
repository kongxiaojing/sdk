using LeagueSharp.SDK.UI;

namespace NabbTracker
{
    /// <summary>
    ///     The menu class.
    /// </summary>
    internal class Menus
    {
        /// <summary>
        ///     Builds the general Menu.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            ///     The general Menu.
            /// </summary>
            Vars.Menu = new Menu("nabbtracker", "NabbTracker", true);
            {
                Vars.Menu.Add(new MenuBool("allies", "Allies", true));
                Vars.Menu.Add(new MenuBool("enemies", "Enemies", true));

                /// <summary>
                ///     The miscellaneous Menu.
                /// </summary>
                Vars.MiscMenu = new Menu("misc", "Miscellaneous", true);
                {
                    Vars.MiscMenu.Add(new MenuBool("colorblind", "Colorblind Mode"));
                }
                Vars.Menu.Add(Vars.MiscMenu);
            }
            Vars.Menu.Attach();
        }
    }
}