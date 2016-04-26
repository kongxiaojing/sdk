using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using SharpDX;
using Geometry = ExorAIO.Utilities.Geometry;

namespace ExorAIO.Champions.Lucian
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
        public static void Harass(EventArgs args)
        {
            if (!Targets.Target.IsValidTarget())
            {
                return;
            }

            /// <summary>
            ///     The Q Harass Logic.
            /// </summary>
            if (Vars.Q.IsReady() && GameObjects.Player.ManaPercent > ManaManager.NeededQMana &&
                Vars.Menu["spells"]["q"]["harass"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     Through enemy minions.
                /// </summary>
                foreach (var minion 
                    in from minion in Targets.Minions
                        let polygon =
                            new Geometry.Rectangle(
                                GameObjects.Player.ServerPosition,
                                GameObjects.Player.ServerPosition.Extend(minion.ServerPosition, Vars.Q2.Range),
                                Vars.Q2.Width)
                        where
                            !polygon.IsOutside(
                                (Vector2)
                                    Vars.Q2.GetPrediction(
                                        GameObjects.EnemyHeroes.FirstOrDefault(
                                            t =>
                                                !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.Q2.Range) &&
                                                t.Health < Vars.Q.GetDamage(t))).CastPosition)
                        select minion)
                {
                    Vars.Q.CastOnUnit(minion);
                }
            }
        }
    }
}