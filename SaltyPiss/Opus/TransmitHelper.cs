using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Concentus.Enums;
using Concentus.Structs;
using SaltyPiss.Core;

namespace SaltyPiss.Opus {
	public static class TransmitHelper {

		/// <summary>
		/// Can be set by external code sources to report when something happens.
		/// </summary>
		public static Action<object> ProgressReportAction { get; set; }

		public const int SAMPLE_RATE = 48000;
		public const int BITRATE = 522240;
		public const int PACKETS_PER_DELAY_PHASE = 8;
		public const int PAGE_LENGTH = 960;
		public const int DELAY_TIME_NO_PHASE = PAGE_LENGTH / (SAMPLE_RATE / 1000);
		public const int DELAY_TIME = DELAY_TIME_NO_PHASE * PACKETS_PER_DELAY_PHASE;

		public const int CHANNELS = 2;
		public const OpusSignal SIGNAL_TYPE = OpusSignal.OPUS_SIGNAL_MUSIC;
		public const OpusApplication APPLICATION = OpusApplication.OPUS_APPLICATION_AUDIO;

		public static readonly OpusEncoder ENCODER = new OpusEncoder(SAMPLE_RATE, CHANNELS, APPLICATION) {
			Bitrate = BITRATE,
			SignalType = SIGNAL_TYPE
		};


		/// <summary>
		/// Given a media file, this will extract the pcm audio data from it with ffmpeg. Depending on the file, this may return from a cache.
		/// </summary>
		/// <returns></returns>
		public static void EncodeParallel(FileInfo mediaFile) {
			FileInfo cacheTarget = new FileInfo(mediaFile.FullName + "-OPUSPACKETS-" + PAGE_LENGTH);
			if (cacheTarget.Exists) {
				return;
			}

			OpusEncoder enc = new OpusEncoder(SAMPLE_RATE, CHANNELS, APPLICATION) {
				Bitrate = BITRATE,
				SignalType = SIGNAL_TYPE
			};

			short[] pcm = FFMPEG.GetPCM(mediaFile);
			List<byte[]> rawOpusPackets = new List<byte[]>();
			int originalLength = pcm.Length;

			List<short> newPcm = new List<short>(pcm);
			newPcm.AddRange(new short[PAGE_LENGTH * 2]);
			pcm = newPcm.ToArray();
			// ^ Fill pcm with blank 0 space to prevent an out of range exception lol

			for (int i = 0; i < originalLength; i += PAGE_LENGTH * 2) {
				byte[] data = new byte[PAGE_LENGTH];
				enc.Encode(pcm, i, PAGE_LENGTH, data, 0, PAGE_LENGTH);
				rawOpusPackets.Add(data);
			}
			WriteToCache(cacheTarget, rawOpusPackets);
		}

		public static List<byte[]> Encode(FileInfo mediaFile, bool skipStatusUpdates = false) {
			FileInfo cacheTarget = new FileInfo(mediaFile.FullName + "-OPUSPACKETS-" + PAGE_LENGTH);
			if (cacheTarget.Exists) {
				return GetFromCache(cacheTarget);
			}

			short[] pcm = FFMPEG.GetPCM(mediaFile);
			List<byte[]> rawOpusPackets = new List<byte[]>();
			if (!skipStatusUpdates) ProgressReportAction?.Invoke(1);
			int originalLength = pcm.Length;

			List<short> newPcm = new List<short>(pcm);
			newPcm.AddRange(new short[PAGE_LENGTH * 2]);
			pcm = newPcm.ToArray();
			// ^ Fill pcm with blank 0 space to prevent an out of range exception lol

			if (!skipStatusUpdates) Debug.WriteLine("Starting encoding loop...");
			int j = 0;
			for (int i = 0; i < originalLength; i += PAGE_LENGTH * 2) {
				byte[] data = new byte[PAGE_LENGTH];
				ENCODER.Encode(pcm, i, PAGE_LENGTH, data, 0, PAGE_LENGTH);
				if (j % 2000 == 0 && j != 0) {
					if (!skipStatusUpdates) ProgressReportAction?.Invoke((i, originalLength));
					if (!skipStatusUpdates) Debug.WriteLine($"{i} of {originalLength}");
				}
				j++;
				rawOpusPackets.Add(data);
			}
			if (!skipStatusUpdates) Debug.WriteLine("Finished encoding loop.");
			if (!skipStatusUpdates) ProgressReportAction?.Invoke(("", originalLength));
			if (!skipStatusUpdates) ProgressReportAction?.Invoke(2);
			WriteToCache(cacheTarget, rawOpusPackets);
			if (!skipStatusUpdates) ProgressReportAction?.Invoke(3);
			return rawOpusPackets;
		}

		private static List<byte[]> GetFromCache(FileInfo cacheTarget) {
			using BinaryReader reader = new BinaryReader(cacheTarget.OpenRead());
			int numPackets = reader.ReadInt32();
			List<byte[]> packets = new List<byte[]>(numPackets);
			for (int i = 0; i < numPackets; i++) {
				int length = reader.ReadInt32();
				byte[] packet = new byte[length];
				reader.Read(packet, 0, length);
				packets.Add(packet);
			}
			return packets;
		}

		private static void WriteToCache(FileInfo cacheTarget, List<byte[]> packets) {
			using BinaryWriter writer = new BinaryWriter(cacheTarget.OpenWrite());
			writer.Write(packets.Count);
			for (int i = 0; i < packets.Count; i++) {
				byte[] packet = packets[i];
				writer.Write(packet.Length);
				writer.Write(packet);
			}
		}

	}
}
