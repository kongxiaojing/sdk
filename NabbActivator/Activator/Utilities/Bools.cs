using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;

namespace NabbActivator
{
    /// <summary>
    ///     The bools.
    /// </summary>
    internal class Bools
    {
        /// <summary>
        ///     Defines whether a Health Potion is running.
        /// </summary>
        public static bool IsHealthPotRunning()
        {
            return ObjectManager.Player.HasBuff("ItemCrystalFlask") ||
                   ObjectManager.Player.HasBuff("RegenerationPotion") ||
                   ObjectManager.Player.HasBuff("ItemMiniRegenPotion") ||
                   ObjectManager.Player.HasBuff("ItemDarkCrystalFlask") ||
                   ObjectManager.Player.HasBuff("ItemCrystalFlaskJungle");
        }

        /// <summary>
        ///     Defines whether a Mana Potion is running.
        /// </summary>
        public static bool IsManaPotRunning()
        {
            return ObjectManager.Player.HasBuff("ItemDarkCrystalFlask") ||
                   ObjectManager.Player.HasBuff("ItemCrystalFlaskJungle");
        }

        /// <summary>
        ///     Gets a value indicating whether the target has protection or not.
        /// </summary>
        /// <summary>
        ///     Gets a value indicating whether a determined root is worth cleansing.
        /// </summary>
        public static bool IsValidSnare()
        {
            return ObjectManager.Player.Buffs.Any(
                b =>
                    b.Type == BuffType.Snare &&
                    !Vars.InvalidSnareCasters.Contains((b.Caster as Obj_AI_Hero).ChampionName));
        }

        /// <summary>
        ///     Gets a value indicating whether a determined Stun is worth cleansing.
        /// </summary>
        public static bool IsValidStun()
        {
            return ObjectManager.Player.Buffs.Any(
                b =>
                    b.Type == BuffType.Stun &&
                    !Vars.InvalidStunCasters.Contains((b.Caster as Obj_AI_Hero).ChampionName));
        }

        /// <summary>
        ///     Gets a value indicating whether BuffType is worth cleansing.
        /// </summary>
        public static bool ShouldCleanse(Obj_AI_Hero target)
        {
            return !Invulnerable.Check(target) &&
                GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(1500f)) &&
                (
                   IsValidStun() ||
                   IsValidSnare() ||
                   target.HasBuffOfType(BuffType.Flee) ||
                   target.HasBuffOfType(BuffType.Charm) ||
                   target.HasBuffOfType(BuffType.Taunt) ||
                   target.HasBuffOfType(BuffType.Polymorph) ||
                   (ObjectManager.Player.HealthPercent < 40 &&
                    ObjectManager.Player.HasBuff("SummonerDot"))
                );
        }

        /// <summary>
        ///     Defines whether the player should use a cleanser.
        /// </summary>
        public static bool ShouldUseCleanser()
        {
            return !Invulnerable.Check(ObjectManager.Player) &&
                GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(1500f)) &&
                (
                   ObjectManager.Player.HasBuffOfType(BuffType.Suppression) ||
                   ObjectManager.Player.HasBuff("zedrtargetmark") ||
                   ObjectManager.Player.HasBuff("summonerexhaust") ||
                   ObjectManager.Player.HasBuff("fizzmarinerdoombomb") ||
                   ObjectManager.Player.HasBuff("vladimirhemoplague") ||
                   ObjectManager.Player.HasBuff("mordekaiserchildrenofthegrave")
                );
        }
    }
}