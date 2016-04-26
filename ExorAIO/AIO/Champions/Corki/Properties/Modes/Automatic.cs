using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Enumerations;

namespace ExorAIO.Champions.Corki
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
            ///     The Automatic Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        !Bools.HasAnyImmunity(t) &&
                        t.IsValidTarget(Vars.Q.Range)))
                {
                    Vars.Q.Cast(Vars.Q.GetPrediction(target).CastPosition);
                }
            }

            /// <summary>
            ///     The Automatic R LastHit Logics.
            /// </summary>
            if (Vars.R.IsReady() &&
                Variables.Orbwalker.ActiveMode != OrbwalkingMode.Combo &&
                GameObjects.Player.ManaPercent > ManaManager.NeededRMana &&
                Vars.Menu["spells"]["r"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var minion in GameObjects.EnemyMinions.Where(
                    m =>
                        m.IsValidTarget(Vars.R.Range) &&
                        !m.IsValidTarget(Vars.AARange) &&
                        m.Health < Vars.R.GetDamage(m)))
                {
                    if (!Vars.R.GetPrediction(minion).CollisionObjects.Any(c => c is Obj_AI_Minion))
                    {
                        Vars.R.Cast(minion.ServerPosition);
                    }
                }
            }
        }
    }
}