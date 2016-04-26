using System;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Renekton
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
            if (GameObjects.Player.IsRecalling()) {}

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            /* if (Vars.Q.IsReady() &&
                !GameObjects.Player.UnderTurret() &&
                GameObjects.Player.ManaPercent >= 50 &&
                Vars.Menu["logical"].GetValue<MenuBool>().Value)
            {
                if (Variables.Orbwalker.ActiveMode == OrbwalkingMode.Combo ||
                    GameObjects.Player.HasBuff("RenektonPreExecute"))
                {
                    return;
                }

                if (GameObjects.EnemyMinions.Any(
                    t =>
                        Extensions.IsValidTarget(t, Vars.Q.Range) &&
                        (!Extensions.IsValidTarget(t, Vars.W.Range) || !Vars.W.IsReady())))
                {
                    Vars.Q.Cast();
                }
            }*/

            /// <summary>
            ///     The Automatic R Logic.
            /// </summary>
            /*if (Vars.R.IsReady() &&
                GameObjects.Player.CountEnemyHeroesInRange(700f) > 0 &&
                HealthPrediction.GetHealthPrediction(LeagueSharp.SDK.GameObjects.Player, (int) (250 + Game.Ping/2f)) <=
                GameObjects.Player.MaxHealth/4 &&
                Vars.Menu["lifesaver"].GetValue<MenuBool>().Value)
            {
                Vars.R.Cast();
            }*/
        }
    }
}