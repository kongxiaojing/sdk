using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Jhin
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
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                Vars.R.Instance.Name.Equals("JhinRShot") &&
                Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value)
            {
                if (Targets.RTargets.Any())
                {
                    if (Vars.Menu["spells"]["r"]["nearmouse"].GetValue<MenuBool>().Value)
                    {
                        Vars.R.Cast(Vars.R.GetPrediction(Targets.RTargets.OrderBy(t => t.Distance(Game.CursorPos)).FirstOrDefault()).UnitPosition);
                        return;
                    }
                    else
                    {
                        Vars.R.Cast(Vars.R.GetPrediction(Targets.RTargets.FirstOrDefault()).UnitPosition);
                        return;
                    }
                }

                Vars.R.Cast(Game.CursorPos);
            }

            if (Invulnerable.Check(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Target.IsValidTarget(Vars.Q.Range) &&
                GameObjects.Player.HasBuff("JhinPassiveReload") &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.CastOnUnit(Targets.Target);
            }
        }
    }
}