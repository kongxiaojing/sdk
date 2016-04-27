using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

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
        public static void Automatic(EventArgs args)
        {
            if (GameObjects.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.Q.Range)))
                {
                    if (!Vars.Q.GetPrediction(Targets.Target).CollisionObjects.Any(c => c is Obj_AI_Minion))
                    {
                        Vars.Q.Cast(target.ServerPosition);
                    }
                }
            }

            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Menu["spells"]["e"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            Bools.IsImmobile(t) &&
                            !Invulnerable.Check(t) &&
                            t.IsValidTarget(Vars.E.Range)))
                {
                    Vars.E.Cast(target.ServerPosition);
                }
            }

            /// <summary>
            ///     The Automatic R Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                
                GameObjects.Player.ManaPercent > ManaManager.NeededRMana &&
                Vars.Menu["spells"]["r"]["logical"].GetValue<MenuBool>().Value &&
                Vars.Menu["spells"]["r"]["stacks"].GetValue<MenuSlider>().Value >
                    GameObjects.Player.GetBuffCount("kogmawlivingartillerycost"))
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        t.HealthPercent < 50 &&
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.R.Range)))
                {
                    Vars.R.Cast(target.ServerPosition);
                }
            }
        }
    }
}