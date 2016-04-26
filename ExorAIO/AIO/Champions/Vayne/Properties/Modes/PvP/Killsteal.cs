using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Vayne
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
        public static void Killsteal(EventArgs args)
        {
            /// <summary>
            ///     The Q KillSteal Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                !GameObjects.Player.IsWindingUp &&
                Vars.Menu["spells"]["q"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        t.IsValidTarget(Vars.Q.Range) &&
                        !t.IsValidTarget(Vars.AARange) &&
                        t.CountEnemyHeroesInRange(700f) <= 2 &&
                        t.Health < Vars.Q.GetDamage(t) +
                            GameObjects.Player.GetAutoAttackDamage(t) +
                            (t.GetBuffCount("vaynesilvereddebuff") == 2
                                ? Vars.W.GetDamage(t)
                                : 0)))
                {
                    Vars.Q.Cast(target.ServerPosition);
                    Variables.Orbwalker.ForceTarget = target;
                }
            }

            /// <summary>
            ///     The E KillSteal Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                !GameObjects.Player.IsWindingUp &&
                Vars.Menu["spells"]["e"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        t.IsValidTarget(Vars.E.Range) &&
                        t.Health < Vars.E.GetDamage(t) +
                            (t.GetBuffCount("vaynesilvereddebuff") == 2
                                ? Vars.W.GetDamage(t)
                                : 0)))
                {
                    Vars.E.CastOnUnit(target);
                }
            }
        }
    }
}