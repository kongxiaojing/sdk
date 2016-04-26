using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

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
            if (Bools.HasSheenBuff() ||
                !Targets.Target.IsValidTarget() ||
                Bools.HasAnyImmunity(Targets.Target))
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
                if (!Vars.Q.GetPrediction(Targets.Target).CollisionObjects.Any(c => c is Obj_AI_Minion))
                {
                    Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).UnitPosition);
                    return;
                }
            }

            if (Bools.HasAnyImmunity(Targets.Target, true))
            {
                return;
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

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                GameObjects.Player.CountEnemyHeroesInRange(Vars.AARange) == 0 &&
                Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        t.IsValidTarget(2000f) &&
                        Vars.Menu["spells"]["r"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value))
                {
                    Vars.R.Cast(Vars.R.GetPrediction(target).UnitPosition);
                }

                if (Bools.IsImmobile(Targets.Target))
                {
                    Vars.R.Cast(Vars.R.GetPrediction(Targets.Target).UnitPosition);
                }
                
                Vars.R.CastIfWillHit(Targets.Target, 2);
            }
        }
    }
}