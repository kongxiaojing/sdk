using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using LeagueSharp.SDK.Enumerations;

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
        public static void Automatic(EventArgs args)
        {
            if (GameObjects.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Support Mode Option.
            /// </summary>
            if (Variables.Orbwalker.GetTarget() != null &&
                Variables.Orbwalker.GetTarget() is Obj_AI_Minion &&
                GameObjects.AllyHeroes.Any(a => a.Distance(GameObjects.Player) < 2500) &&
                Vars.Menu["miscellaneous"]["support"].GetValue<MenuBool>().Value)
            {
                Variables.Orbwalker.SetAttackState(
                    Variables.Orbwalker.ActiveMode != OrbwalkingMode.Hybrid &&
                    Variables.Orbwalker.ActiveMode != OrbwalkingMode.LaneClear);
            }
        }
    }
}