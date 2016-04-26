using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Sivir
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Automatic(EventArgs args)
        {
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t => Bools.IsImmobile(t) && !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.Q.Range)))
                {
                    Vars.Q.Cast(Targets.Target.ServerPosition);
                }
            }
        }

        /// <summary>
        ///     Called while processing Spelaneclearlearast operations.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="GameObjectProcessSpellCastEventArgs" /> instance containing the event data.</param>
        public static void AutoShield(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (GameObjects.Player.IsInvulnerable || Bools.HasAnyImmunity(GameObjects.Player))
            {
                return;
            }

            if (sender.IsMe || sender == null || !sender.IsValid)
            {
                return;
            }

            /// <summary>
            ///     Special check for Kalista's E.
            /// </summary>
            if (args.SData.Name.Equals("KalistaExpungeWrapper"))
            {
                if (!GameObjects.Player.HasBuff("KalistaExpungeMarker"))
                {
                    return;
                }
            }
            else
            {
                if (args.Target == null)
                {
                    return;
                }

                if (!args.Target.IsMe)
                {
                    return;
                }

                /// <summary>
                ///     Block Dragon's AutoAttacks.
                /// </summary>
                if (sender is Obj_AI_Minion && sender.CharData.BaseSkinName.Equals("SRU_Dragon"))
                {
                    Vars.E.Cast();
                }

                if (!sender.IsEnemy || !(sender as Obj_AI_Hero).IsValidTarget())
                {
                    return;
                }

                /// <summary>
                ///     Special check for the AutoAttacks.
                /// </summary>
                if (AutoAttack.IsAutoAttack(args.SData.Name))
                {
                    if (!args.SData.Name.ToLower().Contains("red") && !args.SData.Name.ToLower().Contains("gold") &&
                        !args.SData.Name.ToLower().Contains("blue"))
                    {
                        if (!sender.IsMelee || !sender.Buffs.Any(b => AutoAttack.IsAutoAttackReset(args.SData.Name)))
                        {
                            return;
                        }
                    }

                    Console.WriteLine(args.SData.Name);
                }

                /// <summary>
                ///     Special check for the Located AoE skillshots.
                /// </summary>
                if (args.SData.TargettingType.Equals(SpellDataTargetType.LocationAoe))
                {
                    if (args.SData.Name.Equals("TormentedSoil") || args.SData.Name.Equals("MissFortuneScattershot"))
                    {
                        return;
                    }
                }

                /// <summary>
                ///     Special check for the on target-position AoE spells.
                /// </summary>
                if (args.SData.TargettingType.Equals(SpellDataTargetType.SelfAoe))
                {
                    if (!args.SData.Name.Equals("MockingShout"))
                    {
                        return;
                    }
                }
            }

            /// <summary>
            ///     If the sender is Zed and the processed arg is a Targetted spell (His Ultimate), delay the shieldcasting by 200ms.
            /// </summary>
            DelayAction.Add(
                args.Target.IsMe && sender.CharData.BaseSkinName.Equals("Zed") &&
                args.SData.TargettingType.Equals(SpellDataTargetType.Self)
                    ? 200
                    : 0, () => { Vars.E.Cast(); });
        }
    }
}