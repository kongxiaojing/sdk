using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;
using SharpDX;

namespace ExorAIO.Utilities
{
    /// <summary>
    ///     The Bools class.
    /// </summary>
    internal class Bools
    {
        /// <summary>
        ///     Gets a value indicating whether the target has protection or not.
        /// </summary>
        public static bool HasAnyImmunity(Obj_AI_Hero unit, bool includeSpellShields = false)
            =>
                unit.IsInvulnerable ||
                unit.HasBuffOfType(BuffType.SpellImmunity) ||
                (includeSpellShields && unit.HasBuffOfType(BuffType.SpellShield));

        /// <summary>
        ///     Gets a value indicating whether the player has a sheen-like buff.
        /// </summary>
        public static bool HasSheenBuff()
            =>
                GameObjects.Player.HasBuff("Sheen") ||
                GameObjects.Player.HasBuff("LichBane") ||
                GameObjects.Player.HasBuff("ItemFrozenFist");

        /// <summary>
        ///     Gets a value indicating whether a determined champion can move or not.
        /// </summary>
        public static bool IsImmobile(Obj_AI_Base target)
        {
            if (target is Obj_AI_Minion ||
                target is Obj_AI_Turret)
            {
                return target.HasBuff("teleport_target");
            }
            else if (target is Obj_AI_Hero)
            {
                return 
                    target.HasBuff("pantheon_grandskyfall_jump") ||
                    target.HasBuff("rebirth") ||
                    target.HasBuff("zhonyasringshield") ||
                    target.MoveSpeed < 50 ||
                    (target as Obj_AI_Hero).IsRecalling() ||
                    (target as Obj_AI_Hero).IsCastingInterruptableSpell() ||
                    target.HasBuffOfType(BuffType.Stun) ||
                    target.HasBuffOfType(BuffType.Flee) ||
                    target.HasBuffOfType(BuffType.Sleep) ||
                    target.HasBuffOfType(BuffType.Snare) ||
                    target.HasBuffOfType(BuffType.Taunt) ||
                    target.HasBuffOfType(BuffType.Charm) ||
                    target.HasBuffOfType(BuffType.Knockup) ||
                    target.HasBuffOfType(BuffType.Suppression);
            }
            
            return false;
        }

        /// <summary>
        ///     Gets a value indicating whether a determined champion has a stackable item.
        /// </summary>
        public static bool HasTear(Obj_AI_Hero target)
            =>
                target.InventoryItems.Any(
                    item =>
                        item.Id.Equals(ItemId.Tear_of_the_Goddess) ||
                        item.Id.Equals(ItemId.Archangels_Staff) ||
                        item.Id.Equals(ItemId.Manamune) ||
                        item.Id.Equals(ItemId.Tear_of_the_Goddess_Crystal_Scar) ||
                        item.Id.Equals(ItemId.Archangels_Staff_Crystal_Scar) ||
                        item.Id.Equals(ItemId.Manamune_Crystal_Scar));

        /// <summary>
        ///     Gets a value indicating whether a determined root is worth cleansing.
        /// </summary>
        public static bool IsValidSnare()
            =>
                GameObjects.Player.Buffs.Any(
                    b =>
                        b.Type == BuffType.Snare &&
                        !Vars.InvalidSnareCasters.Contains((b.Caster as Obj_AI_Hero).ChampionName));

        /// <summary>
        ///     Gets a value indicating whether a determined Stun is worth cleansing.
        /// </summary>
        public static bool IsValidStun()
            =>
                GameObjects.Player.Buffs.Any(
                    b =>
                        b.Type == BuffType.Stun &&
                        !Vars.InvalidStunCasters.Contains((b.Caster as Obj_AI_Hero).ChampionName));

        /// <summary>
        ///     Gets a value indicating whether BuffType is worth cleansing.
        /// </summary>
        public static bool ShouldCleanse(Obj_AI_Hero target)
            =>
                !HasAnyImmunity(GameObjects.Player, true) &&
                GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(1500f)) &&
                (
                    target.HasBuffOfType(BuffType.Flee) ||
                    target.HasBuffOfType(BuffType.Charm) ||
                    target.HasBuffOfType(BuffType.Taunt) ||
                    target.HasBuffOfType(BuffType.Knockup) ||
                    target.HasBuffOfType(BuffType.Knockback) ||
                    target.HasBuffOfType(BuffType.Polymorph) ||
                    target.HasBuffOfType(BuffType.Suppression)
                );

        /// <summary>
        ///     Defines whether the player has a deadly mark.
        /// </summary>
        public static bool HasDeadlyMark()
            =>
                !HasAnyImmunity(GameObjects.Player) &&
                GameObjects.Player.HasBuff("zedrtargetmark") ||
                GameObjects.Player.HasBuff("summonerexhaust") ||
                GameObjects.Player.HasBuff("fizzmarinerdoombomb") ||
                GameObjects.Player.HasBuff("vladimirhemoplague") ||
                GameObjects.Player.HasBuff("mordekaiserchildrenofthegrave");

        /// <summary>
        ///     Returns true if the target is a perfectly valid rend target.
        /// </summary>
        public static bool IsPerfectRendTarget(Obj_AI_Base target)
        {
            if (target is Obj_AI_Minion)
            {
                if (target.IsValidTarget(Vars.E.Range) &&
                    target.HasBuff("kalistaexpungemarker"))
                {
                    return true;
                }
            }
            else if (target is Obj_AI_Hero)
            {
                if (target.IsValidTarget(Vars.E.Range) &&
                    target.HasBuff("kalistaexpungemarker") &&
                    !Invulnerable.Check(target as Obj_AI_Hero))
                {
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        ///     Returns true if the target is inside a cone.
        /// </summary>
        public static bool IsInsideCone(Obj_AI_Hero enemy)
            =>
                (enemy.ServerPosition.ToVector2() - GameObjects.Player.ServerPosition.ToVector2())
                    .Distance(new Vector2()) < Vars.R.Range * Vars.R.Range &&

                (Vars.End.ToVector2() - GameObjects.Player.ServerPosition.ToVector2()
                    .Rotated(-70f * (float)Math.PI / 180 / 2))
                        .CrossProduct(enemy.ServerPosition.ToVector2() - GameObjects.Player.ServerPosition.ToVector2()) > 0 &&
                        
                (enemy.ServerPosition.ToVector2() - GameObjects.Player.ServerPosition.ToVector2())
                    .CrossProduct(Vars.End.ToVector2() - GameObjects.Player.ServerPosition.ToVector2()
                        .Rotated(-70f * (float)Math.PI / 180 / 2).Rotated(70f * (float)Math.PI / 180)) > 0;
    }
}