using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.KogMaw
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
            if (Bools.HasSheenBuff() || !Targets.Target.IsValidTarget() || Bools.HasAnyImmunity(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.Target.IsValidTarget(Vars.Q.Range) &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!Vars.Q.GetPrediction(Targets.Target).CollisionObjects.Any())
                {
                    Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).CastPosition);
                }
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() && Targets.Target.IsValidTarget(Vars.W.Range) &&
                Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast();
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() && Targets.Target.IsValidTarget(Vars.E.Range - 100f) &&
                Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(Vars.E.GetPrediction(Targets.Target).CastPosition);
            }

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (Vars.R.IsReady() && Targets.Target.HealthPercent < 50 && Targets.Target.IsValidTarget(Vars.R.Range) &&
                Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!Vars.Menu["spells"]["miscellaneous"]["urf"].GetValue<MenuBool>().Value)
                {
                    if (GameObjects.Player.GetBuffCount("kogmawlivingartillerycost") >
                        Vars.Menu["spells"]["r"]["stacks"].GetValue<MenuSlider>().Value)
                    {
                        return;
                    }
                    if (GameObjects.Player.ManaPercent < ManaManager.NeededRMana)
                    {
                        return;
                    }
                }

                if (!Targets.Target.IsValidTarget(Vars.W.Range))
                {
                    Vars.R.Cast(Vars.R.GetPrediction(Targets.Target).CastPosition);
                }
                else if (Vars.W.IsReady() && !GameObjects.Player.HasBuff("KogMawBioArcaneBarrage"))
                {
                    Vars.R.Cast(Vars.R.GetPrediction(Targets.Target).CastPosition);
                }
            }
        }
    }
}