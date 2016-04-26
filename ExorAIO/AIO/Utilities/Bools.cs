using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;

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
            =>
                // To Do: Aatrox's Stasis.
                target.HasBuff("rebirth") ||
                target.HasBuff("teleport_target") ||
                target.HasBuff("zhonyasringshield") ||
                target.HasBuff("pantheon_grandskyfall_jump") ||
                (target as Obj_AI_Hero).MoveSpeed < 50 ||
                (target as Obj_AI_Hero).IsRecalling() ||
                (target as Obj_AI_Hero).IsCastingInterruptableSpell() ||
                (target as Obj_AI_Hero).HasBuffOfType(BuffType.Stun) ||
                (target as Obj_AI_Hero).HasBuffOfType(BuffType.Flee) ||
                (target as Obj_AI_Hero).HasBuffOfType(BuffType.Sleep) ||
                (target as Obj_AI_Hero).HasBuffOfType(BuffType.Snare) ||
                (target as Obj_AI_Hero).HasBuffOfType(BuffType.Taunt) ||
                (target as Obj_AI_Hero).HasBuffOfType(BuffType.Charm) ||
                (target as Obj_AI_Hero).HasBuffOfType(BuffType.Knockup) ||
                (target as Obj_AI_Hero).HasBuffOfType(BuffType.Suppression);

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
                !HasAnyImmunity(GameObjects.Player) &&
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
            =>
                target.IsValidTarget(Vars.E.Range) &&
                target.HasBuff("KalistaExpungeMarker") &&
                !HasAnyImmunity(target as Obj_AI_Hero);
    }
}