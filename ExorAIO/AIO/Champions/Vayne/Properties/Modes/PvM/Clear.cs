using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Vayne
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void Clear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            ///     The Q Clear Logics.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededQMana)
            {
                /// <summary>
                ///     The Q FarmHelper Logic.
                /// </summary>
                if (Vars.Menu["spells"]["q"]["farmhelper"].GetValue<MenuBool>().Value)
                {
                    if (Targets.Minions.Any() &&
                        Targets.Minions.Count(
                            m =>
                                m.Health < GameObjects.Player.GetAutoAttackDamage(m) + Vars.Q.GetDamage(m)) > 1)
                    {
                        Vars.Q.Cast(Game.CursorPos);
                    }
                }
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void JungleClear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (Variables.Orbwalker.GetTarget() as Obj_AI_Minion == null)
            {
                return;
            }

            /// <summary>
            ///     The Q JungleClear Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededQMana &&
                Vars.Menu["spells"]["q"]["jungleclear"].GetValue<MenuBool>().Value &&
                Targets.JungleMinions.Contains(Variables.Orbwalker.GetTarget() as Obj_AI_Minion))
            {
                Vars.Q.Cast(Game.CursorPos);
            }
        }
    }
}