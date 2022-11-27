using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SaltyPiss.Core {

	/// <summary>
	/// A lazy wrapper for ffmpeg.
	/// </summary>
	public static class FFMPEG {

		/// <summary>
		/// Given an audio or video file, this will extract the pcm audio data from the file.
		/// </summary>
		/// <param name="mediaFile"></param>
		/// <returns></returns>
		public static short[] GetPCM(FileInfo mediaFile) {
			Debug.WriteLine("Grabbing PCM audio data...");
			FileInfo pcmFile = new FileInfo(mediaFile.FullName.Replace(mediaFile.Extension, ".pcm"));
			if (pcmFile.Exists) {
				Debug.WriteLine("Found pre-cached pcm data. Converting to short array...");
				return ShortFromBytes(File.ReadAllBytes(pcmFile.FullName));
			}
			ProcessStartInfo ffmpeg_inf = new ProcessStartInfo {
				FileName = "ffmpeg",
				Arguments = $"-i \"{mediaFile.FullName}\" -ac 2 -ar 48000 -f s16le pipe:1",
				CreateNoWindow = true,
				RedirectStandardOutput = true
			};
			Process ffmpeg = Process.Start(ffmpeg_inf);
			//ffmpeg.WaitForExit();
			//return ShortFromBytes(File.ReadAllBytes(pcmFile.FullName));
			Debug.WriteLine("Started ffmpeg, beginning copy process...");
			using Stream ffout = ffmpeg.StandardOutput.BaseStream;
			using MemoryStream mstr = new MemoryStream();
			ffout.CopyTo(mstr);
			mstr.Position = 0;
			Debug.WriteLine("Done, converting into short array...");
			return ShortFromBytes(mstr.ToArray());
		}

		public static short[] ShortFromBytes(byte[] bytes) {
			if (bytes.Length % 2 != 0) throw new ArgumentException("Byte array does not have an even number of elements.");
			short[] data = new short[bytes.Length / 2];
			Buffer.BlockCopy(bytes, 0, data, 0, bytes.Length);
			return data;
			/*
			short[] data = new short[bytes.Length / 2];
			for (int i = 0; i < bytes.Length; i += 2) {
				data[i / 2] = BitConverter.ToInt16(bytes, i);
			}
			return data;
			*/
		}

	}
}
