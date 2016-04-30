using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Ezreal
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
            if (GameObjects.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Q LastHit Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededQMana &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var minion in Targets.Minions.Where(
                    m =>
                        !m.IsValidTarget(Vars.AARange) &&
                        m.Health < Vars.Q.GetDamage(m) &&
                        m.Health > GameObjects.Player.GetAutoAttackDamage(m)).OrderBy(
                            o =>
                                o.MaxHealth))
                {
                    if (!Vars.Q.GetPrediction(minion).CollisionObjects.Any(c => c is Obj_AI_Minion))
                    {
                        Vars.Q.Cast(Vars.Q.GetPrediction(minion).UnitPosition);
                    }
                }
            }

            /// <summary>
            ///     The Tear Stacking Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Bools.HasTear(GameObjects.Player) &&
                Variables.Orbwalker.ActiveMode == OrbwalkingMode.None &&
                GameObjects.Player.CountEnemyHeroesInRange(1500) == 0 &&
                GameObjects.Player.ManaPercent > ManaManager.NeededTearMana &&
                Vars.Menu["miscellaneous"]["tear"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast(Game.CursorPos);
            }

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Variables.Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.Combo:
                    if (Variables.Orbwalker.GetTarget() as Obj_AI_Hero == null)
                    {
                        return;
                    }
                    break;

                case OrbwalkingMode.LaneClear:
                    if (Variables.Orbwalker.GetTarget() as Obj_HQ == null &&
                        Variables.Orbwalker.GetTarget() as Obj_AI_Turret  == null &&
                        Variables.Orbwalker.GetTarget() as Obj_BarracksDampener == null)
                    {
                        return;
                    }
                    break;

                default:
                    return;
                    break;
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                ObjectManager.Player.ManaPercent < ManaManager.NeededWMana &&
                GameObjects.Player.TotalMagicalDamage < GameObjects.Player.TotalAttackDamage &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.AllyHeroes.Where(
                    t =>
                        !t.IsMe &&
                        t.IsWindingUp &&
                        t.IsValidTarget(Vars.W.Range, false)))
                {
                    Vars.W.Cast(Vars.W.GetPrediction(target).UnitPosition);
                }
            }
        }
    }
}