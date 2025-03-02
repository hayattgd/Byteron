namespace Byteron.software;

public static class TextRenderer
{
	//43 text to fill entire screen (pos:0,0 size:6x6)
	//43x24 text

	public static bool DrawCharacter(int x, int y, int character, int color)
	{
		bool success = true;
		if (character < 0) return true; //Returns true because this is space or \n

		for (int rx = 0; rx < 5; rx++)
		{
			for (int ry = 0; ry < 5; ry++)
			{
				if (font[character][ry][rx] == '@')
				{
					if (!Application.display.SetPixel(x+rx, y+ry, color))
					{
						success = false;
					}
				}
			}
		}

		return success;
	}

	public static bool DrawCharacter(int x, int y, char character, int color)
	{
		return DrawCharacter(x, y, CharToIdx(character), color);
	}

	public static void DrawText(int x, int y, string text, int color)
	{
		int currentx = x;
		int currenty = y;
		for (int i = 0; i < text.Length; i++)
		{
			currentx += 6;
			if (text[i] == '\n')
			{
				currenty += 6;
				currentx = x;
			}
			DrawCharacter(currentx, currenty, text[i], color);
		}
	}

	public static void DrawTextWrap(int x, int y, string text, int color)
	{
		int currentx = x;
		int currenty = y;
		for (int i = 0; i < text.Length; i++)
		{
			currentx += 6;
			if ((text[i] == '\n') || (!Application.display.CheckRect(currentx, currenty, 5, 5)))
			{
				currenty += 6;
				currentx = x;
			}
			DrawCharacter(currentx, currenty, text[i], color);
		}
	}

	//ik im going crazy with this one
	public static int CharToIdx(char character)
	{
		return character switch
		{
			'\n' => -1,
			' ' => -1,
			'A' => 1,
			'B' => 2,
			'C' => 3,
			'D' => 4,
			'E' => 5,
			'F' => 6,
			'G' => 7,
			'H' => 8,
			'I' => 9,
			'J' => 10,
			'K' => 11,
			'L' => 12,
			'M' => 13,
			'N' => 14,
			'O' => 15,
			'P' => 16,
			'Q' => 17,
			'R' => 18,
			'S' => 19,
			'T' => 20,
			'U' => 21,
			'V' => 22,
			'W' => 23,
			'X' => 24,
			'Y' => 25,
			'Z' => 26,
			'a' => 27,
			'b' => 28,
			'c' => 29,
			'd' => 30,
			'e' => 31,
			'f' => 32,
			'g' => 33,
			'h' => 34,
			'i' => 35,
			'j' => 36,
			'k' => 37,
			'l' => 38,
			'm' => 39,
			'n' => 40,
			'o' => 41,
			'p' => 42,
			'q' => 43,
			'r' => 44,
			's' => 45,
			't' => 46,
			'u' => 47,
			'v' => 48,
			'w' => 49,
			'x' => 50,
			'y' => 51,
			'z' => 52,
			'0' => 53,
			'1' => 54,
			'2' => 55,
			'3' => 56,
			'4' => 57,
			'5' => 58,
			'6' => 59,
			'7' => 60,
			'8' => 61,
			'9' => 62,
			'+' => 63,
			'-' => 64,
			'*' => 65,
			'/' => 66,
			'=' => 67,
			'(' => 68,
			')' => 69,
			'>' => 70,
			'<' => 71,
			'[' => 72,
			']' => 73,
			'{' => 74,
			'}' => 75,
			'!' => 76,
			'"' => 77,
			'#' => 78,
			'$' => 79,
			'%' => 80,
			'&' => 81,
			'\'' => 82,
			'.' => 83,
			'_' => 84,
			':' => 85,
			_ => 0,
		};
	}

	static readonly char[][][] font = [
		[
			['@', '@', '@', '@', '@'],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
			['@', '@', '@', '@', '@'],
		],
		[
			[' ', '@', '@', '@', ' '],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
			['@', '@', '@', '@', '@'],
			['@', ' ', ' ', ' ', '@'],
		],
		[
			['@', '@', '@', '@', ' '],
			['@', ' ', ' ', ' ', '@'],
			['@', '@', '@', '@', ' '],
			['@', ' ', ' ', ' ', '@'],
			['@', '@', '@', '@', ' '],
		],
		[
			[' ', '@', '@', '@', ' '],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', ' '],
			['@', ' ', ' ', ' ', '@'],
			[' ', '@', '@', '@', ' '],
		],
		[
			['@', '@', '@', '@', ' '],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
			['@', '@', '@', '@', ' '],
		],
		[
			['@', '@', '@', '@', '@'],
			['@', ' ', ' ', ' ', ' '],
			['@', '@', '@', '@', '@'],
			['@', ' ', ' ', ' ', ' '],
			['@', '@', '@', '@', '@'],
		],
		[
			['@', '@', '@', '@', '@'],
			['@', ' ', ' ', ' ', ' '],
			['@', '@', '@', '@', '@'],
			['@', ' ', ' ', ' ', ' '],
			['@', ' ', ' ', ' ', ' '],
		],
		[
			[' ', '@', '@', '@', ' '],
			['@', ' ', ' ', ' ', ' '],
			['@', ' ', ' ', '@', '@'],
			['@', ' ', ' ', ' ', '@'],
			[' ', '@', '@', '@', ' '],
		],
		[
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
			['@', '@', '@', '@', '@'],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
		],
		[
			['@', '@', '@', '@', '@'],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			['@', '@', '@', '@', '@'],
		],
		[
			['@', '@', '@', '@', '@'],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
		],
		[
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', '@', ' '],
			['@', '@', '@', ' ', ' '],
			['@', ' ', ' ', '@', ' '],
			['@', ' ', ' ', ' ', '@'],
		],
		[
			['@', ' ', ' ', ' ', ' '],
			['@', ' ', ' ', ' ', ' '],
			['@', ' ', ' ', ' ', ' '],
			['@', ' ', ' ', ' ', ' '],
			['@', '@', '@', '@', '@'],
		],
		[
			['@', ' ', ' ', ' ', '@'],
			['@', '@', ' ', '@', '@'],
			['@', ' ', '@', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
		],
		[
			['@', ' ', ' ', ' ', '@'],
			['@', '@', ' ', ' ', '@'],
			['@', ' ', '@', ' ', '@'],
			['@', ' ', ' ', '@', '@'],
			['@', ' ', ' ', ' ', '@'],
		],
		[
			[' ', '@', '@', '@', ' '],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
			[' ', '@', '@', '@', ' '],
		],
		[
			['@', '@', '@', '@', ' '],
			['@', ' ', ' ', ' ', '@'],
			['@', '@', '@', '@', ' '],
			['@', ' ', ' ', ' ', ' '],
			['@', ' ', ' ', ' ', ' '],
		],
		[
			[' ', '@', '@', '@', ' '],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', '@', ' '],
			[' ', '@', '@', ' ', '@'],
		],
		[
			['@', '@', '@', '@', ' '],
			['@', ' ', ' ', ' ', '@'],
			['@', '@', '@', '@', ' '],
			['@', ' ', '@', ' ', ' '],
			['@', ' ', ' ', '@', ' '],
		],
		[
			[' ', '@', '@', '@', '@'],
			['@', ' ', ' ', ' ', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', ' ', ' ', ' ', '@'],
			['@', '@', '@', '@', ' '],
		],
		[
			['@', '@', '@', '@', '@'],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
		],
		[
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
			[' ', '@', '@', '@', ' '],
		],
		[
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', ' ', ' ', '@'],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
		],
		[
			['@', ' ', '@', ' ', '@'],
			['@', ' ', '@', ' ', '@'],
			['@', ' ', '@', ' ', '@'],
			['@', ' ', '@', ' ', '@'],
			[' ', '@', '@', '@', ' '],
		],
		[
			['@', ' ', ' ', ' ', '@'],
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', ' ', '@', ' '],
			['@', ' ', ' ', ' ', '@'],
		],
		[
			['@', ' ', ' ', ' ', '@'],
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
		],
		[
			['@', '@', '@', '@', '@'],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			['@', '@', '@', '@', '@'],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', '@', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', '@', '@', ' '],
		],
		[
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', '@', ' ', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', '@', '@', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', '@', '@', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', ' ', '@', '@', ' '],
		],
		[
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', '@', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', '@', '@', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', '@', '@', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', ' ', '@', '@', ' '],
		],
		[
			[' ', ' ', '@', '@', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', '@', '@', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', '@', '@', ' ', ' '],
		],
		[
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', '@', ' ', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', ' ', '@', ' '],
		],
		[
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
		],
		[
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', '@', '@', ' ', ' '],
		],
		[
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', '@', ' ', ' '],
			[' ', '@', ' ', '@', ' '],
		],
		[
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', ' ', '@', '@', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', '@', ' ', '@', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', '@', ' ', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', ' ', '@', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', '@', '@', ' ', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', '@', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', '@', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', '@', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', '@', '@', ' '],
			[' ', '@', '@', ' ', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', '@', '@', '@', ' '],
		],
		[
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', '@', '@', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			['@', ' ', ' ', ' ', '@'],
			['@', ' ', '@', ' ', '@'],
			['@', ' ', '@', ' ', '@'],
			[' ', '@', ' ', '@', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', ' ', '@', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', ' ', '@', '@', ' '],
			[' ', '@', '@', ' ', ' '],
			[' ', '@', '@', '@', ' '],
		],
		[
			[' ', '@', '@', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', '@', '@', ' '],
		],
		[
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
		],
		[
			[' ', '@', '@', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', '@', '@', ' '],
		],
		[
			[' ', '@', '@', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', '@', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', '@', '@', '@', ' '],
		],
		[
			[' ', '@', ' ', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
		],
		[
			[' ', '@', '@', '@', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', '@', '@', '@', ' '],
		],
		[
			[' ', '@', '@', '@', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', '@', '@', ' '],
		],
		[
			[' ', '@', '@', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
		],
		[
			[' ', '@', '@', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', '@', '@', ' '],
		],
		[
			[' ', '@', '@', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', '@', '@', '@', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
		],
		[
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
		],
		[
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', '@', '@', '@', ' '],
			[' ', ' ', ' ', ' ', ' '],
		],
		[
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
		],
		[
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
		],
		[
			[' ', '@', ' ', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
		],
		[
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', ' ', '@', ' '],
		],
		[
			[' ', '@', '@', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', '@', '@', ' ', ' '],
		],
		[
			[' ', ' ', '@', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', '@', '@', ' '],
		],
		[
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			['@', '@', ' ', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
		],
		[
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', ' ', '@', '@'],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
		],
		[
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
		],
		[
			[' ', '@', ' ', '@', ' '],
			[' ', '@', ' ', '@', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
		],
		[
			[' ', '@', ' ', '@', ' '],
			['@', '@', '@', '@', '@'],
			[' ', '@', ' ', '@', ' '],
			['@', '@', '@', '@', '@'],
			[' ', '@', ' ', '@', ' '],
		],
		[
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', '@', ' '],
			[' ', '@', '@', ' ', ' '],
			[' ', ' ', '@', '@', ' '],
			[' ', '@', '@', ' ', ' '],
		],
		[
			['@', ' ', ' ', ' ', '@'],
			[' ', ' ', ' ', '@', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', '@', ' ', ' ', ' '],
			['@', ' ', ' ', ' ', '@'],
		],
		[
			[' ', '@', ' ', ' ', ' '],
			['@', ' ', '@', ' ', ' '],
			[' ', '@', '@', ' ', '@'],
			['@', ' ', ' ', '@', ' '],
			[' ', '@', '@', ' ', '@'],
		],
		[
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
			['@', '@', '@', '@', '@'],
		],
		[
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
			[' ', ' ', '@', ' ', ' '],
			[' ', ' ', ' ', ' ', ' '],
		],
	];
}