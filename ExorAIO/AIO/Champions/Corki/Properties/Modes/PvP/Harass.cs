using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Corki
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
            if (!Targets.Target.IsValidTarget() ||
                Bools.HasAnyImmunity(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The R Harass Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                Targets.Target.IsValidTarget(Vars.R.Range) &&
                GameObjects.Player.ManaPercent > ManaManager.NeededRMana &&
                Vars.Menu["spells"]["r"]["autoharass"].GetValue<MenuBool>().Value &&
                Vars.Menu["spells"]["r"]["whitelist"][Targets.Target.ChampionName.ToLower()].GetValue<MenuBool>().Value)
            {
                if (!Vars.R.GetPrediction(Targets.Target).CollisionObjects.Any(c => c is Obj_AI_Minion))
                {
                    Vars.R.Cast(Vars.R.GetPrediction(Targets.Target).UnitPosition);
                }
            }
        }
    }
}