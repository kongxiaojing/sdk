using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using System.Drawing;

namespace NabbActivator
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
            if (SpellSlots.GetSmiteSlot().IsReady() &&
                SpellSlots.GetSmiteSlot() != SpellSlot.Unknown &&
                Vars.Menu["smite"]["drawings"]["damage"].GetValue<MenuBool>().Value)
            {
                Drawing.OnDraw += delegate
                {
                    Targets.JungleMinions.ForEach(
                        unit =>
                        {
                            /// <summary>
                            ///     Defines what HPBar Offsets it should display.
                            /// </summary>
                            var mobOffset = Vars.JungleHpBarOffsetList.FirstOrDefault(x => x.BaseSkinName.Equals(unit.CharData.BaseSkinName));
                            
                            var barPos = unit.HPBarPosition;
                            {
                                barPos.X += mobOffset.XOffset;
                                barPos.Y += mobOffset.XOffset;
                            }

                            var drawEndXPos = barPos.X + mobOffset.Width * (unit.HealthPercent / 100);
                            var drawStartXPos = barPos.X + (unit.Health > Vars.GetSmiteDamage
                                ? mobOffset.Width * (((unit.Health - Vars.GetSmiteDamage) / unit.MaxHealth * 100) / 100)
                                : 0);

                            Drawing.DrawLine(
                                drawStartXPos,
                                barPos.Y,
                                drawEndXPos,
                                barPos.Y,
                                mobOffset.Height,
                                unit.Health < Vars.GetSmiteDamage
                                    ? Color.Blue 
                                    : Color.Orange
                            );

                            Drawing.DrawLine(
                                drawStartXPos,
                                barPos.Y,
                                drawStartXPos,
                                barPos.Y + mobOffset.Height + 1,
                                1,
                                Color.Lime
                            );
                        }
                    );
                };
            }
        }
    }
}