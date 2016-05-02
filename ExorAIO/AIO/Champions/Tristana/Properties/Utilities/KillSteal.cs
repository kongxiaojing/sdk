using LeagueSharp;
using LeagueSharp.Data.Enumerations;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Tristana
{
    /// <summary>
    ///     The killsteal class.
    /// </summary>
    internal class KillSteal
    {
        /// <summary>
        ///     Gets the perfect damage reduction from sources.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The damage dealt against all the sources.
        /// </returns>
        public static float GetEDamage(Obj_AI_Hero target)
        {
            var healthDebuffer = 0f;
            var damage =
                (float)GameObjects.Player.GetSpellDamage(target, SpellSlot.E) +
                (float)GameObjects.Player.GetSpellDamage(target, SpellSlot.E, DamageStage.Buff);

            /// <summary>
            ///     Gets the predicted reduction from Blitzcrank Shield.
            /// </summary>
            if (target is Obj_AI_Hero)
            {
                if ((target as Obj_AI_Hero).ChampionName.Equals("Blitzcrank") &&
                    !(target as Obj_AI_Hero).HasBuff("BlitzcrankManaBarrierCD"))
                {
                    healthDebuffer += target.Mana / 2;
                }
            }

            return damage 
                - target.PhysicalShield 
                - target.HPRegenRate
                - healthDebuffer;
        }
    }
}