using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Ashe
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
                !Targets.Target.IsValidTarget())
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Target.IsValidTarget(Vars.AARange) &&
                GameObjects.Player.HasBuff("asheqcastready") &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast();
            }

            if (Invulnerable.Check(Targets.Target) ||
                Targets.Target.IsValidTarget(Vars.AARange))
            {
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Targets.Target.IsValidTarget(Vars.W.Range) &&
                Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!Vars.W.GetPrediction(Targets.Target).CollisionObjects.Any())
                {
                    Vars.W.Cast(Vars.W.GetPrediction(Targets.Target).UnitPosition);
                }
            }


            /// <summary>
            ///     The E -> R Combo Logics.
            /// </summary>
            if (Vars.R.IsReady() &&
                Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value &&
                Vars.Menu["spells"]["r"]["whitelist"][Targets.Target.ChampionName.ToLower()].GetValue<MenuBool>().Value)
            {
				if (!Vars.R.GetPrediction(Targets.Target).CollisionObjects.Any())
                {
					if (Vars.E.IsReady() &&
						!Targets.Target.IsValidTarget(Vars.W.Range) &&
						Vars.Menu["spells"]["e"]["logical"].GetValue<MenuBool>().Value)
					{
						Vars.E.Cast(Vars.E.GetPrediction(Targets.Target).UnitPosition);
					}

					Vars.R.Cast(Vars.R.GetPrediction(Targets.Target).UnitPosition);
				}
            }
        }
    }
}