using System;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Tristana
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
                Invulnerable.Check(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.Target.IsValidTarget(Vars.E.Range) &&
                Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value &&
                Vars.Menu["spells"]["e"]["whitelist"][Targets.Target.ChampionName.ToLower()].GetValue<MenuBool>().Value)
            {
                Vars.E.CastOnUnit(Targets.Target);
            }
        }
    }
}