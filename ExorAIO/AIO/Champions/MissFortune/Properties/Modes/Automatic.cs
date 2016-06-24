using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.MissFortune
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
            Variables.Orbwalker.SetAttackState(!GameObjects.Player.HasBuff("missfortunebulletsound"));
            Variables.Orbwalker.SetMovementState(!GameObjects.Player.HasBuff("missfortunebulletsound"));

            /// <summary>
            ///     The Semi-Automatic R Management.
            /// </summary>
            if (Vars.R.IsReady() &&
                Vars.Menu["spells"]["r"]["bool"].GetValue<MenuBool>().Value)
            {
                if (Targets.Target.IsValidTarget(Vars.R.Range) &&
                    !GameObjects.Player.HasBuff("missfortunebulletsound") &&
                    Vars.Menu["spells"]["r"]["key"].GetValue<MenuKeyBind>().Active)
                {
                    if (Vars.E.IsReady())
                    {
                        Vars.E.Cast(Vars.E.GetPrediction(Targets.Target).CastPosition);
                    }
                    Vars.R.Cast(Vars.R.GetPrediction(Targets.Target).UnitPosition);
                }
                else if (GameObjects.Player.HasBuff("missfortunebulletsound") &&
                    !Vars.Menu["spells"]["r"]["key"].GetValue<MenuKeyBind>().Active)
                {
                    Vars.R.Cast();
                }
            }
        }
    }
}