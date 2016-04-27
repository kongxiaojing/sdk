using System.Windows.Forms;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using Menu = LeagueSharp.SDK.UI.Menu;

namespace AsunaCondemn
{
    /// <summary>
    ///     The settings class.
    /// </summary>
    internal class Menus
    {
        /// <summary>
        ///     Sets the menu.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            ///     Sets the main menu.
            /// </summary>
            Vars.Menu = new Menu("asunacondemn", "AsunaCondemn", true);
            {
                /// <summary>
                ///     Sets the spells menu.
                /// </summary>
                Vars.EMenu = new Menu("e", "Use E to:");
                {
                    Vars.Menu.Add(new MenuKeyBind("logical", "Execute:", Keys.Space, KeyBindType.Press));
                }
                Vars.Menu.Add(Vars.EMenu);

                /// <summary>
                ///     Sets the drawings menu.
                /// </summary>
                Vars.DrawingsMenu = new Menu("drawings", "Drawings");
                {
                    Vars.DrawingsMenu.Add(new MenuBool("e", "E Prediction", true));
                }
                Vars.Menu.Add(Vars.DrawingsMenu);
            }
        }
    }
}