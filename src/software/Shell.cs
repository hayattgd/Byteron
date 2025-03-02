namespace Byteron.software;

public class Shell
{
	public void Run()
	{
		Application.display.Clear(8);
		TextRenderer.DrawText(0, 0, "Welcome to " + Application.name + "!", 12);
		TextRenderer.DrawText(0, 6, "type help for list of commands.", 12);
		Prompt(0, 12);
	}

	public void Prompt(int x, int y)
	{
		TextRenderer.DrawText(x, y, ">", 12);
	}

	public string ReadLine(int x, int y)
	{
		return "";
	}
}