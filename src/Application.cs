using Byteron.hardware;
using Byteron.software;
using Raylib_cs;

namespace Byteron;

public static class Application
{
	public const string name = "Byteron";
	public static bool debug;

	public static Display display = new();
	public static Folder rootfolder = new(name + "-HDD", true);

	public static Shell shell = new();

	public static void Init()
	{
		Raylib.InitWindow(896, 560, name);
		Raylib.SetWindowState(ConfigFlags.ResizableWindow | ConfigFlags.VSyncHint);

		display.Init();
	}

	public static void Update()
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(display.average);
		shell.Run();
		display.Draw();
		Raylib.EndDrawing();
	}

	public static void Unload()
	{

	}
}