using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

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
            ///     The Semi-Automatic R Management.
            /// </summary>
            if (Vars.R.IsReady() &&
                Vars.Menu["spells"]["r"]["bool"].GetValue<MenuBool>().Value)
            {
                if (!GameObjects.Player.HasBuff("AbsoluteZero") &&
                    GameObjects.Player.CountEnemyHeroesInRange(Vars.R.Range) > 0 &&
                    Vars.Menu["spells"]["r"]["key"].GetValue<MenuKeyBind>().Active)
                {
                    Vars.R.Cast();
                }

                if (GameObjects.Player.HasBuff("AbsoluteZero") &&
                    !Vars.Menu["spells"]["r"]["key"].GetValue<MenuKeyBind>().Active)
                {
                    Variables.Orbwalker.Move(Game.CursorPos);
                    Vars.R.Cast();
                }
            }

            /// <summary>
            ///     The JungleClear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["spells"]["q"]["jungleclear"].GetValue<MenuBool>().Value)
            {
                if (Targets.JungleMinions.Any())
                {
                    foreach (var minion in Targets.JungleMinions.Where(
                        m =>
                            m.IsValidTarget(Vars.Q.Range) &&
                            m.Health < Vars.Q.GetDamage(m)))
                    {
                        Vars.Q.CastOnUnit(minion);
                    }
                }
            }

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Minions.Any() &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.Player.Health + (30 + 45 * GameObjects.Player.Spellbook.GetSpell(SpellSlot.Q).Level) +
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
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.Player.ManaPercent < ManaManager.NeededWMana &&
                    !GameObjects.Player.Buffs.Any(b => b.Name.Equals("visionary")))
                {
                    return;
                }

                switch (Variables.Orbwalker.ActiveMode)
                {
                    case OrbwalkingMode.Combo:
                        if (GameObjects.AllyHeroes.Any(
                            h =>
                                !h.IsMe &&
                                h.IsValidTarget(Vars.W.Range, false)))
                        {
                            Vars.W.CastOnUnit(GameObjects.AllyHeroes.Where(
                                a =>
                                    Vars.Menu["spells"]["w"]["whitelist"][a.ChampionName.ToLower()].GetValue<MenuBool>().Value).OrderBy(
                                        o =>
                                            o.TotalAttackDamage).First());
                        }
                        else
                        {
                            if (Targets.Target.IsValidTarget())
                            {
                                Vars.W.CastOnUnit(GameObjects.Player);
                            }
                        }
                        break;
                        
                    case OrbwalkingMode.LaneClear:
                        if (Targets.Minions.Any() ||
                            Targets.JungleMinions.Any())
                        {
                            Vars.W.CastOnUnit(GameObjects.Player);
                        }
                        break;
                        
                    default:
                        if (Targets.Minions.Any() &&
                            GameObjects.AllyMinions.Any())
                        {
                            foreach (var minion in GameObjects.AllyMinions.Where(m => m.IsValidTarget(Vars.W.Range, false)))
                            {
                                if (minion.GetMinionType() == MinionTypes.Super ||
                                    minion.GetMinionType() == MinionTypes.Siege)
                                {
                                    Vars.W.CastOnUnit(minion);
                                }
                            }
                        }
                        else if (!GameObjects.AllyMinions.Any() &&
                            (Targets.Minions.Any() || Targets.JungleMinions.Any()))
                        {
                            Vars.W.CastOnUnit(GameObjects.Player);
                        }
                        break;
                }
            }
        }
    }
}