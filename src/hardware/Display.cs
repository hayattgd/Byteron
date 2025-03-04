using Byteron.software;
using Raylib_cs;

namespace Byteron.hardware;

public class Display
{
	public Display()
	{
		fps = 60;
	}

	private int _fps;
	public int fps {
		get => _fps;
		set
		{
			int limited = value;
			if (limited < 7) { limited = 7; }

			_fps = limited;
			Raylib.SetTargetFPS(limited);
		}
	}

	public int width { get; private set; } = 256;
	public int height { get; private set; } = 144;

	public int[][] pixel { get; private set; } = [];

	public int pixelsize { get; private set; } = 1;

	public Color[] palette = [
		//GO-LINE PALETTE
		//https://lospec.com/palette-list/go-line
		// <3 Thank you

		new Color(67, 0,  103), //0
		new Color(148,33, 106), //1
		new Color(255,0,  77 ), //2
		new Color(255,132,38 ), //3
		new Color(255,221,52 ), //4
		new Color(80, 225,18 ), //5
		new Color(63, 166,11 ), //6
		new Color(54, 89, 135), //7
		new Color(0,  0,  0  ), //8
		new Color(0,  51, 255), //9
		new Color(41, 173,255), //10
		new Color(0,  255,204), //11
		new Color(255,241,232), //12
		new Color(194,195,199), //13
		new Color(171,82, 54 ), //14
		new Color(95, 87, 79 ), //15
	];

	public Color average
	{
		get
		{
			long r = 0;
			long g = 0;
			long b = 0;
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Color onepixel = palette[pixel[x][y]];
					r += onepixel.R;
					g += onepixel.G;
					b += onepixel.B;
				}
			}

			int ar = (int)(r / (width * height));
			int ag = (int)(g / (width * height));
			int ab = (int)(b / (width * height));
			return new(ar, ag, ab);
		}
	}

	public void Init()
	{
		pixel = new int[width][];
		for (int x = 0; x < width; x++)
		{
			pixel[x] = new int[height];
			for (int y = 0; y < height; y++)
			{
				pixel[x][y] = (x + y) % 16;
			}
		}
	}

	public bool ChangeResolution(int w, int h)
	{
		int actualw = w;
		int actualh = h;
		if (w < 32) actualw = 32;
		if (h < 18) actualh = 18;

		width = actualw;
		height = actualh;
		Init();

		return actualw == w && actualh == h;
	}

	public void Clear(int color)
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				pixel[x][y] = color;
			}
		}
	}

	public void FillRect(int x, int y, int w, int h, int color)
	{
		for (int cw = 0; cw < w; cw++)
		{
			for (int ch = 0; ch < h; ch++)
			{
				SetPixel(x + cw, y + ch, color);
			}
		}
	}

	public bool CheckRect(int x, int y, int w, int h)
	{
		if (!CheckPixel(x, y)) return false;
		if (!CheckPixel(x + w, y + h)) return false;

		return true;
	}

	public bool SetPixel(int x, int y, int color)
	{
		if (!CheckPixel(x, y)) return false;

		pixel[x][y] = color;
		return true;
	}

	public bool CheckPixel(int x, int y)
	{
		if (x < 0) return false;
		if (y < 0) return false;
		if (x > width - 1) return false;
		if (y > height - 1) return false;

		return true;
	}

	public void Draw()
	{
		pixelsize = (int)MathF.Max(MathF.Min(Raylib.GetRenderWidth() / width, Raylib.GetRenderHeight() / height), 1);
		
		if (Application.debug)
		{
			TextRenderer textRenderer = new(this);
			textRenderer.DrawText(2, height - 6, "Res:" + width + "x" + height, 10);
			textRenderer.DrawText(2, height - 12, "Scale:" + pixelsize, 10);
			textRenderer.DrawText(2, height - 18, "FPS:" + Raylib.GetFPS() + "/" + fps, 5);
			for (int i = 0; i < palette.Length; i++)
			{
				SetPixel(0, height - i, i);
			}
		}

		int xpadding = Raylib.GetRenderWidth() - width * pixelsize;
		int ypadding = Raylib.GetRenderHeight() - height * pixelsize;

		int xoffset = xpadding / 2;
		int yoffset = ypadding / 2;

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				// if (x % 2 == 0 && y % 2 == 0) continue;
				Raylib.DrawRectangle(x * pixelsize + xoffset, y * pixelsize + yoffset, pixelsize, pixelsize, palette[pixel[x][y]]);
			}
		}
	}
}