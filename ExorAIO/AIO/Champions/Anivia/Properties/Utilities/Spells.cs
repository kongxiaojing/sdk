using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;

namespace ExorAIO.Champions.Anivia
{
    /// <summary>
    ///     The spell class.
    /// </summary>
    internal class Spells
    {
        /// <summary>
        ///     Initializes the spells.
        /// </summary>
        public static void Initialize()
        {
            Vars.Q = new Spell(SpellSlot.Q, 1100f);
            Vars.W = new Spell(SpellSlot.W, 1000f);
            Vars.E = new Spell(SpellSlot.E, 650f);
            Vars.R = new Spell(SpellSlot.R, 625f);

            Vars.Q.SetSkillshot(0.25f, 120f, 850f, false, SkillshotType.SkillshotLine);
            Vars.W.SetSkillshot(0f, 100f, float.MaxValue, false, SkillshotType.SkillshotCircle);
            Vars.E.SetTargetted(0.25f, 1200f);
            Vars.R.SetSkillshot(0.25f, 400f, 1600f, false, SkillshotType.SkillshotCircle);
        }
    }
}