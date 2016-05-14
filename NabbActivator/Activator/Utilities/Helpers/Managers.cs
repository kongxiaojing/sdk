using LeagueSharp.SDKEx.UI;

namespace NabbActivator
{
    /// <summary>
    ///     The managers class.
    /// </summary>
    internal class Managers
    {
        /// <summary>
        ///     Sets the minimum necessary health percent to use a health potion.
        /// </summary>
        public static int MinHealthPercent => Vars.Menu["consumablesmenu"]["healthslider"].GetValue<MenuSlider>().Value;

        /// <summary>
        ///     Sets the minimum necessary mana percent to use a mana potion.
        /// </summary>
        public static int MinManaPercent => Vars.Menu["consumablesmenu"]["manaslider"].GetValue<MenuSlider>().Value;
    }
}