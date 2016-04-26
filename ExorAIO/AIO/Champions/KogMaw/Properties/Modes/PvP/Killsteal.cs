using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.Data.Enumerations;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.KogMaw
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
            ///     The KillSteal Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Vars.Menu["spells"]["q"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) && !t.IsValidTarget(Vars.AARange) &&
                            t.IsValidTarget(Vars.Q.Range - 100f) && t.Health < Vars.Q.GetDamage(t)))
                {
                    var pred = Vars.Q.GetPrediction(target, false, -1, CollisionableObjects.YasuoWall);
                    if (pred.Hitchance > Vars.Q.MinHitChance)
                    {
                        Vars.Q.Cast(pred.CastPosition);
                        return;
                    }
                }
            }

            /// <summary>
            ///     The KillSteal E Logic.
            /// </summary>
            if (Vars.E.IsReady() && Vars.Menu["spells"]["e"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) && !t.IsValidTarget(Vars.AARange) &&
                            t.IsValidTarget(Vars.E.Range - 100f) && t.Health < Vars.E.GetDamage(t)))
                {
                    Vars.E.Cast(Vars.E.GetPrediction(target).CastPosition);
                    return;
                }
            }

            /// <summary>
            ///     The KillSteal R Logic.
            /// </summary>
            if (Vars.R.IsReady() && Vars.Menu["killsteal"].GetValue<MenuBool>().Value &&
                Vars.Menu["spells"]["r"]["stacks"].GetValue<MenuSlider>().Value >
                GameObjects.Player.GetBuffCount("kogmawlivingartillerycost"))
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.R.Range) && !t.IsValidTarget(Vars.W.Range) &&
                            t.Health < Vars.R.GetDamage(t)))
                {
                    Vars.R.Cast(Vars.R.GetPrediction(target).CastPosition);
                }
            }
        }
    }
}