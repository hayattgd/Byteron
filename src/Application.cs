using Byteron.hardware;
using Byteron.software;
using Raylib_cs;

namespace Byteron;

public static class Application
{
	public const string name = "Byteron";
	public static bool debug;
	public static bool isRunning = true;

	public static readonly string AppPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), name);
	public static bool reset = false;

	public static Display display = new();

	public static Shell shell = new(new(display));

	public static void Init()
	{
		Raylib.InitWindow((int)(display.width * 4.5f), (int)(display.height * 4.5f), name);
		Raylib.SetWindowState(ConfigFlags.ResizableWindow | ConfigFlags.VSyncHint);

		display.Init();
		shell.Run();
		Filesystem.Init();

		isRunning = true;
		reset = false;

		#if DEBUG
		debug = true;
		#endif
	}

	public static void Update()
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(display.average);

		shell.Update();
		display.Draw();

		Raylib.EndDrawing();
	}

	public static void Unload()
	{

	}
}