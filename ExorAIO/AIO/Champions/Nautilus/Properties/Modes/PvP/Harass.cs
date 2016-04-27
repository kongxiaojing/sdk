using System;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Nautilus
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
            ///     The E Harass Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.Target.IsValidTarget(Vars.E.Range) &&
                GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                Vars.Menu["spells"]["e"]["harass"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast();
            }
        }
    }
}