using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Cassiopeia
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
                Targets.Target.HasBuffOfType(BuffType.Poison) &&
                Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                DelayAction.Add(
                    Vars.Menu["spells"]["e"]["delay"].GetValue<MenuSlider>().Value, () =>
                {
                    Vars.E.CastOnUnit(Targets.Target);
                });
            }

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                Vars.Menu["spells"]["r"]["combo"].GetValue<MenuSliderButton>().BValue &&
                Vars.Menu["spells"]["r"]["enemies"].GetValue<MenuSliderButton>().SValue <=
                    Targets.RTargets.Count())
            {
                Vars.R.Cast(Targets.RTargets[0].ServerPosition);
            }

            if (Targets.Target.HasBuffOfType(BuffType.Poison))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Target.IsValidTarget(Vars.Q.Range) &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).CastPosition);
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            DelayAction.Add(1000, () =>
            {
                if (Vars.W.IsReady() &&
                    !Vars.Q.IsReady() &&
                    Targets.Target.IsValidTarget(Vars.W.Range) &&
                    Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
                {
                    Vars.W.Cast(Vars.W.GetPrediction(Targets.Target).CastPosition);
                }
            });
        }
    }
}