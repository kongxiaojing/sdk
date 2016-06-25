using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using Geometry = ExorAIO.Utilities.Geometry;

namespace ExorAIO.Champions.MissFortune
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
        public static void Killsteal(EventArgs args)
        {
            /// <summary>
            ///     The Q Killsteal Logic.
            /// </summary>
            if (Vars.Q.IsReady())
            {
                /// <summary>
                ///     Normal Q KilLSteal Logic.
                /// </summary>
                if (Vars.Menu["spells"]["q"]["killsteal"].GetValue<MenuBool>().Value)
                {
                    foreach (var target in GameObjects.EnemyHeroes.Where(
                        t =>
                            !Invulnerable.Check(t) &&
                            t.IsValidTarget(Vars.Q.Range) &&
                            Vars.GetRealHealth(t) <
                                (float)GameObjects.Player.GetSpellDamage(t, SpellSlot.Q)))
                    {
                        Vars.Q.CastOnUnit(target);
                    }
                }

                if (!GameObjects.EnemyHeroes.Any(
                    t =>
                        !Invulnerable.Check(t) &&
                        !t.IsValidTarget(Vars.Q.Range) &&
                        t.IsValidTarget(Vars.Q2.Range-50f)))
                {
                    return;
                }

                /// <summary>
                ///     Extended Q KilLSteal Logic.
                /// </summary>
                if (Vars.Menu["spells"]["q"]["extended"]["exkillsteal"].GetValue<MenuBool>().Value)
                {
                    /// <summary>
                    ///     Through enemy minions.
                    /// </summary>
                    foreach (var minion 
                        in from minion
                        in Targets.Minions.Where(m => m.IsValidTarget(Vars.Q.Range))

                        let polygon = new Geometry.Sector(
                            (Vector2)minion.ServerPosition,
                            (Vector2)minion.ServerPosition.Extend(GameObjects.Player.ServerPosition, -(Vars.Q2.Range - Vars.Q.Range)),
                            40f * (float)Math.PI / 180f,
                            (Vars.Q2.Range - Vars.Q.Range)-50f)

                        where
                            !polygon.IsOutside((Vector2)GameObjects.EnemyHeroes.FirstOrDefault(
                            t =>
                                !Invulnerable.Check(t) &&
                                !t.IsValidTarget(Vars.Q.Range) &&
                                t.IsValidTarget(Vars.Q2.Range-50f) &&
                                (t.NetworkId == Vars.PassiveTargetNetworkId ||
                                    !Targets.Minions.Any(m => !polygon.IsOutside((Vector2)m.ServerPosition)))).ServerPosition) &&

                            !polygon.IsOutside((Vector2)Movement.GetPrediction(
                                GameObjects.EnemyHeroes.FirstOrDefault(
                                t =>
                                    !Invulnerable.Check(t) &&
                                    !t.IsValidTarget(Vars.Q.Range) &&
                                    t.IsValidTarget(Vars.Q2.Range-50f) &&
                                    (t.NetworkId == Vars.PassiveTargetNetworkId ||
                                        !Targets.Minions.Any(m => !polygon.IsOutside((Vector2)m.ServerPosition)))),

                                GameObjects.Player.Distance(GameObjects.EnemyHeroes.FirstOrDefault(
                                t =>
                                    !Invulnerable.Check(t) &&
                                    !t.IsValidTarget(Vars.Q.Range) &&
                                    t.IsValidTarget(Vars.Q2.Range-50f) &&
                                    (t.NetworkId == Vars.PassiveTargetNetworkId ||
                                        !Targets.Minions.Any(m => !polygon.IsOutside((Vector2)m.ServerPosition)))).ServerPosition) / Vars.Q.Speed + Vars.Q.Delay).UnitPosition)

                        select minion)
                    {
                        Vars.Q.CastOnUnit(minion);
                    }

                    /// <summary>
                    ///     Through enemy heroes.
                    /// </summary>
                    foreach (var target
                        in from target
                        in GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(Vars.Q.Range))

                        let polygon = new Geometry.Sector(
                            (Vector2)target.ServerPosition,
                            (Vector2)target.ServerPosition.Extend(GameObjects.Player.ServerPosition, -(Vars.Q2.Range - Vars.Q.Range)),
                            40f * (float)Math.PI / 180f,
                            (Vars.Q2.Range - Vars.Q.Range)-50f)

                        where
                            !polygon.IsOutside((Vector2)GameObjects.EnemyHeroes.FirstOrDefault(
                            t =>
                                !Invulnerable.Check(t) &&
                                !t.IsValidTarget(Vars.Q.Range) &&
                                t.IsValidTarget(Vars.Q2.Range-50f) &&
                                t.HasBuff("MissFortuneMark") ||
                                        !Targets.Minions.Any(m => !polygon.IsOutside((Vector2)m.ServerPosition))).ServerPosition) &&

                            !polygon.IsOutside((Vector2)Movement.GetPrediction(
                                GameObjects.EnemyHeroes.FirstOrDefault(
                                t =>
                                    !Invulnerable.Check(t) &&
                                    !t.IsValidTarget(Vars.Q.Range) &&
                                    t.IsValidTarget(Vars.Q2.Range-50f) &&
                                    t.HasBuff("MissFortuneMark") ||
                                        !Targets.Minions.Any(m => !polygon.IsOutside((Vector2)m.ServerPosition))),

                                GameObjects.Player.Distance(GameObjects.EnemyHeroes.FirstOrDefault(
                                t =>
                                    !Invulnerable.Check(t) &&
                                    !t.IsValidTarget(Vars.Q.Range) &&
                                    t.IsValidTarget(Vars.Q2.Range-50f) &&
                                    t.HasBuff("MissFortuneMark") ||
                                        !Targets.Minions.Any(m => !polygon.IsOutside((Vector2)m.ServerPosition))).ServerPosition) / Vars.Q.Speed + Vars.Q.Delay).UnitPosition)

                        select target)
                    {
                        Vars.Q.CastOnUnit(target);
                    }
                }
            }
        }
    }
}