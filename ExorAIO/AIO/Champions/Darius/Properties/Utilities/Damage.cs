using ExorAIO.Utilities;
using LeagueSharp;

namespace ExorAIO.Champions.Darius
{
    /// <summary>
    ///     The killsteal class.
    /// </summary>
    internal class Damage
    {
        public static float GetRDamage(Obj_AI_Hero target)
            =>
                (float)
                    (Vars.R.GetDamage(target) + (target.HasBuff("dariushemo")
                        ? (1 + 0.20 * target.GetBuffCount("dariushemo"))
                        : 0));
    }
}