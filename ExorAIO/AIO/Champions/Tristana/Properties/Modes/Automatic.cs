using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.UI;

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
        public static void Automatic(EventArgs args)
        {
            /// <summary>
            ///     The Combo Focus Logic (E Charges).
            /// </summary>
            foreach (var target in ObjectManager.Get<Obj_AI_Base>().Where(
                t =>
                    t.HasBuff("TristanaECharge") &&
                    t.IsValidTarget(Vars.AARange)))
            {
                Variables.Orbwalker.ForceTarget = target;
            }

            if (Bools.HasSheenBuff() ||
                !(Variables.Orbwalker.GetTarget() as Obj_AI_Base).IsValidTarget())
            {
                return;
            }

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.IsWindingUp &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                if (!Vars.E.IsReady() ||
                    (Variables.Orbwalker.GetTarget() as Obj_AI_Base).HasBuff("TristanaECharge"))
                {
                    Vars.Q.Cast();
                }
            }
        }
    }
}