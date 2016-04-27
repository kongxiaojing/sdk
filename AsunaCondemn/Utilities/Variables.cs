using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace AsunaCondemn
{
    /// <summary>
    ///     The Vars class.
    /// </summary>
    internal class Vars
    {
        /// <summary>
        ///     Gets or sets the E Spell.
        /// </summary>
        public static Spell E { get; internal set; }

        /// <summary>
        ///     Gets or sets the Flash Spell.
        /// </summary>
        public static Spell Flash { get; internal set; }

        /// <summary>
        ///     Gets or sets the assembly menu.
        /// </summary>
        public static Menu Menu { get; internal set; }

        /// <summary>
        ///     Gets or sets the E Spell menu.
        /// </summary>
        public static Menu EMenu { get; internal set; }

        /// <summary>
        ///     Gets or sets the Drawings menu.
        /// </summary>
        public static Menu DrawingsMenu { get; internal set; }

        /// <summary>
        ///     Gets the Player's real AutoAttack-Range.
        /// </summary>
        public static float AARange => GameObjects.Player.GetRealAutoAttackRange();
    }
}