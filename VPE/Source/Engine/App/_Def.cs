using System;
using log4net;

namespace VitPro.Engine {

	/// <summary>
	/// Provides core application methods.
	/// </summary>
	public static partial class App {
		
		static ILog log = LogManager.GetLogger(typeof(App));

		static bool Initialized = false;

		public static void Init() {
			if (Initialized)
				return;
			log4net.Config.BasicConfigurator.Configure();
			Initialized = true;
			log.Info("Initializing VPE");
			InitWindow();
			InitEvents();
			InitGL();
			VSync = false;
			log.Info("VPE initialized successfully");
		}

		static App() {
			Init();
		}

        public static double RunningTime {
            get { return timer.RunningTime; }
        }

	}

}
