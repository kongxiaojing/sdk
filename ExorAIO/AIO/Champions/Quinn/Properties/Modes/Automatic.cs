using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.UI;

namespace ExorAIO.Champions.Quinn
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
            /// <summary>
            ///     Block Attacks while in R stance.
            /// </summary>
            Variables.Orbwalker.SetAttackState(!Vars.R.Instance.Name.Equals("QuinnRFinale"));

            /// <summary>
            ///     The Focus Logic (Passive Mark).
            /// </summary>
            foreach (var target in GameObjects.EnemyHeroes.Where(
                t =>
                    t.HasBuff("quinnw") &&
                    t.IsValidTarget(Vars.AARange)))
            {
                Variables.Orbwalker.ForceTarget = target;
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["manamanager"]) &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var enemy in GameObjects.EnemyHeroes.Where(
                    x =>
                        !x.IsDead &&
                        !x.IsVisible &&
                        x.Distance(GameObjects.Player.ServerPosition) < Vars.W.Range))
                {
                    Vars.W.Cast();
                }

                if (Vars.Locations.Any(h => GameObjects.Player.Distance(h) < Vars.W.Range))
                {
                    Vars.W.Cast();
                }
            }

            /// <summary>
            ///     The Automatic R Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                GameObjects.Player.InFountain() &&
                Vars.R.Instance.Name.Equals("QuinnR"))
            {
                Vars.R.Cast();
            }
        }
    }
}