using System;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace VitPro.Engine {

	partial class Sound {
		
		const double ListenerZ = 1;
		const double RolloffFactor = 2;

		static Sound() {
			App.Init();
			audio = new AudioContext();
			AL.DistanceModel(ALDistanceModel.ExponentDistanceClamped);
			Listener = Vec2.Zero;
		}

		static AudioContext audio;

		static Vec2 _listen;

		/// <summary>
		/// Gets or sets the listener position.
		/// </summary>
		/// <value>The listener position.</value>
		public static Vec2 Listener {
			get { return _listen; }
			set {
				_listen = value;
				AL.Listener(ALListener3f.Position, (float)value.X, (float)value.Y, (float)ListenerZ);
			}
		}

	}

}
