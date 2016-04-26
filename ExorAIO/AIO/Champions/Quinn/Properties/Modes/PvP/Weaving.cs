using ExorAIO.Utilities;
using LeagueSharp;

namespace ExorAIO.Champions.Quinn
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="(sender as Obj_AI_Hero)">The (sender as Obj_AI_Hero).</param>
        /// <param name="args">The args.</param>
        public static void Weaving(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(args.Target is Obj_AI_Hero) || Bools.HasAnyImmunity((Obj_AI_Hero) args.Target)) {}
        }
    }
}