using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Jax
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Clear(EventArgs args) 
        {
            /// <summary>
            ///     The Q JungleGrab Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                !Targets.JungleMinions.Any(x => x.IsValidTarget(Vars.E.Range)) &&
                Vars.Menu["spells"]["q"]["junglegrab"].GetValue<MenuBool>().Value)
            {
                Vars.Q.CastOnUnit(Targets.JungleMinions[0]);
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void Clear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (Variables.Orbwalker.GetTarget() as Obj_AI_Minion == null)
            {
                return;
            }

            /// <summary>
            ///     The Clear W Logics.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededWMana &&
                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuBool>().Value)
            {
                if ((Variables.Orbwalker.GetTarget() as Obj_AI_Minion).Health <
                    Vars.W.GetDamage(Variables.Orbwalker.GetTarget() as Obj_AI_Minion) +
                    GameObjects.Player.GetAutoAttackDamage(Variables.Orbwalker.GetTarget() as Obj_AI_Minion) * 2)
                {
                    Vars.W.Cast();
                }
            }
        }
    }
}