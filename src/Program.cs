using Raylib_cs;

namespace Byteron;

public class Program
{
	public static int Main(string[] args)
	{
		if (args.Contains("-debug")) Application.debug = true;

		Application.Init();
		Raylib.SetExitKey(KeyboardKey.Null);

		return Init();
	}

	public static int Init()
	{
		Application.isRunning = true;
		Application.reset = false;

		while ((!Raylib.WindowShouldClose()) && Application.isRunning)
		{
			Application.Update();
		}

		Application.Unload();
		if (Application.reset)
		{
			return Init();
		}

		return 0;
	}
}