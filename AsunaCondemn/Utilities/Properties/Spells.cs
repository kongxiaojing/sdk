using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.Enumerations;

namespace AsunaCondemn
{
    /// <summary>
    ///     The settings class.
    /// </summary>
    internal class Spells
    {
        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public static void Initialize()
        {
            Vars.E = new Spell(SpellSlot.E, Vars.AARange);
            Vars.Flash = new Spell(ObjectManager.Player.GetSpellSlot("SummonerFlash"), 425f);

            Vars.E.SetSkillshot(0.42f, 60f, 1200f, false, SkillshotType.SkillshotLine);
        }
    }
}