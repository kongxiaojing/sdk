using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;

namespace AsunaCondemn
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    public class Logics
    {
        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Automatic(EventArgs args)
        {
            /// <summary>
            ///     The fixed Condem Logic Kappa.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Flash.IsReady() &&
                Vars.Menu["e"]["logical"].GetValue<MenuKeyBind>().Active)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        t.IsValidTarget(Vars.E.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        !t.IsValidTarget(GameObjects.Player.BoundingRadius) &&
                        GameObjects.Player.Distance(GameObjects.Player.ServerPosition.Extend(t.ServerPosition, Vars.Flash.Range)) >
                            GameObjects.Player.Distance(t) + t.BoundingRadius))
                {
                    for (var i = 1; i < 10; i++)
                    {
                        if ((!GameObjects.Player.IsDashing()
                                ? (target.ServerPosition - Vector3.Normalize(target.ServerPosition - GameObjects.Player.ServerPosition) * (float)(i * 42.5)).IsWall()
                                : true) &&
                            (Vars.E.GetPrediction(target).UnitPosition - Vector3.Normalize(target.ServerPosition - GameObjects.Player.ServerPosition) * (float)(i * 42.5)).IsWall() &&

                            (!GameObjects.Player.IsDashing()
                                ? (target.ServerPosition - Vector3.Normalize(target.ServerPosition - GameObjects.Player.ServerPosition) * i * 44).IsWall()
                                : true) &&
                            (Vars.E.GetPrediction(target).UnitPosition - Vector3.Normalize(target.ServerPosition - GameObjects.Player.ServerPosition) * i * 44).IsWall())
                        {
                            Vars.E.CastOnUnit(target);
                        }
                    }
                }
            }
        }
    }
}