using Raylib_cs;

namespace Byteron;

public class Program
{
	public static int Main(string[] args)
	{
		if (args.Contains("-debug")) Application.debug = true;

		Application.Init();

		while (!Raylib.WindowShouldClose())
		{
			Application.Update();
		}

		Application.Unload();

		return 0;
	}
}