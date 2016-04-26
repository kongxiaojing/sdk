using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Olaf
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
            if (!(args.Target is Obj_AI_Hero) || Bools.HasAnyImmunity((Obj_AI_Hero) args.Target))
            {
                return;
            }

            /// <summary>
            ///     The E Weaving Logic.
            /// </summary>
            if (Vars.E.IsReady() && Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.E.CastOnUnit(args.Target as Obj_AI_Hero);
            }
        }
    }
}