using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Tristana
{
    /// <summary>
    ///     The killsteal class.
    /// </summary>
    internal class KillSteal
    {
        /// <summary>
        ///     Gets the damage from the E stacks + R damage.
        /// </summary>
        public static float GetEDamage(Obj_AI_Hero target)
        {
            double dmg = 0f;

            dmg += Vars.E.GetDamage(target) + (15 + 3 * (target.GetBuffCount("TristanaECharge") + 1)) +
                   (0.15 + 0.045 * GameObjects.Player.FlatPhysicalDamageMod) +
                   0.15 * GameObjects.Player.TotalMagicalDamage;

            return (float) dmg;
        }
    }
}