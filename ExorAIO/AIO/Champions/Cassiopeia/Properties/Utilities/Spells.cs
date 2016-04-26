using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;

namespace ExorAIO.Champions.Cassiopeia
{
    /// <summary>
    ///     The spells class.
    /// </summary>
    internal class Spells
    {
        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public static void Initialize()
        {
            Vars.Q = new Spell(SpellSlot.Q, 850f);
            Vars.W = new Spell(SpellSlot.W, 850f);
            Vars.E = new Spell(SpellSlot.E, 750f);
            Vars.R = new Spell(SpellSlot.R, 775f);

            Vars.Q.SetSkillshot(
                0.75f, Vars.Q.Instance.SData.CastRadius, float.MaxValue, false, SkillshotType.SkillshotCircle);
            Vars.W.SetSkillshot(
                0.5f, Vars.W.Instance.SData.CastRadius, float.MaxValue, false, SkillshotType.SkillshotCircle);
            Vars.E.SetTargetted(0.125f, float.MaxValue);
            Vars.R.SetSkillshot(0.3f, (float) (80 * Math.PI / 180), float.MaxValue, false, SkillshotType.SkillshotCone);
        }
    }
}