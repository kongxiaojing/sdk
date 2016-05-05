using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Ezreal
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
            /// <summary>
            ///     The R Logics.
            /// </summary>
            if (Vars.R.IsReady() &&
                GameObjects.Player.CountEnemyHeroesInRange(Vars.AARange) == 0)
            {
                /// <summary>
                ///     The R Combo Logic.
                /// </summary>
                if (Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value &&
                    !Vars.Menu["spells"]["r"]["aoe"].GetValue<MenuSliderButton>().BValue)
                {
                    foreach (var target in GameObjects.EnemyHeroes.Where(
                        t =>
                            !Invulnerable.Check(t) &&
                            t.IsValidTarget(2000f) &&
                            Vars.Menu["spells"]["r"]["whitelist2"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value))
                    {
                        Vars.R.Cast(Vars.R.GetPrediction(target).UnitPosition);
                        return;
                    }
                }

                /// <summary>
                ///     The Automatic R Logic.
                /// </summary>
                if (Vars.Menu["spells"]["r"]["logical"].GetValue<MenuBool>().Value)
                {
                    if (!Targets.Target.IsValidTarget() &&
                        Bools.IsImmobile(Targets.Target) &&
                        !Invulnerable.Check(Targets.Target))
                    {
                        Vars.R.Cast(Targets.Target.ServerPosition);
                        return;
                    }
                }

                /// <summary>
                ///     The AoE R Logic.
                /// </summary>
                if (Vars.Menu["spells"]["r"]["aoe"].GetValue<MenuSliderButton>().BValue)
                {
                    Vars.R.CastIfWillHit(Targets.Target, Vars.Menu["spells"]["r"]["aoe"].GetValue<MenuSliderButton>().SValue);
                }
            }

            if (Bools.HasSheenBuff() ||
                !Targets.Target.IsValidTarget() ||
                Invulnerable.Check(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Target.IsValidTarget(Vars.Q.Range) &&
                !Targets.Target.IsValidTarget(Vars.AARange) &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!Vars.Q.GetPrediction(Targets.Target).CollisionObjects.Any(c => Targets.Minions.Contains(c)))
                {
                    Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).UnitPosition);
                    return;
                }
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Targets.Target.IsValidTarget(Vars.W.Range) &&
                !Targets.Target.IsValidTarget(Vars.AARange) &&
                Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                if (Targets.Target.IsValidTarget(Vars.AARange) &&
                    GameObjects.Player.CountAllyHeroesInRange(Vars.W.Range) < 2)
                {
                    Vars.W.Cast(Vars.W.GetPrediction(Targets.Target).UnitPosition);
                }
                else if (GameObjects.Player.TotalMagicalDamage > GameObjects.Player.TotalAttackDamage)
                {
                    Vars.W.Cast(Vars.W.GetPrediction(Targets.Target).UnitPosition);
                }
                return;
            }
        }
    }
}