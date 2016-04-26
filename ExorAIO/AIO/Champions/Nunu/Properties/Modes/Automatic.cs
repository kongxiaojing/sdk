using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Nunu
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
            ///     The JungleClear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Vars.Menu["spells"]["q"]["jungleclear"].GetValue<MenuBool>().Value)
            {
                if (Targets.JungleMinions.Any())
                {
                    foreach (var minion in
                        Targets.JungleMinions.Where(
                            m =>
                                m.IsValidTarget(Vars.Q.Range) && m.Health < Vars.Q.GetDamage(m) &&
                                !m.CharData.BaseSkinName.Contains("Mini")))
                    {
                        Vars.Q.CastOnUnit(minion);
                    }
                }
            }

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.Minions.Any() &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.Player.Health + 30 + 45 * GameObjects.Player.Spellbook.GetSpell(SpellSlot.Q).Level +
                    GameObjects.Player.TotalMagicalDamage * 0.75 < GameObjects.Player.MaxHealth)
                {
                    foreach (var minion in Targets.Minions.Where(m => m.IsValidTarget(Vars.Q.Range)))
                    {
                        Vars.Q.CastOnUnit(minion);
                    }
                }
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                (GameObjects.Player.ManaPercent > ManaManager.NeededWMana ||
                 GameObjects.Player.Buffs.Any(b => b.Name.Equals("visionary"))) &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.AllyHeroes.Any(h => !h.IsMe))
                {
                    foreach (var ally in
                        GameObjects.AllyHeroes.Where(
                            a =>
                                a.IsValidTarget(Vars.W.Range, false) &&
                                (GameObjects.EnemyMinions.Any(t => t.IsValidTarget(1200f)) || Targets.Minions.Any()) &&
                                Vars.Menu["spells"]["w"]["whitelist"]
                                    .GetValue<MenuBool>().Value).OrderBy(o => GameObjects.Player.TotalAttackDamage))
                    {
                        Vars.W.CastOnUnit(ally);
                    }
                }
                else if (Targets.Minions.Any() && GameObjects.AllyMinions.Any() &&
                         Variables.Orbwalker.ActiveMode != OrbwalkingMode.Combo)
                {
                    foreach (var minion in
                        GameObjects.AllyMinions.Where(
                            m =>
                                m.IsValidTarget(Vars.W.Range, false) &&
                                m.CharData.BaseSkinName.ToLower().Contains("super") ||
                                m.CharData.BaseSkinName.ToLower().Contains("siege")))
                    {
                        Vars.W.CastOnUnit(minion);
                    }
                }
                else
                {
                    Vars.W.CastOnUnit(GameObjects.Player);
                }
            }

            /// <summary>
            ///     The Semi-Automatic R Management.
            ///     Testare Semi-Auto R.
            /// </summary>
            if (Vars.R.IsReady() && Vars.Menu["spells"]["r"]["boolrsa"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.Player.CountEnemyHeroesInRange(Vars.R.Range) > 0 &&
                    Vars.Menu["spells"]["r"]["keyrsa"].GetValue<MenuKeyBind>().Active &&
                    !GameObjects.Player.HasBuff("AbsoluteZero"))
                {
                    Vars.R.Cast();
                }
                else if (!Vars.Menu["spells"]["r"]["keyrsa"].GetValue<MenuKeyBind>().Active &&
                         GameObjects.Player.HasBuff("AbsoluteZero"))
                {
                    GameObjects.Player.IssueOrder(GameObjectOrder.MoveTo, GameObjects.Player.Position);
                    Vars.R.Cast();
                }
            }
        }
    }
}