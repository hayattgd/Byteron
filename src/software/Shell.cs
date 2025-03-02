using Raylib_cs;

namespace Byteron.software;

public class Shell
{
	public Shell(TextRenderer render)
	{
		this.render = render;
	}

	public bool cantype;
	public int x;
	public int y;
	public string typed { get; private set; } = "";

	int ticks = 0;
	int bg = 8;
	int fg = 12;

	public List<Text> text = [];

	public TextRenderer render;

	public struct Text
	{
		public Text(string t, int c)
		{
			text = t;
			color = c;
		}
		public string text;
		public int color;
	}

	public void Printf(string str, int c)
	{
		text.Add(new(str, c));
		if(text.Count > Application.display.height / 6)
		{
			text.RemoveAt(0);
		}
	}

	public void Run()
	{
		Application.display.Clear(8);
		Printf("Welcome to " + Application.name + "!", fg);
		Printf("type help for list of commands.", fg);
		Prompt();
	}

	public void Prompt()
	{
		Printf(">", fg);
		y = text.Count - 1;
		x = 1;
		cantype = true;
		typed = "";
	}

	public void RunCommand(string raw)
	{
		string[] args = raw.Split(' ');
		string command = args[0];

		if (command == "") return;

		try
		{
			switch (command)
			{
				case "help":
				{
					Printf("help - Shows this text", fg);
					Printf("clear - Clear texts on console", fg);
					Printf("echo - Prints text", fg);
					Printf("color - Sets color of shell", fg);
					Printf("cd - Changes current directory", fg);
					Printf("ls - List files and folders existing", fg);
					break;
				}

				case "clear":
				{
					text.Clear();
					break;
				}

				case "echo":
				{
					if (args.Length < 2) break;
					string str = "";
					for (int i = 1; i < args.Length; i++)
					{
						str += args[i] + " ";
					}
					Printf(str, fg);
					break;
				}

				case "color":
				{
					if (args.Length < 2)
					{
						Printf("No color specified.", 2);
						break;
					}

					if (int.Parse(args[1]) > 15)
					{
						Printf("Color out of range.", 2);
						break;
					}

					fg = int.Parse(args[1]);
					if (args.Length == 3)
					{
						if (int.Parse(args[2]) > 15)
						{
							Printf("Color out of range.", 2);
							break;
						}
						bg = int.Parse(args[2]);
					}

					break;
				}

				default:
				{
					Printf("command \"" + command + "\" don't exists.", 2);
					break;
				}
			}
		}
		catch (Exception ex)
		{
			Printf(ex.ToString(), 2);
		}
	}

	public void Update()
	{
		ticks++;

		Application.display.Clear(bg);
		for (int i = 0; i < text.Count; i++)
		{
			render.DrawText(0, i * 6, text[i].text, text[i].color);
		}

		int charcode = Raylib.GetCharPressed();
		if (cantype)
		{
			if (charcode != 0)
			{
				char character = Convert.ToChar(charcode);
				typed += character;
			}
			else if(Raylib.IsKeyPressed(KeyboardKey.Backspace) && typed.Length > 0)
			{
				typed = typed.Remove(typed.Length - 1, 1);
			}
			else if(Raylib.IsKeyPressed(KeyboardKey.Enter))
			{
				cantype = false;
				text[y] = new(text[y].text + typed, text[y].color);
				RunCommand(typed);
				typed = "";
				Prompt();
			}

			render.DrawText(x * 6, y * 6, typed + (ticks / (render.target.fps / 4) % 2 == 0 ? "_" : ""), fg);
		}
	}
}