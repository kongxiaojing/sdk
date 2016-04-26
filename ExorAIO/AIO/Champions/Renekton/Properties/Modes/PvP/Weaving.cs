using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Renekton
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
        public static void Weaving(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!Targets.Minions.Any() && args.Target is Obj_AI_Minion)
            {
                /// <summary>
                ///     The W JungleClear Logic.
                /// </summary>
                if (Vars.W.IsReady() && Vars.Menu["spells"]["w"]["jungleclear"].GetValue<MenuBool>().Value)
                {
                    Vars.W.Cast();
                }
            }
            else if (args.Target is Obj_AI_Hero && Bools.HasAnyImmunity(args.Target as Obj_AI_Hero))
            {
                /// <summary>
                ///     The W Weaving Logic.
                /// </summary>
                if (Vars.W.IsReady() && Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
                {
                    Vars.W.Cast();
                }
            }
        }
    }
}