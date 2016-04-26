using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using static ExorAIO.Utilities.Vars;

namespace ExorAIO.Champions.Kalista
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
            ///     The Soulbound declaration.
            /// </summary>
            if (SoulBound == null)
            {
                SoulBound = GameObjects.AllyHeroes.Find(t => t.HasBuff("kalistacoopstrikeally"));
            }

            /// <summary>
            ///     The Focus Logic (Passive Mark).
            /// </summary>
            foreach (var target in
                GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(AARange) && t.HasBuff("kalistacoopstrikemarkally")))
            {
                Variables.Orbwalker.ForceTarget = target;
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (W.IsReady() && !GameObjects.Player.IsRecalling() && Variables.Orbwalker.ActiveMode == OrbwalkingMode.None &&
                GameObjects.Player.CountEnemyHeroesInRange(1500f) == 0 &&
                GameObjects.Player.ManaPercent > ManaManager.NeededWMana &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var loc in
                    Locations.Where(
                        l =>
                            GameObjects.Player.Distance(l) < W.Range &&
                            !ObjectManager.Get<Obj_AI_Minion>()
                                .Any(
                                    m =>
                                        m.Distance(l) < 2000f &&
                                        m.CharData.BaseSkinName.Equals(
                                            "RobotBuddy", StringComparison.InvariantCultureIgnoreCase))))
                {
                    W.Cast(loc);
                }
            }

            /// <summary>
            ///     The Automatic E Logics.
            /// </summary>
            if (E.IsReady())
            {
                /// <summary>
                ///     The E Before death Logic.
                /// </summary>
                /*if (E.GetHealthPrediction(LeagueSharp.SDK.GameObjects.Player, (int) (1000 + Game.Ping/2f)) <= 0 &&
                    Vars.Menu[$"{MainMenuName}.espell.death"].GetValue<MenuBool>().Value)
                {
                    E.Cast();
                }*/

                /// <summary>
                ///     The E Minion Harass Logic.
                /// </summary>
                if (GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                    Vars.Menu["spells"]["r"]["harass"].GetValue<MenuBool>().Value)
                {
                    if (
                        Targets.Minions.Any(
                            m => Bools.IsPerfectRendTarget(m) && m.Health < KillSteal.GetPerfectRendDamage(m)))
                    {
                        if (Targets.Harass.Count() > (Items.HasItem(3085) ? 1 : 0) &&
                            Vars.Menu["spells"]["e"]["whitelist"]
                                .GetValue<MenuBool>().Value)
                        {
                            E.Cast();
                        }
                        else if (Targets.Harass.Count() >= 2)
                        {
                            E.Cast();
                        }
                    }
                }

                /// <summary>
                ///     The E JungleClear Logic.
                /// </summary>
                if (Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuBool>().Value)
                {
                    if (
                        Targets.JungleMinions.Count(
                            m =>
                                Bools.IsPerfectRendTarget(m) && m.CharData.BaseSkinName.Contains("Mini") &&
                                m.Health < KillSteal.GetPerfectRendDamage(m)) == 0)
                    {
                        E.Cast();
                    }
                    else if (
                        Targets.JungleMinions.Count(
                            m =>
                                Bools.IsPerfectRendTarget(m) && m.CharData.BaseSkinName.Contains("Mini") &&
                                m.Health < KillSteal.GetPerfectRendDamage(m)) == 2)
                    {
                        E.Cast();
                    }
                }
            }

            /// <summary>
            ///     The Automatic R Logic.
            /// </summary>
            /*if (R.IsReady() &&
                SoulBound.CountEnemyHeroesInRange(800f) > 0 &&
                Extensions.IsValidTarget(SoulBound, R.Range, false) &&
                HealthPrediction.GetHealthPrediction(SoulBound, (int) (1000 + Game.Ping/2f)) <= 0 &&
                Vars.Menu[$"{MainMenuName}.rspell.lifesaver"].GetValue<MenuBool>().Value)
            {
                R.Cast();
            }*/
        }
    }
}