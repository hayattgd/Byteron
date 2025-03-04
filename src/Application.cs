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
		Raylib.InitWindow((int)MathF.Floor(display.width * 3.7f), (int)MathF.Floor(display.height * 3.7f), name);
		Raylib.SetWindowState(ConfigFlags.VSyncHint | ConfigFlags.FullscreenMode | ConfigFlags.ResizableWindow);

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

		shell.Update();
		Raylib.ClearBackground(display.average);
		display.Draw();

		Raylib.EndDrawing();
	}

	public static void Unload()
	{

	}
}