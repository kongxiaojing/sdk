using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Warwick
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
        public static void Clear(EventArgs args)
        {
            if (Bools.HasSheenBuff() || !(Variables.Orbwalker.GetTarget() as Obj_AI_Minion).IsValid())
            {
                return;
            }

            /// <summary>
            ///     The W Clear Logic.
            /// </summary>
            if (Vars.W.IsReady() && GameObjects.Player.IsWindingUp &&
                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast();
            }

            if (GameObjects.Player.IsWindingUp)
            {
                return;
            }

            /// <summary>
            ///     The Q Clear Logic.
            /// </summary>
            if (Vars.Q.IsReady() && GameObjects.Player.ManaPercent > ManaManager.NeededQMana &&
                GameObjects.Player.Health + Vars.Q.GetDamage(Variables.Orbwalker.GetTarget() as Obj_AI_Minion) * 0.8 <
                GameObjects.Player.MaxHealth && Vars.Menu["spells"]["q"]["clear"].GetValue<MenuBool>().Value)
            {
                Vars.Q.CastOnUnit(Variables.Orbwalker.GetTarget() as Obj_AI_Minion);
            }
        }
    }
}