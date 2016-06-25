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
        public static void Combo(EventArgs args)
        {
            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                !Bools.HasSheenBuff() &&
                Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                if (Variables.Orbwalker.GetTarget() as Obj_AI_Hero != null ||
                    GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(Vars.R.Range)) &&
                    Vars.Menu["spells"]["w"]["engager"].GetValue<MenuBool>().Value)
                {
                    Vars.W.Cast();
                }
            }

            if (!Targets.Target.IsValidTarget())
            {
                return;
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.Target.IsValidTarget(Vars.E.Range) &&
				!Invulnerable.Check(Targets.Target, DamageType.Magical, false) &&
                Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(Vars.E.GetPrediction(Targets.Target).CastPosition);
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
            ///     The Q Extended Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["spells"]["q"]["extended"]["excombo"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     Through enemy minions.
                /// </summary>
                foreach (var minion 
                    in from minion
                    in Targets.Minions.Where(
                        m =>
                            m.IsValidTarget(Vars.Q.Range) &&
                            Vars.Menu["spells"]["q"]["extended"]["excombokill"].GetValue<MenuBool>().Value
                                ? m.Health <
                                    (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)
                                : true)

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
                            (t.NetworkId == Vars.PassiveTarget.NetworkId ||
                                !Targets.Minions.Any(m => !polygon.IsOutside((Vector2)m.ServerPosition)))).ServerPosition) &&

                        !polygon.IsOutside((Vector2)Movement.GetPrediction(
                            GameObjects.EnemyHeroes.FirstOrDefault(
                            t =>
                                !Invulnerable.Check(t) &&
                                !t.IsValidTarget(Vars.Q.Range) &&
                                t.IsValidTarget(Vars.Q2.Range-50f) &&
                                (t.NetworkId == Vars.PassiveTarget.NetworkId ||
                                    !Targets.Minions.Any(m => !polygon.IsOutside((Vector2)m.ServerPosition)))),

                            GameObjects.Player.Distance(GameObjects.EnemyHeroes.FirstOrDefault(
                            t =>
                                !Invulnerable.Check(t) &&
                                !t.IsValidTarget(Vars.Q.Range) &&
                                t.IsValidTarget(Vars.Q2.Range-50f) &&
                                (t.NetworkId == Vars.PassiveTarget.NetworkId ||
                                    !Targets.Minions.Any(m => !polygon.IsOutside((Vector2)m.ServerPosition)))).ServerPosition) / Vars.Q.Speed + Vars.Q.Delay).UnitPosition)

                    select minion)
                {
                    Vars.Q.CastOnUnit(minion);
                }
            }
        }
    }
}