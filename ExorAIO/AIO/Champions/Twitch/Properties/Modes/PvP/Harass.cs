using System;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Twitch
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
        public static void Harass(EventArgs args)
        {
            if (!Targets.Target.IsValidTarget() || Bools.HasAnyImmunity(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The W Harass Logic.
            /// </summary>
            if (Vars.W.IsReady() && Targets.Target.IsValidTarget(Vars.W.Range) &&
                GameObjects.Player.ManaPercent > ManaManager.NeededWMana &&
                Vars.Menu["spells"]["w"]["harass"].GetValue<MenuBool>().Value)
            {
                if (!GameObjects.Player.HasBuff("TwitchFullAutomatic") &&
                    Targets.Target.GetBuffCount("twitchdeadlyvenom") <= 4)
                {
                    Vars.W.Cast(Vars.W.GetPrediction(Targets.Target).CastPosition);
                }
            }
        }
    }
}