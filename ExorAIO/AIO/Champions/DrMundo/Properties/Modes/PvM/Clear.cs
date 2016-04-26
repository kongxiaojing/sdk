using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.DrMundo
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
            ///     The Q Clear Logics.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.HealthPercent > ManaManager.NeededQMana &&
                Vars.Menu["spells"]["q"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The Q LaneClear Logic.
                /// </summary>
                if (Targets.Minions.Any())
                {
                    foreach (var minion in Targets.Minions.Where(
                        m =>
                            m.Health < Vars.Q.GetDamage(m) &&
                            !Vars.Q.GetPrediction(m).CollisionObjects.Any(c => c is Obj_AI_Minion)))
                    {
                        Vars.Q.Cast(Vars.Q.GetPrediction(minion).UnitPosition);
                    }
                }

                /// <summary>
                ///     The Q JungleClear Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any())
                {
                    Vars.Q.Cast(Targets.JungleMinions[0].ServerPosition);
                }
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void JungleClear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (Variables.Orbwalker.GetTarget() as Obj_AI_Minion == null)
            {
                return;
            }

            /// <summary>
            ///     The E JungleClear Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuBool>().Value &&
                Targets.JungleMinions.Contains(Variables.Orbwalker.GetTarget() as Obj_AI_Minion))
            {
                Vars.E.Cast();
            }
        }
    }
}