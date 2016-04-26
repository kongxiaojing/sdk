using System.Drawing;
using System.Linq;
using ExorAIO.Champions.Kalista;
using LeagueSharp;
using LeagueSharp.SDK;

namespace ExorAIO.Utilities
{
    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal class Healthbars
    {
        /// <summary>
        ///     Loads the drawings.
        /// </summary>
        public static void Initialize()
        {
            Drawing.OnDraw += delegate
            {
                GameObjects.Jungle
                    .Where(
                        h =>
                            !h.IsMe &&
                            h.IsValid() &&
                            Bools.IsPerfectRendTarget(h) &&
                            !h.CharData.BaseSkinName.Contains("Mini") &&
                            !h.CharData.BaseSkinName.Contains("Minion"))
                    .ForEach(
                        unit =>
                        {
                            /// <summary>
                            ///     Defines what HPBar Offsets it should display.
                            /// </summary>
                            var mobOffset =
                                Vars.JungleHpBarOffsetList.FirstOrDefault(
                                    x => x.BaseSkinName.Equals(unit.CharData.BaseSkinName));

                            var width = unit is Obj_AI_Minion ? mobOffset.Width : Vars.Width;
                            var height = unit is Obj_AI_Minion ? mobOffset.Height : Vars.Height;
                            var xOffset = unit is Obj_AI_Minion ? mobOffset.XOffset: Vars.XOffset;
                            var yOffset = unit is Obj_AI_Minion ? mobOffset.YOffset : Vars.YOffset;

                            var barPos = unit.HPBarPosition;

                            barPos.X += xOffset;
                            barPos.Y += yOffset;

                            var drawEndXPos = barPos.X + width * (unit.HealthPercent / 100);
                            var drawStartXPos = barPos.X +
                                                (unit.Health > KillSteal.GetPerfectRendDamage(unit)
                                                    ? width *
                                                      ((unit.Health - KillSteal.GetPerfectRendDamage(unit)) /
                                                       unit.MaxHealth * 100 / 100)
                                                    : 0);

                            Drawing.DrawLine(drawStartXPos, barPos.Y, drawEndXPos, barPos.Y, height, unit.Health < KillSteal.GetPerfectRendDamage(unit) ? Color.Blue : Color.Orange);
                            Drawing.DrawLine(drawStartXPos, barPos.Y, drawStartXPos, barPos.Y + height + 1, 1, Color.Lime);
                        });
            };
        }
    }
}