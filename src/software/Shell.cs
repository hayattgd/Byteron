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

	public string currentpath = "";

	string previousCommand = "";

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
		if(text.Count > render.target.height / 6)
		{
			text.RemoveAt(0);
		}
	}

	public void Run()
	{
		Init();
		Prompt();
	}

	public void Init()
	{
		render.target.Clear(8);
		Printf("Welcome to " + Application.name + "!", fg);
		Printf("type help for list of commands.", fg);
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
		previousCommand = raw;

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
					Printf("exit - Shutdown Byteron", fg);
					Printf("reset - Reset Byteron", fg);
					Printf("dbg - Toggle debug info", fg);
					Printf("res - Change resolution", fg);
					Printf("clear - Clear texts on console", fg);
					Printf("echo - Prints text", fg);
					Printf("color - Sets color of shell", fg);
					Printf("cd - Changes current directory", fg);
					Printf("touch - Create new file", fg);
					Printf("mkdir - Create new folder", fg);
					Printf("ls - List files and folders existing", fg);
					Printf("rm - Remove folder/file", fg);
					break;
				}

				case "exit":
				{
					Application.isRunning = false;
					break;
				}

				case "reset":
				{
					Application.isRunning = false;
					Application.reset = true;
					text.Clear();
					Application.display = new();
					Application.display.Init();
					render.target = Application.display;
					Init();
					break;
				}

				case "dbg":
				{
					Application.debug = !Application.debug;
					break;
				}

				case "res":
				{
					if (args.Length < 2 || (args[1] != "fit" && args.Length != 3))
					{
						Printf("Usage:res [width] [height]", 2);
						Printf("Usage:res fit (scale) (-f)", 2);
						Printf("Current:"+render.target.width+"x"+render.target.height, 2);
						Printf("Default:256x144", 2);
						break;
					}
					if (args[1] == "fit")
					{
						int scale = 1;
						if (args.Length > 2) scale = int.Parse(args[2]);
						if (!(args.Length == 4 && args[3] == "-f"))
						{
							if (scale > 18)
							{
								Printf("Scaling up to " + scale + " cause glitch/lag.", 2);
								Printf("If you proceeds with it, run again with -f", 2);
								break;
							}
						}
						Raylib.SetWindowSize(render.target.width * scale, render.target.height * scale);
					}
					else if (args.Length == 3)
					{
						int w = int.Parse(args[1]);
						int h = int.Parse(args[2]);

						if (w*h > 95000)
						{
							Printf("High resolution not recommended.", 2);
							break;
						}

						if (!render.target.ChangeResolution(w, h))
						{
							Printf("22x18 is minium screen size.", 4);
						}
					}
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
						Printf("Usage:color [fg] (bg)", 2);
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

				case "cd":
				{
					if (Filesystem.ListFolders(currentpath).Contains(args[1]))
					{
						Path.Combine(currentpath, args[1]);
					}
					else
					{
						Printf($"Folder {args[1]} does not exist.", 2);
					}
					break;
				}

				case "touch":
				{
					if (args.Length != 2)
					{
						Printf("Usage: touch [NAME]", 2);
					}

					Filesystem.WriteFile(Path.Combine(currentpath, args[1]), "");
					break;
				}

				case "mkdir":
				{
					if (args.Length != 2)
					{
						Printf("Usage: mkdir [NAME]", 2);
					}

					Filesystem.CreateFolder(Path.Combine(currentpath, args[1]));
					break;
				}

				case "ls":
				{
					string[] folders = Filesystem.ListFolders(currentpath);
					string[] files = Filesystem.ListFiles(currentpath);

					foreach (string stuff in folders.Concat(files))
					{
						if (folders.Contains(stuff))
						{
							Printf(stuff, 10);
						}
						else
						{
							Printf(stuff, 12);
						}
					}
					break;
				}

				case "rm":
				{
					if (args.Length != 2 || (args.Length == 3 && args[2] != "-r"))
					{
						Printf("Usage: rm [NAME] (-r)", 2);
						Printf("-r : Remove file/folders recursively", 2);
					}

					if (Filesystem.FileExists(args[1]))
					{
						Filesystem.RemoveFile(args[1]);
					}
					else if (Filesystem.FolderExists(args[1]))
					{
						bool recursive = args.Length == 3 && args[2] == "-r";
						Filesystem.RemoveFolder(args[1], recursive);
					}
					else
					{
						Printf($"File/Folder {args[1]} does not exist.", 2);
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
			Console.WriteLine(ex.ToString());
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
			else if(Raylib.IsKeyPressed(KeyboardKey.Up))
			{
				typed = previousCommand;
			}
			else if(Raylib.IsKeyPressed(KeyboardKey.Down))
			{
				typed = "";
			}

			render.DrawText(x * 6, y * 6, typed + (ticks / (render.target.fps / 4) % 2 == 0 ? "_" : ""), fg);
		}
	}
}