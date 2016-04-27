using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using Geometry = ExorAIO.Utilities.Geometry;

namespace ExorAIO.Champions.Lux
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
            if (!Targets.Target.IsValidTarget() ||
                Invulnerable.Check(Targets.Target) ||
                Targets.Target.HasBuff("luxilluminatingfraulein"))
            {
                return;
            }

            /// <summary>
            ///     The Q Harass Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededQMana &&
                Vars.Menu["spells"]["q"]["clear"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.EnemyHeroes.Any(
                    t =>
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.Q.Range)))
                {
                    if (Vars.Q.GetLineFarmLocation(Targets.Minions, Vars.Q.Width).MinionsHit <= 1 &&
                        !new Geometry.Rectangle(
                            GameObjects.Player.ServerPosition,
                            GameObjects.Player.ServerPosition.Extend(
                                Targets.Minions[0].ServerPosition, Vars.Q.Range),
                                Vars.Q.Width).IsOutside(
                                    (Vector2)Vars.Q.GetPrediction(GameObjects.EnemyHeroes.FirstOrDefault(
                                        t => 
                                            !Invulnerable.Check(t) &&
                                            t.IsValidTarget(Vars.Q.Range))).UnitPosition))
                    {
                        Vars.Q.Cast(Vars.Q.GetLineFarmLocation(Targets.Minions, Vars.Q.Width).Position);
                    }
                }
            }

            /// <summary>
            ///     The E Harass Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.Target.IsValidTarget(Vars.E.Range) &&
                GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                GameObjects.Player.Spellbook.GetSpell(SpellSlot.E).ToggleState == 1 &&
                Vars.Menu["spells"]["e"]["harass"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(Vars.E.GetPrediction(Targets.Target).CastPosition);
            }
        }
    }
}