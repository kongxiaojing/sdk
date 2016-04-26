using LeagueSharp;
using LeagueSharp.Data.Enumerations;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Kalista
{
    /// <summary>
    ///     The Killsteal class.
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
        public static float GetPerfectRendDamage(Obj_AI_Base target)
        {
            var healthDebuffer = 0f;
            var RendDamage = (float) GameObjects.Player.GetSpellDamage(target, SpellSlot.E) +
                             (float) GameObjects.Player.GetSpellDamage(target, SpellSlot.E, DamageStage.Buff);

            if (target.Type.Equals(GameObjectType.obj_AI_Hero))
            {
                /// <summary>
                /// Gets the predicted reduction from Blitzcrank Shield.
                /// </summary>
                if ((target as Obj_AI_Hero).ChampionName.Equals("Blitzcrank") &&
                    !(target as Obj_AI_Hero).HasBuff("BlitzcrankManaBarrierCD"))
                {
                    healthDebuffer += target.Mana / 2;
                }
            }

            return RendDamage - (target.PhysicalShield + target.HPRegenRate) - healthDebuffer;
        }
    }
}