using System;
using ExorAIO.Utilities;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.UI;
using LeagueSharp.SDKEx.Utils;

namespace ExorAIO.Champions.Pantheon
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
            if (!Targets.Target.IsValidTarget() ||
                Invulnerable.Check(Targets.Target))
            {
                return;
            }

            if (Bools.HasSheenBuff())
            {
                if (Targets.Target.IsValidTarget(Vars.AARange))
                {
                    return;
                }
            }

            /// <summary>
            ///     The Combo Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Target.IsValidTarget(Vars.Q.Range) &&
                !GameObjects.Player.HasBuff("pantheonesound") &&
                !GameObjects.Player.HasBuff("pantheonpassiveshield") &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.CastOnUnit(Targets.Target);
            }

            /// <summary>
            ///     The Combo W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Targets.Target.IsValidTarget(Vars.W.Range) &&
                !GameObjects.Player.HasBuff("pantheonesound") &&
                Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!Targets.Target.IsValidTarget(Vars.AARange) ||
                    !GameObjects.Player.HasBuff("pantheonpassiveshield") &&
                    GameObjects.Player.GetBuffCount("pantheonpassivecounter") < 3)
                {
                    Vars.W.CastOnUnit(Targets.Target);
                }
            }

            /// <summary>
            ///     The Combo E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.Target.IsValidTarget(Vars.E.Range) &&
                !GameObjects.Player.HasBuff("pantheonpassiveshield") &&
                Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(Targets.Target.ServerPosition);
            }
        }
    }
}