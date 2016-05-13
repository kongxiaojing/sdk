using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Karma
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Automatic(EventArgs args)
        {
            /// <summary>
            ///     The W Lifesaver Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Targets.Target.IsValidTarget(Vars.W.Range) &&
                !Invulnerable.Check(Targets.Target, DamageType.Magical, false) &&
                Vars.Menu["spells"]["w"]["lifesaver"].GetValue<MenuSliderButton>().BValue &&
                Vars.Menu["spells"]["w"]["lifesaver"].GetValue<MenuSliderButton>().SValue >
                    GameObjects.Player.HealthPercent)
            {
                if (Vars.Menu["spells"]["r"]["empw"].GetValue<MenuBool>().Value)
                {
                    Vars.R.Cast();
                }

                Vars.W.CastOnUnit(Targets.Target);
            }

            /// <summary>
            ///     The AoE E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                GameObjects.Player.CountAllyHeroesInRange(600f) >=
                    Vars.Menu["spells"]["e"]["engager"].GetValue<MenuSliderButton>().SValue + 1 &&
                Vars.Menu["spells"]["e"]["engager"].GetValue<MenuSliderButton>().BValue)
            {
                if (Vars.Menu["spells"]["r"]["empe"].GetValue<MenuBool>().Value)
                {
                    Vars.R.Cast();
                }

                Vars.E.CastOnUnit(GameObjects.Player);
            }
        }
    }
}