using System;
using log4net;
using OpenTK;

namespace VitPro.Engine {
	
	partial class App {
		
		internal static GameWindow window;

		static void InitWindow() {
			System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(true); // Needed for fonts?
			var flags = OpenTK.Graphics.GraphicsContextFlags.Default;
#if DEBUG
			log.Info("Using DEBUG mode for OpenGL");
			flags |= OpenTK.Graphics.GraphicsContextFlags.Debug;
#endif
			log.Info("Creating window");
			window = new GameWindow(
				640, 480,
				OpenTK.Graphics.GraphicsMode.Default, 
				"VPE Application",
				GameWindowFlags.Default,
				DisplayDevice.Default,
				0, 0, flags, null);
		}

		/// <summary>
		/// Gets or sets a value indicating whether vertical synchronization is on.
		/// </summary>
		/// <value><c>true</c> if VSync is on; otherwise, <c>false</c>.</value>
		public static bool VSync {
			get { return window.VSync == VSyncMode.On; }
			set { window.VSync = value ? VSyncMode.On : VSyncMode.Off; }
		}

		/// <summary>
		/// Gets or sets window title.
		/// </summary>
		/// <value>Title.</value>
		public static string Title {
			get { 
				return window.Title; 
			}
			set {
				log.Info(string.Format("Changing window title to \"{0}\"", value));
				window.Title = value; 
			}
		}

		/// <summary>
		/// Gets window's width.
		/// </summary>
		/// <value>The width.</value>
		public static int Width { get { return window.Width; } }

		/// <summary>
		/// Gets window's height.
		/// </summary>
		/// <value>The height.</value>
		public static int Height { get { return window.Height; } }

		/// <summary>
		/// Gets or sets windw's size.
		/// </summary>
		/// <value>The size.</value>
		public static Vec2i Size {
			get {
				return new Vec2i(Width, Height); 
			}
			set {
				window.Size = new System.Drawing.Size(value.X, value.Y);
			}
		}

		/// <summary>
		/// Gets the aspect of the app window.
		/// </summary>
		/// <value>The aspect.</value>
		public static double Aspect {
			get { return (double) Width / Height; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether application is fullscreen.
		/// </summary>
		/// <value><c>true</c> if fullscreen; otherwise, <c>false</c>.</value>
		public static bool Fullscreen {
			get { return window.WindowState == WindowState.Fullscreen; }
			set {
				if (value) {
					log.Info("Switching to fullscreen mode");
					window.WindowState = WindowState.Fullscreen;
				} else {
					log.Info("Switching to windowed mode");
					window.WindowState = WindowState.Normal;
				}
			}
		}

	}

}
