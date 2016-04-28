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
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                !GameObjects.Player.IsRecalling() &&
                !GameObjects.Player.IsUnderEnemyTurret() &&
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
                if (Targets.Minions.Any(
                    m =>
                        Bools.IsPerfectRendTarget(m) &&
                        m.Health < KillSteal.GetPerfectRendDamage(m)) &&
                    GameObjects.EnemyHeroes.Any(t => Bools.IsPerfectRendTarget(t)) &&
                    Vars.Menu["spells"]["e"]["harass"].GetValue<MenuBool>().Value)
                {
                    /// <summary>
                    ///     Check for Mana Manager if the killable minion is only one, else do not use it.)
                    /// </summary>
                    if (Targets.Minions.Count(
                        m =>
                            Bools.IsPerfectRendTarget(m) &&
                            m.Health < KillSteal.GetPerfectRendDamage(m)) == 1)
                    {
                        if (GameObjects.Player.ManaPercent < ManaManager.NeededEMana)
                        {
                            return;
                        }
                    }

                    /// <summary>
                    ///     Check for E Whitelist if the harassable target is only one, else do not use it.)
                    /// </summary>
                    if (GameObjects.EnemyHeroes.Count(t => Bools.IsPerfectRendTarget(t)) == 1)
                    {
                        if (!Vars.Menu["spells"]["e"]["whitelist"][GameObjects.EnemyHeroes.FirstOrDefault(t => Bools.IsPerfectRendTarget(t)).ChampionName.ToLower()].GetValue<MenuBool>().Value)
                        {
                            return;
                        }
                    }

                    /// <summary>
                    ///     Check for invulnerability through all the harassable targets.
                    /// </summary>
                    foreach (var target in GameObjects.EnemyHeroes.Where(t => Bools.IsPerfectRendTarget(t)))
                    {
                        if (Invulnerable.Check(target, DamageType.Physical, false))
                        {
                            return;
                        }
                    }

                    Vars.E.Cast();
                }

                /// <summary>
                ///     The E JungleClear Logic.
                /// </summary>
                if (Vars.Menu["spells"]["e"]["junglesteal"].GetValue<MenuBool>().Value)
                {
                    foreach (var minion in Targets.JungleMinions.Where(
                        m =>
                            Bools.IsPerfectRendTarget(m) &&
                            m.Health < KillSteal.GetPerfectRendDamage(m)))
                    {

                        Vars.E.Cast();
                    }
                }
            }
        }
    }
}