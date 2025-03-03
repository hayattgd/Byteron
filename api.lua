Shell = {
	---Prints out text to shell console
	---Color must be 0..15
	---@param text string
	---@param color integer
	print = function (text, color) end,

	---Runs command.
	---@param command string
	run = function (command) end,

	---Makes shell stop working
	stopupdate = function () end,

	---Makes shell start working
	startupdate = function () end,
}

Input = {
	---Get if key is pressed down.
	---@param keycode integer
	---@return integer
	key = function (keycode) return 0 end,

	---Get if key just pressed right now.
	---@param keycode integer
	---@return integer
	keydown = function (keycode) return 0 end,

	---Get if key just released right now.
	---@param keycode integer
	---@return integer
	keyup = function (keycode) return 0 end,

	---Gets a signal of key type (including repeats).
	---@param keycode integer
	---@return integer
	keyrepeat = function (keycode) return 0 end,

	---keycode
	code = {
		A = 0,
		B = 0,
		C = 0,
		D = 0,
		E = 0,
		F = 0,
		G = 0,
		H = 0,
		I = 0,
		J = 0,
		K = 0,
		L = 0,
		M = 0,
		N = 0,
		O = 0,
		P = 0,
		Q = 0,
		R = 0,
		S = 0,
		T = 0,
		U = 0,
		V = 0,
		W = 0,
		X = 0,
		Y = 0,
		Z = 0,
		One = 0,
		Two = 0,
		Three = 0,
		Four = 0,
		Five = 0,
		Six = 0,
		Seven = 0,
		Eight = 0,
		Nine = 0,
		Zero = 0,
		Space = 0,
		Up = 0,
		Left = 0,
		Down = 0,
		Right = 0,
		Enter = 0,
		Shift = 0,
		Control = 0,
	}
}

Render = {
	---Represents current width of screen.
	width = 256,

	---Represents current height of screen.
	height = 144,

	---Represents current fps.
	fps = 60,

	---Initalize renderer.
	init = function () end,

	---Fills entire screen with [color].
	---Color must be 0..15
	---@param color integer
	clear = function (color) end,

	---Sets specified pixel with color.
	---Color must be 0..15
	---@param x integer
	---@param y integer
	---@param color integer
	setpixel = function (x, y, color) end,

	---Gets color of specified position.
	---@param x integer
	---@param y integer
	---@return integer
	getpixel = function (x, y) return 0 end,

	---Check if pixel is inside screen.
	---@param x integer
	---@param y integer
	---@return boolean
	checkpixel = function (x, y) return false end,

	---Fills rectangle with specified position, size, color.
	---@param x integer
	---@param y integer
	---@param w integer
	---@param h integer
	---@param color integer
	fillrect = function (x, y, w, h, color) end,

	---Check if rectangle is inside screen.
	---@param x integer
	---@param y integer
	---@param w integer
	---@param h integer
	---@return integer
	checkrect = function (x, y, w, h) return 0 end,
}

Text = {
	---Draws text on specified position and color.
	---Color must be 0..15
	---@param x integer
	---@param y integer
	---@param text string
	---@param color integer
	draw = function (x, y, text, color) end,
}