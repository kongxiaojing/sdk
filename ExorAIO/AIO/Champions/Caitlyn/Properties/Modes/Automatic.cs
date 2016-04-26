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
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) &&
                            t.IsValidTarget(Vars.Q.Range)))
                {
                    if (Bools.IsImmobile(target) ||
                        target.HasBuff("caitlynyordletrapdebuff") ||
                        target.HasBuff("caitlynyordletrapinternal"))
                    {
                        Vars.Q.Cast(Vars.Q.GetPrediction(target).UnitPosition);
                    }
                }
            }

            /// <summary>
            ///     The Automatic W Logic. 
            /// </summary>
            if (Vars.W.IsReady() &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            Bools.IsImmobile(t) &&
                            !Bools.HasAnyImmunity(t, true) &&
                            t.IsValidTarget(Vars.W.Range)))
                {
                    if (!GameObjects.Minions.Any(
                        m =>
                            m.Distance(target.ServerPosition) < 100f &&
                            m.CharData.BaseSkinName.Contains("Cupcake")))
                    {
                        Vars.W.Cast(target.ServerPosition);
                    }
                }
            }
        }
    }
}