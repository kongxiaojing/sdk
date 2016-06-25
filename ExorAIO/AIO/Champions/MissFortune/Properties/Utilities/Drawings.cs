using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using Geometry = ExorAIO.Utilities.Geometry;
using Color = System.Drawing.Color;

namespace ExorAIO.Champions.MissFortune
{
    /// <summary>
    ///     The prediction drawings class.
    /// </summary>
    internal class ConeDrawings
    {
        /// <summary>
        ///     Loads the range drawings.
        /// </summary>
        public static void Initialize()
        {
            if (GameObjects.Player.IsDead)
            {
                return;
            }

            Drawing.OnDraw += delegate
            {
                if (Vars.Q.IsReady() &&
                    Vars.Menu["drawings"]["qc"].GetValue<MenuBool>().Value)
                {
                    if (!GameObjects.EnemyHeroes.Any(
                        t =>
                            !Invulnerable.Check(t) &&
                            !t.IsValidTarget(Vars.Q.Range) &&
                            t.IsValidTarget(Vars.Q2.Range-50f)))
                    {
                        return;
                    }

                    foreach (var minion in Targets.Minions.Where(m => m.IsValidTarget(Vars.Q.Range)))
                    {
                        var polygon = new Geometry.Sector(
                            (Vector2)minion.ServerPosition,
                            (Vector2)minion.ServerPosition.Extend(GameObjects.Player.ServerPosition,
                            -(Vars.Q2.Range - Vars.Q.Range)),
                            40f * (float)Math.PI / 180f,
                            Vars.Q2.Range - Vars.Q.Range);

                        polygon.Draw(
                            !polygon.IsOutside((Vector2)GameObjects.EnemyHeroes.FirstOrDefault(
                            t =>
                                !Invulnerable.Check(t) &&
                                !t.IsValidTarget(Vars.Q.Range) &&
                                t.IsValidTarget(Vars.Q2.Range-50f)).ServerPosition) &&
                            !polygon.IsOutside((Vector2)Movement.GetPrediction(
                                GameObjects.EnemyHeroes.FirstOrDefault(
                                t =>
                                    !Invulnerable.Check(t) &&
                                    !t.IsValidTarget(Vars.Q.Range) &&
                                    t.IsValidTarget(Vars.Q2.Range-50f)),
                                GameObjects.Player.Distance(GameObjects.EnemyHeroes.FirstOrDefault(
                                t =>
                                    !Invulnerable.Check(t) &&
                                    !t.IsValidTarget(Vars.Q.Range) &&
                                    t.IsValidTarget(Vars.Q2.Range-50f))) / Vars.Q.Speed + Vars.Q.Delay).UnitPosition)

                            ? Color.Green
                            : Color.Red);
                    }
                }
            };
        }
    }
}