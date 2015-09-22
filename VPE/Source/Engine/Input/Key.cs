using System;
using OIK = OpenTK.Input.Key;

namespace VitPro.Engine {

	/// <summary>
	/// The available keyboard keys.
	/// </summary>
	
	public enum Key: int {
		
		/// <summary>A key outside the known keys.</summary>
		Unknown = OIK.Unknown,
		#region Modifiers
		/// <summary>The left shift key.</summary>
		ShiftLeft = OIK.ShiftLeft,
		/// <summary>The left shift key (equivalent to ShiftLeft).</summary>
		LShift = ShiftLeft,
		/// <summary>The right shift key.</summary>
		ShiftRight = OIK.ShiftRight,
		/// <summary>The right shift key (equivalent to ShiftRight).</summary>
		RShift = ShiftRight,
		/// <summary>The left control key.</summary>
		ControlLeft = OIK.ControlLeft,
		/// <summary>The left control key (equivalent to ControlLeft).</summary>
		LControl = ControlLeft,
		/// <summary>The right control key.</summary>
		ControlRight = OIK.ControlRight,
		/// <summary>The right control key (equivalent to ControlRight).</summary>
		RControl = ControlRight,
		/// <summary>The left alt key.</summary>
		AltLeft = OIK.AltLeft,
		/// <summary>The left alt key (equivalent to AltLeft.</summary>
		LAlt = AltLeft,
		/// <summary>The right alt key.</summary>
		AltRight = OIK.AltRight,
		/// <summary>The right alt key (equivalent to AltRight).</summary>
		RAlt = AltRight,
		/// <summary>The left win key.</summary>
		WinLeft = OIK.WinLeft,
		/// <summary>The left win key (equivalent to WinLeft).</summary>
		LWin = WinLeft,
		/// <summary>The right win key.</summary>
		WinRight = OIK.WinRight,
		/// <summary>The right win key (equivalent to WinRight).</summary>
		RWin = WinRight,
		/// <summary>The menu key.</summary>
		Menu = OIK.Menu,
		#endregion
		#region Function keys
		/// <summary>The F1 key.</summary>
		F1 = OIK.F1,
		/// <summary>The F2 key.</summary>
		F2 = OIK.F2,
		/// <summary>The F3 key.</summary>
		F3 = OIK.F3,
		/// <summary>The F4 key.</summary>
		F4 = OIK.F4,
		/// <summary>The F5 key.</summary>
		F5 = OIK.F5,
		/// <summary>The F6 key.</summary>
		F6 = OIK.F6,
		/// <summary>The F7 key.</summary>
		F7 = OIK.F7,
		/// <summary>The F8 key.</summary>
		F8 = OIK.F8,
		/// <summary>The F9 key.</summary>
		F9 = OIK.F9,
		/// <summary>The F10 key.</summary>
		F10 = OIK.F10,
		/// <summary>The F11 key.</summary>
		F11 = OIK.F11,
		/// <summary>The F12 key.</summary>
		F12 = OIK.F12,
		/// <summary>The F13 key.</summary>
		F13 = OIK.F13,
		/// <summary>The F14 key.</summary>
		F14 = OIK.F14,
		/// <summary>The F15 key.</summary>
		F15 = OIK.F15,
		/// <summary>The F16 key.</summary>
		F16 = OIK.F16,
		/// <summary>The F17 key.</summary>
		F17 = OIK.F17,
		/// <summary>The F18 key.</summary>
		F18 = OIK.F18,
		/// <summary>The F19 key.</summary>
		F19 = OIK.F19,
		/// <summary>The F20 key.</summary>
		F20 = OIK.F20,
		/// <summary>The F21 key.</summary>
		F21 = OIK.F21,
		/// <summary>The F22 key.</summary>
		F22 = OIK.F22,
		/// <summary>The F23 key.</summary>
		F23 = OIK.F23,
		/// <summary>The F24 key.</summary>
		F24 = OIK.F24,
		/// <summary>The F25 key.</summary>
		F25 = OIK.F25,
		/// <summary>The F26 key.</summary>
		F26 = OIK.F26,
		/// <summary>The F27 key.</summary>
		F27 = OIK.F27,
		/// <summary>The F28 key.</summary>
		F28 = OIK.F28,
		/// <summary>The F29 key.</summary>
		F29 = OIK.F29,
		/// <summary>The F30 key.</summary>
		F30 = OIK.F30,
		/// <summary>The F31 key.</summary>
		F31 = OIK.F31,
		/// <summary>The F32 key.</summary>
		F32 = OIK.F32,
		/// <summary>The F33 key.</summary>
		F33 = OIK.F33,
		/// <summary>The F34 key.</summary>
		F34 = OIK.F34,
		/// <summary>The F35 key.</summary>
		F35 = OIK.F35,
		#endregion
		#region Direction arrows
		/// <summary>The up arrow key.</summary>
		Up = OIK.Up,
		/// <summary>The down arrow key.</summary>
		Down = OIK.Down,
		/// <summary>The left arrow key.</summary>
		Left = OIK.Left,
		/// <summary>The right arrow key.</summary>
		Right = OIK.Right,
		#endregion
		#region Special keys
		/// <summary>The enter key.</summary>
		Enter = OIK.Enter,
		/// <summary>The escape key.</summary>
		Escape = OIK.Escape,
		/// <summary>The space key.</summary>
		Space = OIK.Space,
		/// <summary>The tab key.</summary>
		Tab = OIK.Tab,
		/// <summary>The backspace key.</summary>
		BackSpace = OIK.BackSpace,
		/// <summary>The backspace key (equivalent to BackSpace).</summary>
		Back = BackSpace,
		/// <summary>The insert key.</summary>
		Insert = OIK.Insert,
		/// <summary>The delete key.</summary>
		Delete = OIK.Delete,
		/// <summary>The page up key.</summary>
		PageUp = OIK.PageUp,
		/// <summary>The page down key.</summary>
		PageDown = OIK.PageDown,
		/// <summary>The home key.</summary>
		Home = OIK.Home,
		/// <summary>The end key.</summary>
		End = OIK.End,
		/// <summary>The caps lock key.</summary>
		CapsLock = OIK.CapsLock,
		/// <summary>The scroll lock key.</summary>
		ScrollLock = OIK.ScrollLock,
		/// <summary>The print screen key.</summary>
		PrintScreen = OIK.PrintScreen,
		/// <summary>The pause key.</summary>
		Pause = OIK.Pause,
		/// <summary>The num lock key.</summary>
		NumLock = OIK.NumLock,
		/// <summary>The clear key (Keypad5 with NumLock disabled, on typical keyboards).</summary>
		Clear = OIK.Clear,
		/// <summary>The sleep key.</summary>
		Sleep = OIK.Sleep,
		#endregion
		#region Keypad keys
		/// <summary>The keypad 0 key.</summary>
		Keypad0 = OIK.Keypad0,
		/// <summary>The keypad 1 key.</summary>
		Keypad1 = OIK.Keypad1,
		/// <summary>The keypad 2 key.</summary>
		Keypad2 = OIK.Keypad2,
		/// <summary>The keypad 3 key.</summary>
		Keypad3 = OIK.Keypad3,
		/// <summary>The keypad 4 key.</summary>
		Keypad4 = OIK.Keypad4,
		/// <summary>The keypad 5 key.</summary>
		Keypad5 = OIK.Keypad5,
		/// <summary>The keypad 6 key.</summary>
		Keypad6 = OIK.Keypad6,
		/// <summary>The keypad 7 key.</summary>
		Keypad7 = OIK.Keypad7,
		/// <summary>The keypad 8 key.</summary>
		Keypad8 = OIK.Keypad8,
		/// <summary>The keypad 9 key.</summary>
		Keypad9 = OIK.Keypad9,
		/// <summary>The keypad divide key.</summary>
		KeypadDivide = OIK.KeypadDivide,
		/// <summary>The keypad multiply key.</summary>
		KeypadMultiply = OIK.KeypadMultiply,
		/// <summary>The keypad subtract key.</summary>
		KeypadSubtract = OIK.KeypadSubtract,
		/// <summary>The keypad minus key (equivalent to KeypadSubtract).</summary>
		KeypadMinus = KeypadSubtract,
		/// <summary>The keypad add key.</summary>
		KeypadAdd = OIK.KeypadAdd,
		/// <summary>The keypad plus key (equivalent to KeypadAdd).</summary>
		KeypadPlus = KeypadAdd,
		/// <summary>The keypad decimal key.</summary>
		KeypadDecimal = OIK.KeypadDecimal,
		/// <summary>The keypad enter key.</summary>
		KeypadEnter = OIK.KeypadEnter,
		#endregion
		#region Letters
		/// <summary>The A key.</summary>
		A = OIK.A,
		/// <summary>The B key.</summary>
		B = OIK.B,
		/// <summary>The C key.</summary>
		C = OIK.C,
		/// <summary>The D key.</summary>
		D = OIK.D,
		/// <summary>The E key.</summary>
		E = OIK.E,
		/// <summary>The F key.</summary>
		F = OIK.F,
		/// <summary>The G key.</summary>
		G = OIK.G,
		/// <summary>The H key.</summary>
		H = OIK.H,
		/// <summary>The I key.</summary>
		I = OIK.I,
		/// <summary>The J key.</summary>
		J = OIK.J,
		/// <summary>The K key.</summary>
		K = OIK.K,
		/// <summary>The L key.</summary>
		L = OIK.L,
		/// <summary>The M key.</summary>
		M = OIK.M,
		/// <summary>The N key.</summary>
		N = OIK.N,
		/// <summary>The O key.</summary>
		O = OIK.O,
		/// <summary>The P key.</summary>
		P = OIK.P,
		/// <summary>The Q key.</summary>
		Q = OIK.Q,
		/// <summary>The R key.</summary>
		R = OIK.R,
		/// <summary>The S key.</summary>
		S = OIK.S,
		/// <summary>The T key.</summary>
		T = OIK.T,
		/// <summary>The U key.</summary>
		U = OIK.U,
		/// <summary>The V key.</summary>
		V = OIK.V,
		/// <summary>The W key.</summary>
		W = OIK.W,
		/// <summary>The X key.</summary>
		X = OIK.X,
		/// <summary>The Y key.</summary>
		Y = OIK.Y,
		/// <summary>The Z key.</summary>
		Z = OIK.Z,
		#endregion
		#region Numbers
		/// <summary>The number 0 key.</summary>
		Number0 = OIK.Number0,
		/// <summary>The number 1 key.</summary>
		Number1 = OIK.Number1,
		/// <summary>The number 2 key.</summary>
		Number2 = OIK.Number2,
		/// <summary>The number 3 key.</summary>
		Number3 = OIK.Number3,
		/// <summary>The number 4 key.</summary>
		Number4 = OIK.Number4,
		/// <summary>The number 5 key.</summary>
		Number5 = OIK.Number5,
		/// <summary>The number 6 key.</summary>
		Number6 = OIK.Number6,
		/// <summary>The number 7 key.</summary>
		Number7 = OIK.Number7,
		/// <summary>The number 8 key.</summary>
		Number8 = OIK.Number8,
		/// <summary>The number 9 key.</summary>
		Number9 = OIK.Number9,
		#endregion
		#region Symbols
		/// <summary>The tilde key.</summary>
		Tilde = OIK.Tilde,
		/// <summary>The minus key.</summary>
		Minus = OIK.Minus,
		//Equal,
		/// <summary>The plus key.</summary>
		Plus = OIK.Plus,
		/// <summary>The left bracket key.</summary>
		BracketLeft = OIK.BracketLeft,
		/// <summary>The left bracket key (equivalent to BracketLeft).</summary>
		LBracket = BracketLeft,
		/// <summary>The right bracket key.</summary>
		BracketRight = OIK.BracketRight,
		/// <summary>The right bracket key (equivalent to BracketRight).</summary>
		RBracket = BracketRight,
		/// <summary>The semicolon key.</summary>
		Semicolon = OIK.Semicolon,
		/// <summary>The quote key.</summary>
		Quote = OIK.Quote,
		/// <summary>The comma key.</summary>
		Comma = OIK.Comma,
		/// <summary>The period key.</summary>
		Period = OIK.Period,
		/// <summary>The slash key.</summary>
		Slash = OIK.Slash,
		/// <summary>The backslash key.</summary>
		BackSlash = OIK.BackSlash,
		#endregion
		/// <summary>Indicates the last available keyboard key.</summary>
		LastKey = OIK.LastKey
			
	}

}