using Byteron.hardware;
using MoonSharp.Interpreter;
using Raylib_cs;

namespace Byteron.software;

public class API(Shell shell, Script lua)
{
	// Used to communicate between processes
	public static Dictionary<string, List<DynValue>> pipecallbacks = [];

	[Serializable]
	public class LuaException : Exception
	{
		public LuaException() { }
		public LuaException(string message) : base(message) { }
		public LuaException(string func, string message) : base(func + " : " + message) { }
	}

	Shell shell = shell;
	TextRenderer render { get => shell.render; }
	Display display { get => render.target; }

	string ColorOutOfRange = "Color must be 0..15";
	string MissingArgument = "Missing argument";

	static bool ColorRangeCheck(int? c)
	{
		return c != null && c > 15;
	}

	public void Print(string? text, int? color)
	{
		if (text == null) throw new LuaException("shell.print", MissingArgument);
		if (ColorRangeCheck(color)) throw new LuaException("shell.print", ColorOutOfRange);

		int col = (int)(color == null ? shell.fg : color);
		shell.Printf(text, col);
	}

	public void Run(string? raw)
	{
		if (raw == null) throw new LuaException("shell.run", MissingArgument);

		shell.output = false;
		shell.RunCommand(raw, false);
		shell.output = true;
	}

	public void SetFPS(int? target)
	{
		if (target == null) throw new LuaException("render.setfps", MissingArgument);

		if (target < 7)
		{
			display.fps = 7;
		}
		else
		{
			display.fps = (int)target;
		}
	}

	public void Clear(int? c)
	{
		if (c == null) throw new LuaException("render.clear", ColorOutOfRange);
		if (ColorRangeCheck(c)) throw new LuaException("render.clear", ColorOutOfRange);

		display.Clear((int)c);
	}

	public void SetPixel(int? x, int? y, int? c)
	{
		if (x == null) throw new LuaException("render.setpixel", MissingArgument);
		if (y == null) throw new LuaException("render.setpixel", MissingArgument);
		if (c == null) throw new LuaException("render.setpixel", MissingArgument);
		if (ColorRangeCheck(c)) throw new LuaException("render.setpixel", ColorOutOfRange);

		display.SetPixel((int)x, (int)y, (int)c);
	}

	public int GetPixel(int? x, int? y)
	{
		if (x == null) throw new LuaException("render.getpixel", MissingArgument);
		if (y == null) throw new LuaException("render.getpixel", MissingArgument);

		if (!display.CheckPixel((int)x, (int)y)) return 0;

		return display.pixel[(int)x][(int)y];
	}

	public bool CheckPixel(int? x, int? y)
	{
		if (x == null) throw new LuaException("render.checkpixel", MissingArgument);
		if (y == null) throw new LuaException("render.checkpixel", MissingArgument);

		return display.CheckPixel((int)x, (int)y);
	}

	public void FillRect(int? x, int? y, int? w, int? h, int? c)
	{
		if (x == null) throw new LuaException("render.setrect", MissingArgument);
		if (y == null) throw new LuaException("render.setrect", MissingArgument);
		if (w == null) throw new LuaException("render.setrect", MissingArgument);
		if (h == null) throw new LuaException("render.setrect", MissingArgument);
		if (c == null) throw new LuaException("render.setrect", MissingArgument);
		if (ColorRangeCheck(c)) throw new LuaException("render.setrect", ColorOutOfRange);

		display.FillRect((int)x, (int)y, (int)w, (int)h, (int)c);
	}

	public bool CheckRect(int? x, int? y, int? w, int? h)
	{
		if (x == null) throw new LuaException("render.checkrect", MissingArgument);
		if (y == null) throw new LuaException("render.checkrect", MissingArgument);
		if (w == null) throw new LuaException("render.checkrect", MissingArgument);
		if (h == null) throw new LuaException("render.checkrect", MissingArgument);

		return display.CheckRect((int)x, (int)y, (int)w, (int)h);
	}

	public bool IsKeyDown(int? key)
	{
		if (key == null) throw new LuaException("input.key", MissingArgument);

		return Raylib.IsKeyDown((KeyboardKey)key);
	}

	public bool IsKeyPressedNow(int? key)
	{
		if (key == null) throw new LuaException("input.keydown", MissingArgument);

		return Raylib.IsKeyPressed((KeyboardKey)key);
	}

	public bool IsKeyUpNow(int? key)
	{
		if (key == null) throw new LuaException("input.keyup", MissingArgument);

		return Raylib.IsKeyReleased((KeyboardKey)key);
	}

	public bool KeyRepeated(int? key)
	{
		if (key == null) throw new LuaException("input.keyrepeat", MissingArgument);

		return Raylib.IsKeyPressedRepeat((KeyboardKey)key);
	}

	public float GetMouseDeltaX()
	{
		return Raylib.GetMouseDelta().X / display.pixelsize;
	}

	public float GetMouseDeltaY()
	{
		return Raylib.GetMouseDelta().Y / display.pixelsize;
	}

	public bool MouseDown(int? button)
	{
		int mousebutton = (int)MouseButton.Left;
		if (button != null) mousebutton = (int)button;

		return Raylib.IsMouseButtonDown((MouseButton)mousebutton);
	}

	public bool MousePressed(int? button)
	{
		int mousebutton = (int)MouseButton.Left;
		if (button != null) mousebutton = (int)button;

		return Raylib.IsMouseButtonPressed((MouseButton)mousebutton);
	}

	public bool MouseReleased(int? button)
	{
		int mousebutton = (int)MouseButton.Left;
		if (button != null) mousebutton = (int)button;

		return Raylib.IsMouseButtonReleased((MouseButton)mousebutton);
	}

	public void DrawText(int? x, int? y, string? text, int? color)
	{
		if (x == null) throw new LuaException("text.draw", MissingArgument);
		if (y == null) throw new LuaException("text.draw", MissingArgument);
		if (text == null) throw new LuaException("text.draw", MissingArgument);

		int col = (int)(color == null ? 12 : color);
		if (ColorRangeCheck(col)) throw new LuaException("text.draw", ColorOutOfRange);

		render.DrawText((int)x, (int)y, text, col);
	}

	public void Exit()
	{
		shell.processes.Remove(lua);
	}

	public void AddPipeCallback(string? name, DynValue callback)
	{
		if (name == null) throw new LuaException("pipe.addcallback", MissingArgument);
		if (callback == null) throw new LuaException("pipe.addcallback", MissingArgument);

		if (callback.Type != DataType.Function) return;
		if (!pipecallbacks.ContainsKey(name)) pipecallbacks.Add(name, []);
		pipecallbacks[name].Add(callback);
	}

	public void RemovePipeCallback(string? name, DynValue callback)
	{
		if (name == null) throw new LuaException("pipe.removecallback", MissingArgument);
		if (callback == null) throw new LuaException("pipe.removecallback", MissingArgument);

		if (callback.Type != DataType.Function) return;
		if (!pipecallbacks.ContainsKey(name)) return;
		pipecallbacks[name].Remove(callback);
	}

	public void WritePipe(string? name, DynValue? content)
	{
		if (name == null) throw new LuaException("pipe.write", MissingArgument);
		if (content == null) throw new LuaException("pipe.write", MissingArgument);

		if (!pipecallbacks.ContainsKey(name)) return;
		foreach (var callback in pipecallbacks[name])
		{
			callback.Function.Call(content);
		}
	}

	public void RegisterAPIs()
	{
		lua.Globals["Exit"] = (Action)Exit;

		lua.Globals["Pipe"] = new Table(lua);
		((Table)lua.Globals["Pipe"])["addcallback"] = (Action<string?, DynValue>)AddPipeCallback;
		((Table)lua.Globals["Pipe"])["removecallback"] = (Action<string?, DynValue>)RemovePipeCallback;
		((Table)lua.Globals["Pipe"])["write"] = (Action<string?, DynValue?>)WritePipe;

		lua.Globals["Shell"] = new Table(lua);
		((Table)lua.Globals["Shell"])["print"] = (Action<string?, int?>)Print;
		((Table)lua.Globals["Shell"])["run"] = (Action<string?>)Run;
		((Table)lua.Globals["Shell"])["stopupdate"] = () => shell.update = false;
		((Table)lua.Globals["Shell"])["startupdate"] = () => shell.update = true;

		lua.Globals["Input"] = new Table(lua);
		((Table)lua.Globals["Input"])["key"] = (Func<int?, bool>)IsKeyDown;
		((Table)lua.Globals["Input"])["keydown"] = (Func<int?, bool>)IsKeyPressedNow;
		((Table)lua.Globals["Input"])["keyup"] = (Func<int?, bool>)IsKeyUpNow;
		((Table)lua.Globals["Input"])["keyrepeat"] = (Func<int?, bool>)KeyRepeated;
		((Table)lua.Globals["Input"])["mousebtn"] = (Func<int?, bool>)MouseDown;
		((Table)lua.Globals["Input"])["mousedown"] = (Func<int?, bool>)MousePressed;
		((Table)lua.Globals["Input"])["mouseup"] = (Func<int?, bool>)MouseReleased;
		((Table)lua.Globals["Input"])["lockcursor"] = (Action)Raylib.DisableCursor;
		((Table)lua.Globals["Input"])["unlockcursor"] = (Action)Raylib.EnableCursor;
		((Table)lua.Globals["Input"])["hidecursor"] = (Action)Raylib.HideCursor;
		((Table)lua.Globals["Input"])["showcursor"] = (Action)Raylib.ShowCursor;
		((Table)lua.Globals["Input"])["xdelta"] = (Func<float>)GetMouseDeltaX;
		((Table)lua.Globals["Input"])["ydelta"] = (Func<float>)GetMouseDeltaY;
		
		((Table)lua.Globals["Input"])["mouse"] = new Table(lua);
		((Table)((Table)lua.Globals["Input"])["mouse"])["Left"] = MouseButton.Left;
		((Table)((Table)lua.Globals["Input"])["mouse"])["Right"] = MouseButton.Right;
		((Table)((Table)lua.Globals["Input"])["mouse"])["Middle"] = MouseButton.Middle;
		((Table)((Table)lua.Globals["Input"])["mouse"])["Forward"] = MouseButton.Forward;
		((Table)((Table)lua.Globals["Input"])["mouse"])["Back"] = MouseButton.Back;

		//Keycodes comes here
		((Table)lua.Globals["Input"])["code"] = new Table(lua);
		((Table)((Table)lua.Globals["Input"])["code"])["A"] = KeyboardKey.A;
		((Table)((Table)lua.Globals["Input"])["code"])["B"] = KeyboardKey.B;
		((Table)((Table)lua.Globals["Input"])["code"])["C"] = KeyboardKey.C;
		((Table)((Table)lua.Globals["Input"])["code"])["D"] = KeyboardKey.D;
		((Table)((Table)lua.Globals["Input"])["code"])["E"] = KeyboardKey.E;
		((Table)((Table)lua.Globals["Input"])["code"])["F"] = KeyboardKey.F;
		((Table)((Table)lua.Globals["Input"])["code"])["G"] = KeyboardKey.G;
		((Table)((Table)lua.Globals["Input"])["code"])["H"] = KeyboardKey.H;
		((Table)((Table)lua.Globals["Input"])["code"])["I"] = KeyboardKey.I;
		((Table)((Table)lua.Globals["Input"])["code"])["J"] = KeyboardKey.J;
		((Table)((Table)lua.Globals["Input"])["code"])["K"] = KeyboardKey.K;
		((Table)((Table)lua.Globals["Input"])["code"])["L"] = KeyboardKey.L;
		((Table)((Table)lua.Globals["Input"])["code"])["M"] = KeyboardKey.M;
		((Table)((Table)lua.Globals["Input"])["code"])["N"] = KeyboardKey.N;
		((Table)((Table)lua.Globals["Input"])["code"])["O"] = KeyboardKey.O;
		((Table)((Table)lua.Globals["Input"])["code"])["P"] = KeyboardKey.P;
		((Table)((Table)lua.Globals["Input"])["code"])["Q"] = KeyboardKey.Q;
		((Table)((Table)lua.Globals["Input"])["code"])["R"] = KeyboardKey.R;
		((Table)((Table)lua.Globals["Input"])["code"])["S"] = KeyboardKey.S;
		((Table)((Table)lua.Globals["Input"])["code"])["T"] = KeyboardKey.T;
		((Table)((Table)lua.Globals["Input"])["code"])["U"] = KeyboardKey.U;
		((Table)((Table)lua.Globals["Input"])["code"])["V"] = KeyboardKey.V;
		((Table)((Table)lua.Globals["Input"])["code"])["W"] = KeyboardKey.W;
		((Table)((Table)lua.Globals["Input"])["code"])["X"] = KeyboardKey.X;
		((Table)((Table)lua.Globals["Input"])["code"])["Y"] = KeyboardKey.Y;
		((Table)((Table)lua.Globals["Input"])["code"])["Z"] = KeyboardKey.Z;
		((Table)((Table)lua.Globals["Input"])["code"])["One"] = KeyboardKey.One;
		((Table)((Table)lua.Globals["Input"])["code"])["Two"] = KeyboardKey.Two;
		((Table)((Table)lua.Globals["Input"])["code"])["Three"] = KeyboardKey.Three;
		((Table)((Table)lua.Globals["Input"])["code"])["Four"] = KeyboardKey.Four;
		((Table)((Table)lua.Globals["Input"])["code"])["Five"] = KeyboardKey.Five;
		((Table)((Table)lua.Globals["Input"])["code"])["Six"] = KeyboardKey.Six;
		((Table)((Table)lua.Globals["Input"])["code"])["Seven"] = KeyboardKey.Seven;
		((Table)((Table)lua.Globals["Input"])["code"])["Eight"] = KeyboardKey.Eight;
		((Table)((Table)lua.Globals["Input"])["code"])["Nine"] = KeyboardKey.Nine;
		((Table)((Table)lua.Globals["Input"])["code"])["Zero"] = KeyboardKey.Zero;
		((Table)((Table)lua.Globals["Input"])["code"])["Space"] = KeyboardKey.Space;
		((Table)((Table)lua.Globals["Input"])["code"])["Up"] = KeyboardKey.Up;
		((Table)((Table)lua.Globals["Input"])["code"])["Left"] = KeyboardKey.Left;
		((Table)((Table)lua.Globals["Input"])["code"])["Down"] = KeyboardKey.Down;
		((Table)((Table)lua.Globals["Input"])["code"])["Right"] = KeyboardKey.Right;
		((Table)((Table)lua.Globals["Input"])["code"])["Enter"] = KeyboardKey.Enter;
		((Table)((Table)lua.Globals["Input"])["code"])["Shift"] = KeyboardKey.LeftShift;
		((Table)((Table)lua.Globals["Input"])["code"])["Control"] = KeyboardKey.LeftControl;
		((Table)((Table)lua.Globals["Input"])["code"])["Backspace"] = KeyboardKey.Backspace;

		lua.Globals["Render"] = new Table(lua);
		((Table)lua.Globals["Render"])["width"] = display.width;
		((Table)lua.Globals["Render"])["height"] = display.height;
		((Table)lua.Globals["Render"])["fps"] = display.fps;
		((Table)lua.Globals["Render"])["setfps"] = (Action<int?>)SetFPS;
		((Table)lua.Globals["Render"])["init"] = (Action)display.Init;
		((Table)lua.Globals["Render"])["clear"] = (Action<int?>)Clear;
		((Table)lua.Globals["Render"])["setpixel"] = (Action<int?, int?, int?>)SetPixel;
		((Table)lua.Globals["Render"])["getpixel"] = (Func<int?, int?, int>)GetPixel;
		((Table)lua.Globals["Render"])["checkpixel"] = (Func<int?, int?, bool>)CheckPixel;
		((Table)lua.Globals["Render"])["fillrect"] = (Action<int?, int?, int?, int?, int?>)FillRect;
		((Table)lua.Globals["Render"])["checkrect"] = (Func<int?, int?, int?, int?, bool>)CheckRect;

		lua.Globals["Text"] = new Table(lua);
		((Table)lua.Globals["Text"])["draw"] = (Action<int?, int?, string?, int?>)DrawText;
	}
}