using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using SharpDX;
using System.Drawing;

namespace NabbTracker
{
    /// <summary>
    ///     The Color convert class,
	///		Converts a SharpDX.Color or a System.Drawing.Color type into the same color as seen by a colorblind person
	///		for each type of colorblindness, using the 'FromBgra' function.
	/// 	https://github.com/sharpdx/SharpDX/blob/master/Source/SharpDX.Mathematics/Color.Palette.cs
    /// </summary>
    internal class Colors
	{
		/// <summary>
		///     The convert class.
		/// </summary>
		public static SharpDX.Color Convert(SharpDX.Color color)
		{
			/// <summary>
			///     The yellow color.
			/// </summary>
			if (color == SharpDX.Color.Gray)
			{
				switch (Vars.Menu["miscellaneous"]["colorblind"]["mode"].GetValue<MenuList>().Index)
				{
					case 1: //Deuteranopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFF7F7F81);
						break;
					case 2: //Protanopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFF807E82);
						break;
					case 3: //Tritanopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFF808080);
						break;
					case 4: //Achromatopsia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFF7F7F7F);
						break;
					default:
						Vars.SDXColor = SharpDX.Color.Gray;
						break;
				}
				
				return Vars.SDXColor;
			}

			/// <summary>
			///     The yellow color.
			/// </summary>
			if (color == SharpDX.Color.Yellow)
			{
				switch (Vars.Menu["miscellaneous"]["colorblind"]["mode"].GetValue<MenuList>().Index)
				{
					case 1: //Deuteranopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFFFFE802);
						break;
					case 2: //Protanopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFFFFF205);
						break;
					case 3: //Tritanopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFFFFD6D0);
						break;
					case 4: //Achromatopsia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFFE1E1E1);
						break;
					default:
						Vars.SDXColor = SharpDX.Color.Yellow;
						break;
				}

				return Vars.SDXColor;
			}

			/// <summary>
			///     The red color.
			/// </summary>
			if (color == SharpDX.Color.Red)
			{
				switch (Vars.Menu["miscellaneous"]["colorblind"]["mode"].GetValue<MenuList>().Index)
				{
					case 1: //Deuteranopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFF6D5600);
						break;
					case 2: //Protanopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFF332802);
						break;
					case 3: //Tritanopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFFF70500);
						break;
					case 4: //Achromatopsia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFF4C4C4C);
						break;
					default:
						Vars.SDXColor = SharpDX.Color.Red;
						break;
				}

				return Vars.SDXColor;
			}

			/// <summary>
			///     The cyan color.
			/// </summary>
			if (color == SharpDX.Color.Cyan)
			{
				switch (Vars.Menu["miscellaneous"]["colorblind"]["mode"].GetValue<MenuList>().Index)
				{
					case 1: //Deuteranopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFF91A8FF);
						break;
					case 2: //Protanopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFFCCD3FF);
						break;
					case 3: //Tritanopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFF07F9FF);
						break;
					case 4: //Achromatopsia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFFB2B2B2);
						break;
					default:
						Vars.SDXColor = SharpDX.Color.Cyan;
						break;
				}
				
				return Vars.SDXColor;
			}

			/// <summary>
			///     The lightgreen color.
			/// </summary>
			if (color == SharpDX.Color.LightGreen)
			{
				switch (Vars.Menu["miscellaneous"]["colorblind"]["mode"].GetValue<MenuList>().Index)
				{
					case 1: //Deuteranopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFFD3C594);
						break;
					case 2: //Protanopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFFEDD893);
						break;
					case 3: //Tritanopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFF9ADCE2);
						break;
					case 4: //Achromatopsia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFFC7C7C7);
						break;
					default:
						Vars.SDXColor = SharpDX.Color.LightGreen;
						break;
				}

				return Vars.SDXColor;
			}

			/// <summary>
			///     The purple color.
			/// </summary>
			if (color == SharpDX.Color.Purple)
			{
				switch (Vars.Menu["miscellaneous"]["colorblind"]["mode"].GetValue<MenuList>().Index)
				{
					case 1: //Deuteranopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFF23377D);
						break;
					case 2: //Protanopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFF011981);
						break;
					case 3: //Tritanopia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFF71170F);
						break;
					case 4: //Achromatopsia
						Vars.SDXColor = SharpDX.Color.FromBgra(0xFF343434);
						break;
					default:
						Vars.SDXColor = SharpDX.Color.Purple;
						break;
				}
				
				return Vars.SDXColor;
			}

			return Vars.SDXColor;
		}

		/// <summary>
		///     The convert class.
		/// </summary>
		public static System.Drawing.Color Convert(System.Drawing.Color color)
		{
			/// <summary>
			///     The yellow color.
			/// </summary>
			if (color == System.Drawing.Color.Gray)
			{
				switch (Vars.Menu["miscellaneous"]["colorblind"]["mode"].GetValue<MenuList>().Index)
				{
					case 1: //Deuteranopia
						Vars.SDColor = System.Drawing.Color.FromArgb(127, 127, 129);
						break;
					case 2: //Protanopia
						Vars.SDColor = System.Drawing.Color.FromArgb(128, 126, 130);
						break;
					case 3: //Tritanopia
						Vars.SDColor = System.Drawing.Color.FromArgb(128, 128, 128);
						break;
					case 4: //Achromatopsia
						Vars.SDColor = System.Drawing.Color.FromArgb(127, 127, 127);
						break;
					default:
						Vars.SDColor = System.Drawing.Color.Gray;
						break;
				}

				return Vars.SDColor;
			}

			/// <summary>
			///     The yellow color.
			/// </summary>
			if (color == System.Drawing.Color.Yellow)
			{
				switch (Vars.Menu["miscellaneous"]["colorblind"]["mode"].GetValue<MenuList>().Index)
				{
					case 1: //Deuteranopia
						Vars.SDColor = System.Drawing.Color.FromArgb(255, 232, 2);
						break;
					case 2: //Protanopia
						Vars.SDColor = System.Drawing.Color.FromArgb(255, 242, 5);
						break;
					case 3: //Tritanopia
						Vars.SDColor = System.Drawing.Color.FromArgb(255, 214, 208);
						break;
					case 4: //Achromatopsia
						Vars.SDColor = System.Drawing.Color.FromArgb(225, 225, 225);
						break;
					default:
						Vars.SDColor = System.Drawing.Color.Yellow;
						break;
				}

				return Vars.SDColor;
			}

			/// <summary>
			///     The red color.
			/// </summary>
			if (color == System.Drawing.Color.Red)
			{
				switch (Vars.Menu["miscellaneous"]["colorblind"]["mode"].GetValue<MenuList>().Index)
				{
					case 1: //Deuteranopia
						Vars.SDColor = System.Drawing.Color.FromArgb(109, 86, 0);
						break;
					case 2: //Protanopia
						Vars.SDColor = System.Drawing.Color.FromArgb(51, 40, 2);
						break;
					case 3: //Tritanopia
						Vars.SDColor = System.Drawing.Color.FromArgb(247, 5, 0);
						break;
					case 4: //Achromatopsia
						Vars.SDColor = System.Drawing.Color.FromArgb(76, 76, 76);
						break;
					default:
						Vars.SDColor = System.Drawing.Color.Red;
						break;
				}

				return Vars.SDColor;
			}

			/// <summary>
			///     The cyan color.
			/// </summary>
			if (color == System.Drawing.Color.Cyan)
			{
				switch (Vars.Menu["miscellaneous"]["colorblind"]["mode"].GetValue<MenuList>().Index)
				{
					case 1: //Deuteranopia
						Vars.SDColor = System.Drawing.Color.FromArgb(145, 168, 255);
						break;
					case 2: //Protanopia
						Vars.SDColor = System.Drawing.Color.FromArgb(204, 211, 255);
						break;
					case 3: //Tritanopia
						Vars.SDColor = System.Drawing.Color.FromArgb(7, 249, 255);
						break;
					case 4: //Achromatopsia
						Vars.SDColor = System.Drawing.Color.FromArgb(178, 178, 178);
						break;
					default:
						Vars.SDColor = System.Drawing.Color.Cyan;
						break;
				}

				return Vars.SDColor;
			}

			/// <summary>
			///     The lightgreen color.
			/// </summary>
			if (color == System.Drawing.Color.LightGreen)
			{
				switch (Vars.Menu["miscellaneous"]["colorblind"]["mode"].GetValue<MenuList>().Index)
				{
					case 1: //Deuteranopia
						Vars.SDColor = System.Drawing.Color.FromArgb(211, 197, 148);
						break;
					case 2: //Protanopia
						Vars.SDColor = System.Drawing.Color.FromArgb(237, 216, 147);
						break;
					case 3: //Tritanopia
						Vars.SDColor = System.Drawing.Color.FromArgb(154, 220, 226);
						break;
					case 4: //Achromatopsia
						Vars.SDColor = System.Drawing.Color.FromArgb(199, 199, 199);
						break;
					default:
						Vars.SDColor = System.Drawing.Color.LightGreen;
						break;
				}

				return Vars.SDColor;
			}

			/// <summary>
			///     The purple color.
			/// </summary>
			if (color == System.Drawing.Color.Purple)
			{
				switch (Vars.Menu["miscellaneous"]["colorblind"]["mode"].GetValue<MenuList>().Index)
				{
					case 1: //Deuteranopia
						Vars.SDColor = System.Drawing.Color.FromArgb(35, 55, 125);
						break;
					case 2: //Protanopia
						Vars.SDColor = System.Drawing.Color.FromArgb(1, 19, 81);
						break;
					case 3: //Tritanopia
						Vars.SDColor = System.Drawing.Color.FromArgb(71, 17, 15);
						break;
					case 4: //Achromatopsia
						Vars.SDColor = System.Drawing.Color.FromArgb(34, 34, 34);
						break;
					default:
						Vars.SDColor = System.Drawing.Color.Purple;
						break;
				}

				return Vars.SDColor;
			}

			return Vars.SDColor;
		}
	}
}