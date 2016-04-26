using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using SharpDX;
using Geometry = ExorAIO.Utilities.Geometry;

namespace ExorAIO.Champions.KogMaw
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Clear(EventArgs args)
        {
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The JungleClear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.JungleMinions.Any() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededQMana &&
                Vars.Menu["spells"]["q"]["jungleclear"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast(Targets.JungleMinions[0].Position);
            }

            /// <summary>
            ///     The Clear W Logic.
            /// </summary>
            if (Vars.W.IsReady() && GameObjects.Player.ManaPercent > ManaManager.NeededWMana &&
                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuBool>().Value)
            {
                if (Items.HasItem(3085) ||
                    !Targets.Minions.Any(x => x.IsValidTarget(Vars.W.Range)) &&
                    Targets.JungleMinions.Any(x => x.IsValidTarget(Vars.W.Range)))
                {
                    Vars.W.Cast();
                }
            }

            /// <summary>
            ///     The Clear E Logics.
            /// </summary>
            if (Vars.E.IsReady() && GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                Vars.Menu["spells"]["e"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The JungleClear E Logic.
                /// </summary>
                if (Targets.JungleMinions.Any())
                {
                    Vars.E.Cast(Targets.JungleMinions[0].Position);
                }

                /// <summary>
                ///     The LaneClear E Logics.
                /// </summary>
                else
                {
                    /// <summary>
                    ///     The Aggressive LaneClear E Logic.
                    /// </summary>
                    if (GameObjects.EnemyHeroes.Any(t => !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.E.Range)))
                    {
                        if (Vars.E.GetLineFarmLocation(Targets.Minions, Vars.E.Width).MinionsHit >= 3 &&
                            !new Geometry.Rectangle(
                                GameObjects.Player.ServerPosition,
                                GameObjects.Player.ServerPosition.Extend(
                                    Targets.Minions[0].ServerPosition, Vars.E.Range), Vars.E.Width).IsOutside(
                                        (Vector2)
                                            Vars.E.GetPrediction(
                                                GameObjects.EnemyHeroes.FirstOrDefault(
                                                    t => !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.E.Range)))
                                                .CastPosition))
                        {
                            Vars.Q.Cast(Vars.E.GetLineFarmLocation(Targets.Minions, Vars.E.Width).Position);
                        }
                    }

                    /// <summary>
                    ///     The LaneClear E Logic.
                    /// </summary>
                    else if (
                        !GameObjects.EnemyHeroes.Any(
                            t => !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.E.Range + 100f)))
                    {
                        if (Vars.E.GetLineFarmLocation(Targets.Minions, Vars.E.Width).MinionsHit >= 3)
                        {
                            Vars.E.Cast(Vars.E.GetLineFarmLocation(Targets.Minions, Vars.E.Width).Position);
                        }
                    }
                }
            }
        }
    }
}