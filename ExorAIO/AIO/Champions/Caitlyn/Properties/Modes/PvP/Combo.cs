using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Caitlyn
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
            if (Bools.HasSheenBuff() ||
                !Targets.Target.IsValidTarget() ||
                Bools.HasAnyImmunity(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent >= 20 &&
                Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            t.IsValidTarget(600f) &&
                            !Bools.HasAnyImmunity(t) &&
                            !t.HasBuff("caitlynyordletrapinternal")))
                {
                    if (!Vars.E.GetPrediction(target).CollisionObjects.Any(c => c is Obj_AI_Minion))
                    {
                        Vars.E.Cast(Vars.E.GetPrediction(target).UnitPosition);
                    }
                }
            }
        }
    }
}