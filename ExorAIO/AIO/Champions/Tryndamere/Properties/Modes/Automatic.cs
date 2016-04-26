using System;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Tryndamere
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
            ///     The Lifesaver R Logic.
            /// </summary>
            /*if (Vars.R.IsReady() &&
                !Vars.Q.IsReady() &&
                GameObjects.Player.CountEnemyHeroesInRange(700f) > 0 &&
                HealthPrediction.GetHealthPrediction(LeagueSharp.SDK.GameObjects.Player, (int) (250 + Game.Ping/2f)) <=
                GameObjects.Player.MaxHealth/4 &&
                Vars.Menu["lifesaver"].GetValue<MenuBool>().Value)
            {
                Vars.R.Cast();
            }*/

            if (GameObjects.Player.IsRecalling()) {}

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            /*if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent >= 75 &&
                HealthPrediction.GetHealthPrediction(LeagueSharp.SDK.GameObjects.Player, (int) (250 + Game.Ping/2f)) <=
                GameObjects.Player.MaxHealth/2 &&
                Vars.Menu["logical"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast();
            }*/
        }
    }
}