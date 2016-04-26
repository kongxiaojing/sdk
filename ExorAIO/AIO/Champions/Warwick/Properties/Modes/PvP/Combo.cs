using System;
using ExorAIO.Utilities;
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
        public static void Combo(EventArgs args)
        {
            if (Bools.HasSheenBuff() || !Targets.Target.IsValidTarget() || Bools.HasAnyImmunity(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() && GameObjects.Player.IsWindingUp &&
                GameObjects.Player.ManaPercent > ManaManager.NeededWMana &&
                Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast();
            }

            if (GameObjects.Player.IsWindingUp)
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.Health + Vars.Q.GetDamage(Targets.Target) * 0.8 < GameObjects.Player.MaxHealth &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.CastOnUnit(Targets.Target);
            }

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (Vars.R.IsReady() && !Targets.Target.IsUnderEnemyTurret() && Targets.Target.IsValidTarget(Vars.R.Range) &&
                !Targets.Target.IsValidTarget(Vars.AARange) &&
                Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value &&
                Vars.Menu["spells"]["r"]["whitelist"]
                    .GetValue<MenuBool>().Value)
            {
                Vars.R.CastOnUnit(Targets.Target);
            }
        }
    }
}