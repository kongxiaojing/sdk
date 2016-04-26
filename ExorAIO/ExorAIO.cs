using ExorAIO.Core;
using ExorAIO.Utilities;

namespace ExorAIO
{
    /// <summary>
    ///     The AIO class.
    /// </summary>
    internal class AIO
    {
        /// <summary>
        ///     Loads the Assembly's core processes.
        /// </summary>
        public static void OnLoad()
        {
            /// <summary>
            ///     Loads the Main Menu.
            /// </summary>
            Vars.Menu.Attach();

            /// <summary>
            ///     Tries to load the current Champion.
            /// </summary>
            Bootstrap.LoadChampion();
        }
    }
}