using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.UI;
using LeagueSharp.SDKEx.Utils;
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
            if (ObjectManager.Player.IsDashing())
            {
                return;
            }

            /// <summary>
            ///     The fixed Condem Logic Kappa.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Flash.IsReady() &&
                Vars.Menu["e"]["logical"].GetValue<MenuKeyBind>().Active)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        !Invulnerable.Check(t) &&
                        !GameObjects.Player.IsDashing() &&
                        !t.IsValidTarget(ObjectManager.Player.BoundingRadius) &&
                        ObjectManager.Player.Distance(ObjectManager.Player.ServerPosition.Extend(t.ServerPosition, Vars.Flash.Range)) >
                            ObjectManager.Player.Distance(t) + t.BoundingRadius))
                {
                    for (var i = 1; i < 10; i++)
                    {
                        if ((target.ServerPosition - Vector3.Normalize(target.ServerPosition - ObjectManager.Player.ServerPosition)*i*42).IsWall() &&
                            (target.ServerPosition - Vector3.Normalize(target.ServerPosition - ObjectManager.Player.ServerPosition)*i*44).IsWall() &&
                            (Vars.E.GetPrediction(target).UnitPosition - Vector3.Normalize(target.ServerPosition - ObjectManager.Player.ServerPosition)*i*42).IsWall() &&
                            (Vars.E.GetPrediction(target).UnitPosition - Vector3.Normalize(target.ServerPosition - ObjectManager.Player.ServerPosition)*i*44).IsWall())
                        {
                            Vars.E.CastOnUnit(target);
                            Vars.Flash.Cast(ObjectManager.Player.ServerPosition.Extend(target.ServerPosition, Vars.Flash.Range));
                        }
                    }
                }
            }
        }
    }
}