using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

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
            ///     The Automatic W Logic. 
            /// </summary>
            if (Vars.W.IsReady() &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        t.IsValidTarget(Vars.W.Range) &&
						!Invulnerable.Check(t, DamageType.Magical, false)))
                {
                    if (!ObjectManager.Get<Obj_AI_Minion>().Any(
                        m =>
                            m.Distance(target.ServerPosition) < 100f &&
                            m.CharData.BaseSkinName.Equals("Caitlyntrap")))
                    {
                        Vars.W.Cast(target.ServerPosition);
                    }
                }
            }

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.CountEnemyHeroesInRange(Vars.AARange) < 2 &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.Q.Range)))
                {
                    if (target.HasBuff("caitlynyordletrapdebuff") ||
                        target.HasBuff("caitlynyordletrapinternal"))
                    {
                        Vars.Q.Cast(target.ServerPosition);
                    }
                }
            }

            /// <summary>
            ///     The Semi-Automatic R Management.
            /// </summary>
            if (Vars.R.IsReady() &&
                Vars.Menu["spells"]["r"]["bool"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(Vars.R.Range)) &&
                    Vars.Menu["spells"]["r"]["key"].GetValue<MenuKeyBind>().Active)
                {
                    Vars.R.CastOnUnit(
                        GameObjects.EnemyHeroes
                            .Where(t => t.IsValidTarget(Vars.R.Range))
                            .OrderBy(o => o.Health)
                            .LastOrDefault());
                }
            }
        }
    }
}