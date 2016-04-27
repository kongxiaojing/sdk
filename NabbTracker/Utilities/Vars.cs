using LeagueSharp;
using LeagueSharp.SDK.UI;
using SharpDX.Direct3D9;

namespace NabbTracker
{
    /// <summary>
    ///     The Vars class.
    /// </summary>
    internal class Vars
    {
        /// <summary>
        ///     Gets the SummonerSpell name.
        /// </summary>
        public static string GetSummonerSpellName;

        /// <summary>
        ///     Gets the spellslots.
        /// </summary>
        public static SpellSlot[] SpellSlots = {SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R};

        /// <summary>
        ///     Gets the summoner spellslots.
        /// </summary>
        public static SpellSlot[] SummonerSpellSlots = {SpellSlot.Summoner1, SpellSlot.Summoner2};

        /// <summary>
        ///     The Menu.
        /// </summary>
        public static Menu Menu { internal get; set; }

        /// <summary>
        ///     The Text fcnt.
        /// </summary>
        public static Font DisplayTextFont { internal get; set; } = new Font(Drawing.Direct3DDevice, new System.Drawing.Font("Tahoma", 8));

        /// <summary>
        ///     The Spells Healthbars X coordinate.
        /// </summary>
        public static int SpellX { internal get; set; }

        /// <summary>
        ///     The Spells Healthbars Y coordinate.
        /// </summary>
        public static int SpellY { internal get; set; }

        /// <summary>
        ///     The SummonerSpells Healthbar X coordinate.
        /// </summary>
        public static int SummonerSpellX { internal get; set; }

        /// <summary>
        ///     The SummonerSpells Healthbar Y coordinate.
        /// </summary>
        public static int SummonerSpellY { internal get; set; }

        /// <summary>
        ///     The SpellLevel X coordinate.
        /// </summary>
        public static int SpellLevelX { internal get; set; }

        /// <summary>
        ///     The Healthbars Y coordinate.
        /// </summary>
        public static int SpellLevelY { internal get; set; }
    }
}