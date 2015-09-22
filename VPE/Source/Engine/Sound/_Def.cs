using System;
using System.Collections.Generic;
using System.IO;
using OpenTK.Audio.OpenAL;

namespace VitPro.Engine {

	/// <summary>
	/// Sound.
	/// </summary>
	public partial class Sound {
		
		int id;

		int channels, bits_per_sample, sample_rate;
		byte[] sound_data;

		List<int> srcs;

		int i;
		const int MaxSources = 20;

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Engine.Sound"/> class.
		/// </summary>
		/// <param name="path">Path.</param>
		public Sound(string path)
		{
			sound_data = LoadWave(File.Open(path, FileMode.Open),
				out channels, out bits_per_sample, out sample_rate);
			Init(new System.Runtime.Serialization.StreamingContext());
		}

		[System.Runtime.Serialization.OnDeserialized]
		void Init(System.Runtime.Serialization.StreamingContext context) {
			id = AL.GenBuffer();
			AL.BufferData(id, GetSoundFormat(channels, bits_per_sample),
				sound_data, sound_data.Length, sample_rate);
			srcs = new List<int>();
			i = 0;
		}

		/// <summary>
		/// Play the sound.
		/// </summary>
		/// <param name="volume">Volume.</param>
		/// <param name="looped">If true, sound will be looped.</param>
		public void Play(double volume = 1, bool looped = false) {
			var src = GenSrc();
			AL.Source(src, ALSourcei.Buffer, id);
			AL.Source(src, ALSourceb.Looping, looped);
			AL.Source(src, ALSourcef.Gain, (float)volume);
			AL.SourcePlay(src);
		}

		/// <summary>
		/// Play the sound at the specified position.
		/// </summary>
		/// <param name="pos">Position.</param>
		/// <param name="volume">Volume.</param>
		public void PlayAt(Vec2 pos, double volume = 1) {
			PlayAt(pos.X, pos.Y, volume);
		}

		/// <summary>
		/// Play the sound at the specified position.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="volume">Volume.</param>
		public void PlayAt(double x, double y, double volume = 1) {
			var src = GenSrc();
			//AL.Source(src, ALSourcef.ReferenceDistance, (float)ListenerZ);
			AL.Source(src, ALSourcef.RolloffFactor, (float)RolloffFactor);
			AL.Source(src, ALSource3f.Position, (float)x, (float)y, 0);
			AL.Source(src, ALSourcei.Buffer, id);
			AL.Source(src, ALSourcef.Gain, (float)volume);
			AL.SourcePlay(src);
		}

		int GenSrc() {
			var src = AL.GenSource();
			if (i < srcs.Count)
			{
				AL.DeleteSource(srcs[i]);
				srcs[i] = src;
			}
			else
				srcs.Add(src);
			i = (i + 1) % MaxSources;
			return src;
		}

		/// <summary>
		/// Stop playing the sound.
		/// </summary>
		public void Stop() {
			foreach (var src in srcs)
			{
				AL.SourceStop(src);
				AL.DeleteSource(src);
			}
			srcs.Clear();
			i = 0;
		}

	}

}