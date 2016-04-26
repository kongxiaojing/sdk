using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Jax
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
            ///     The Automatic R Logic.
            /// </summary>
            /*if (Vars.R.IsReady() &&
                HealthPrediction.GetHealthPrediction(LeagueSharp.SDK.GameObjects.Player, (int) (250f + Game.Ping/2f)) <=
                GameObjects.Player.MaxHealth/2 &&
                Vars.Menu["lifesaver"].GetValue<MenuBool>().Value)
            {
                Vars.R.Cast();
            }*/

            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (Vars.E.IsReady() && !GameObjects.Player.IsUnderEnemyTurret() &&
                Vars.Menu["spells"]["e"]["auto"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(t => !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.E.Range)))
                {
                    Vars.E.Cast();
                }
            }
        }
    }
}