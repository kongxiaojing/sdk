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
                if (Vars.Menu["drawings"]["qc"].GetValue<MenuBool>().Value)
                {
                    if (!GameObjects.EnemyHeroes.Any(
                        t =>
                            !Invulnerable.Check(t) &&
                            !t.IsValidTarget(Vars.Q.Range) &&
                            t.IsValidTarget(Vars.Q2.Range-50f) &&
                            Vars.Menu["spells"]["q"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value))
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
                            450f);

                        polygon.Draw(!polygon.IsOutside((Vector2)Vars.Q2.GetPrediction(GameObjects.EnemyHeroes.FirstOrDefault(
                            t =>
                                !Invulnerable.Check(t) &&
                                !t.IsValidTarget(Vars.Q.Range) &&
                                t.IsValidTarget(Vars.Q2.Range-50f) &&
                                Vars.Menu["spells"]["q"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value)).CastPosition)
                            ? Color.Green
                            : Color.Red);
                    }
                }
            };
        }
    }
}