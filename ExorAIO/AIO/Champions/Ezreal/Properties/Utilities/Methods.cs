using LeagueSharp;

namespace ExorAIO.Champions.Ezreal
{
    /// <summary>
    ///     The methods class.
    /// </summary>
    internal class Methods
    {
        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Ezreal.OnUpdate;
            Obj_AI_Base.OnDoCast += Ezreal.OnDoCast;
            Obj_AI_Base.OnBuffAdd += Ezreal.OnBuffAdd;
        }
    }
}