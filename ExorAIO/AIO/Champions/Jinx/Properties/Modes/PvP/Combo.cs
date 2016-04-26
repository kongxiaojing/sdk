using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Jinx
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
        public static void Combo(EventArgs args)
        {
            if (Bools.HasSheenBuff() || !Targets.Target.IsValidTarget() || Bools.HasAnyImmunity(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() && Targets.Target.IsValidTarget(Vars.W.Range) &&
                !GameObjects.EnemyMinions.Any(t => t.IsValidTarget(Vars.PowPow.Range)) &&
                Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!Vars.W.GetPrediction(Targets.Target).CollisionObjects.Any())
                {
                    Vars.W.Cast(Vars.W.GetPrediction(Targets.Target).CastPosition);
                }
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() && Targets.Target.IsValidTarget(Vars.E.Range) &&
                Targets.Target.CountEnemyHeroesInRange(Vars.E.Width) >= 2 &&
                Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(Targets.Target.Position);
            }

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (Vars.R.IsReady() && !GameObjects.EnemyMinions.Any(t => t.IsValidTarget(Vars.PowPow.Range)) &&
                Vars.Menu["combo"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.EnemyMinions.Count(t => t.HealthPercent < 50 && t.IsValidTarget(Vars.R.Range)) >= 2)
                {
                    Vars.R.CastIfWillHit(Targets.Target, 2);
                }
            }
        }
    }
}