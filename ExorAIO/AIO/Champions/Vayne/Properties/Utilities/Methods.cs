using LeagueSharp;
using LeagueSharp.SDKEx;

namespace ExorAIO.Champions.Vayne
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
            Game.OnUpdate += Vayne.OnUpdate;
            Obj_AI_Base.OnDoCast += Vayne.OnDoCast;
            Events.OnGapCloser += Vayne.OnGapCloser;
            Events.OnInterruptableTarget += Vayne.OnInterruptableTarget;
        }
    }
}