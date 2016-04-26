using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

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
            if (Vars.SoulBound == null)
            {
                Vars.SoulBound = GameObjects.AllyHeroes.Find(t => t.HasBuff("kalistacoopstrikeally"));
            }
            else
            {
                /// <summary>
                ///     The Automatic R Logic.
                /// </summary>
                if (Vars.R.IsReady() &&
                    Vars.SoulBound.CountEnemyHeroesInRange(800f) > 0 &&
                    Vars.SoulBound.IsValidTarget(Vars.R.Range, false) &&
                    Health.GetPrediction(Vars.SoulBound, (int)(1000 + Game.Ping/2f)) <= 0 &&
                    Vars.Menu["spells"]["r"]["lifesaver"].GetValue<MenuBool>().Value)
                {
                    Vars.R.Cast();
                }
            }

            /// <summary>
            ///     The Focus Logic (Passive Mark).
            /// </summary>
            foreach (var target in GameObjects.EnemyHeroes.Where(
                t =>
                    t.IsValidTarget(Vars.AARange) &&
                    t.HasBuff("kalistacoopstrikemarkally")))
            {
                Variables.Orbwalker.ForceTarget = target;
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                !GameObjects.Player.IsRecalling() &&
                Variables.Orbwalker.ActiveMode == OrbwalkingMode.None &&
                GameObjects.Player.CountEnemyHeroesInRange(1500f) == 0 &&
                GameObjects.Player.ManaPercent > ManaManager.NeededWMana &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var loc in Vars.Locations.Where(
                    l =>
                        GameObjects.Player.Distance(l) < Vars.W.Range &&
                        !GameObjects.AllyMinions.Any(
                            m =>
                                m.Distance(l) < 2000f &&
                                m.CharData.BaseSkinName.Equals("RobotBuddy"))))
                {
                    Vars.W.Cast(loc);
                }
            }

            /// <summary>
            ///     The Automatic E Logics.
            /// </summary>
            if (Vars.E.IsReady())
            {
                /// <summary>
                ///     The E Before death Logic.
                /// </summary>
                if (Health.GetPrediction(GameObjects.Player, (int)(1000 + Game.Ping/2f)) <= 0 &&
                    Vars.Menu["spells"]["e"]["ondeath"].GetValue<MenuBool>().Value)
                {
                    Vars.E.Cast();
                }

                /// <summary>
                ///     The E Minion Harass Logic.
                /// </summary>
                if (GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                    Vars.Menu["spells"]["e"]["harass"].GetValue<MenuBool>().Value)
                {
                    if (Targets.Minions.Any(
                            m =>
                                Bools.IsPerfectRendTarget(m) &&
                                m.Health < KillSteal.GetPerfectRendDamage(m)))
                    {
                        if (Targets.Harass.Count() > (Items.HasItem(3085) ? 1 : 0) &&
                            !Invulnerable.Check(Targets.Harass.FirstOrDefault(), DamageType.Physical, false) &&
                            Vars.Menu["spells"]["e"]["whitelist"][Targets.Harass.FirstOrDefault().ChampionName.ToLower()].GetValue<MenuBool>().Value)
                        {
                            Vars.E.Cast();
                        }
                        else if (Targets.Harass.Count() >= 2)
                        {
                            Vars.E.Cast();
                        }
                    }
                }

                /// <summary>
                ///     The E JungleClear Logic.
                /// </summary>
                if (Vars.Menu["spells"]["e"]["junglesteal"].GetValue<MenuBool>().Value)
                {
                    if (Targets.JungleMinions.Count(
                        m =>
                            Bools.IsPerfectRendTarget(m) &&
                            m.CharData.BaseSkinName.Contains("Mini") &&
                            m.Health < KillSteal.GetPerfectRendDamage(m)) != 1)
                    {
                        Vars.E.Cast();
                    }
                }
            }
        }
    }
}