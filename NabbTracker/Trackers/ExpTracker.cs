using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using SharpDX;
using System.Drawing;

namespace NabbTracker
{
    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal class ExpTracker
    {
        /// <summary>
        ///     Loads the range drawings.
        /// </summary>
        public static void Initialize()
        {
            Drawing.OnDraw += delegate
            {
				foreach (var unit in GameObjects.Heroes.Where(
					e =>
						e.IsHPBarRendered &&
						(e.IsMe && Vars.Menu["exptracker"]["me"].GetValue<MenuBool>().Value ||
							e.IsEnemy && Vars.Menu["exptracker"]["enemies"].GetValue<MenuBool>().Value ||
							e.IsAlly && !e.IsMe && Vars.Menu["exptracker"]["allies"].GetValue<MenuBool>().Value)))
				{
					var actualExp = unit.Experience;
					var neededExp = 180 + (100 * unit.Level);

					Vars.ExpX = (int)unit.HPBarPosition.X + Vars.ExpXAdjustment(unit);
					Vars.ExpY = (int)unit.HPBarPosition.Y + Vars.ExpYAdjustment(unit);

					if (unit.Level > 1)
					{
						actualExp -= (280 + (80 + 100 * unit.Level))/2 * (unit.Level - 1);
					}

					var expPercent = (int)(actualExp/neededExp * 100);

					Drawing.DrawLine(
						Vars.ExpX - 77,
						Vars.ExpY + 20,
						Vars.ExpX + 55,
						Vars.ExpY + 20,
						7,
						Colors.Convert(System.Drawing.Color.Purple)
					);

					if (expPercent > 0)
					{
						Drawing.DrawLine(
							Vars.ExpX - 77,
							Vars.ExpY + 20,
							Vars.ExpX - 77 + (float)(1.32 * expPercent),
							Vars.ExpY + 20,
							7,
							Colors.Convert(System.Drawing.Color.Red)
						);
					}

					Vars.DisplayTextFont.DrawText(
						null,
						expPercent > 0
							? expPercent.ToString()
							: "0",
						Vars.ExpX - 12,
						Vars.ExpY + 17,
						Colors.Convert(SharpDX.Color.Yellow)
					);
				}
            };
        }
    }
}