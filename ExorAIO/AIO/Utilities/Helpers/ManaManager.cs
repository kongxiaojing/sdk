using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Utilities
{
    /// <summary>
    ///     The Mana manager class.
    /// </summary>
    internal class ManaManager
    {
        /// <summary>
        ///     The minimum mana needed to cast the Q Spell.
        /// </summary>
        public static int NeededQMana
            =>
                Vars.Menu["spells"]["q"]["manamanager"] != null
                    ? Vars.Menu["spells"]["q"]["manamanager"].GetValue<MenuSlider>().Value +
                        (int)(GameObjects.Player.Spellbook.GetSpell(Vars.Q.Slot).ManaCost / GameObjects.Player.MaxMana * 100)
                    : 0;

        /// <summary>
        ///     The minimum mana needed to cast the Extended Q Spell in Mixed.
        /// </summary>
        public static int NeededQMixedMana
            =>
                Vars.Menu["spells"]["q"]["extended"]["mixed"] != null
                    ? Vars.Menu["spells"]["q"]["extended"]["mixed"].GetValue<MenuSliderButton>().SValue +
                        (int)(GameObjects.Player.Spellbook.GetSpell(Vars.Q.Slot).ManaCost / GameObjects.Player.MaxMana * 100)
                    : 0;

        /// <summary>
        ///     The minimum mana needed to cast the Extended Q Spell in Mixed.
        /// </summary>
        public static int NeededQLaneClearMana
            =>
                Vars.Menu["spells"]["q"]["extended"]["laneclear"] != null
                    ? Vars.Menu["spells"]["q"]["extended"]["laneclear"].GetValue<MenuSliderButton>().SValue +
                        (int)(GameObjects.Player.Spellbook.GetSpell(Vars.Q.Slot).ManaCost / GameObjects.Player.MaxMana * 100)
                    : 0;

        /// <summary>
        ///     The minimum mana needed to cast the W Spell.
        /// </summary>
        public static int NeededWMana
            =>
                Vars.Menu["spells"]["w"]["manamanager"] != null
                    ? Vars.Menu["spells"]["w"]["manamanager"].GetValue<MenuSlider>().Value +
                        (int)(GameObjects.Player.Spellbook.GetSpell(Vars.W.Slot).ManaCost / GameObjects.Player.MaxMana * 100)
                    : 0;

        /// <summary>
        ///     The minimum mana needed to cast the E Spell.
        /// </summary>
        public static int NeededEMana
            =>
                Vars.Menu["spells"]["e"]["manamanager"] != null
                    ? Vars.Menu["spells"]["e"]["manamanager"].GetValue<MenuSlider>().Value +
                        (int)(GameObjects.Player.Spellbook.GetSpell(Vars.E.Slot).ManaCost / GameObjects.Player.MaxMana * 100)
                    : 0;

        /// <summary>
        ///     The minimum mana needed to cast the R Spell.
        /// </summary>
        public static int NeededRMana
            =>
                Vars.Menu["spells"]["r"]["manamanager"] != null
                    ? Vars.Menu["spells"]["r"]["manamanager"].GetValue<MenuSlider>().Value +
                        (int)(GameObjects.Player.Spellbook.GetSpell(Vars.R.Slot).ManaCost / GameObjects.Player.MaxMana * 100)
                    : 0;

        /// <summary>
        ///     The minimum mana needed to stack the Tear Item.
        /// </summary>
        public static float NeededTearMana
            =>
                Vars.Menu["miscellaneous"]["manamanager"] != null
                    ? Vars.Menu["miscellaneous"]["manamanager"].GetValue<MenuSlider>().Value +
                        (int)(GameObjects.Player.Spellbook.GetSpell(Vars.Q.Slot).ManaCost / GameObjects.Player.MaxMana * 100)
                    : 0;
    }
}