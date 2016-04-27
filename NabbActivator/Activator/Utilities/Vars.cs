using System.Collections.Generic;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace NabbActivator
{
    /// <summary>
    ///     The Vars class.
    /// </summary>
    internal class Vars
    {
        /// <summary>
        ///     A list of the names of the champions who cast Invalid Snares.
        /// </summary>
        public static readonly List<string> InvalidSnareCasters = new List<string> {"Leona", "Zyra"};

        /// <summary>
        ///     A list of the names of the champions who cast Invalid Stuns.
        /// </summary>
        public static readonly List<string> InvalidStunCasters = new List<string> {"Amumu", "LeeSin", "Alistar", "Hecarim", "Blitzcrank"};

        /// <summary>
        ///     States if the champion has any autoattack resets.
        /// </summary>
        public static bool HasAnyReset = false;

        /// <summary>
        ///     Gets the ignite damage.
        /// </summary>
        public static int GetIgniteDamage = 50 + 20 * GameObjects.Player.Level;

        /// <summary>
        ///     Gets the normal smite's damage.
        /// </summary>
        public static int GetSmiteDamage = 370 + 20 * GameObjects.Player.Level;

        /// <summary>
        ///     Gets the chilling smite's damage.
        /// </summary>
        public static int GetChillingSmiteDamage = 20 + 8 * GameObjects.Player.Level;

        /// <summary>
        ///     Gets the challenging smite's damage.
        /// </summary>
        public static int GetChallengingSmiteDamage = 54 + 6 * GameObjects.Player.Level;

        /// <summary>
        ///     Gets the Delay.
        /// </summary>
        public static int Delay => Menu["activator"]["randomizer"].GetValue<MenuBool>().Value ? WeightedRandom.Next(200, 300) : 0;

        /// <summary>
        ///     Gets or sets the W Spell.
        /// </summary>
        public static Spell W { get; set; }

        /// <summary>
        ///     Gets or sets the assembly menu.
        /// </summary>
        public static Menu Menu { get; set; }

        /// <summary>
        ///     Gets or sets the slider menu.
        /// </summary>
        public static Menu SliderMenu { get; set; }

        /// <summary>
        ///     Gets or sets the smite menu.
        /// </summary>
        public static Menu SmiteMenu { get; set; }
    }
}