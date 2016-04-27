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
            /// The general Menu.
            /// </summary>
            Vars.Menu = new Menu("settings", "Settings", true);
            {
                Vars.Menu.Add(new MenuBool("allies", "Allies", true));
                Vars.Menu.Add(new MenuBool("enemies", "Enemies", true));
            }
        }
    }
}