using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using SaltyPiss.Opus.Data;

namespace SaltyPiss.Opus {

	/// <summary>
	/// A wrapper around the opus DLL.
	/// </summary>
	/// <remarks>
	/// As you can clearly tell, this is the "piss" portion of SaltyPiss. Get it? Opus sorta sounds like "oh piss"?<para/><para/><para/>
	/// Please laugh.
	/// </remarks>
	[Obsolete("This system does not work properly.")] public class OpusWrapper : IDisposable {

		#region DLL

		private const string OPUS = @".\libs\opus.dll";
#pragma warning disable IDE1006 // Naming Styles

		/// <summary>
		/// Initializes a new OpusEncoder. Returns a pointer to the encoder.
		/// </summary>
		/// <param name="Fs">Sampling rate of input signal (Hz) This must be one of 8000, 12000, 16000, 24000, or 48000.</param>
		/// <param name="channels">Number of channels (1 or 2) in input signal</param>
		/// <param name="application">Coding mode (OPUS_APPLICATION_VOIP/OPUS_APPLICATION_AUDIO/OPUS_APPLICATION_RESTRICTED_LOWDELAY)</param>
		/// <param name="err"><see href="https://opus-codec.org/docs/opus_api-1.3.1/group__opus__errorcodes.html">Error codes</see></param>
		/// <returns></returns>
		[DllImport(OPUS, CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		private static extern IntPtr opus_encoder_create(int Fs, int channels, int application, out IntPtr err);

		/// <summary>
		/// Frees resources created by opus_encoder_create
		/// </summary>
		/// <param name="encoder"></param>
		[DllImport(OPUS, CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		private static extern void opus_encoder_destroy(IntPtr encoder);

		/// <summary>
		/// Performs a CTL (control) function on an Opus encoder.
		/// </summary>
		/// <param name="encoder"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[DllImport(OPUS, CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		private static extern int opus_encoder_ctl(IntPtr encoder, params int[] request);

		/// <summary>
		/// Encodes audio.
		/// </summary>
		/// <param name="encoder"></param>
		/// <param name="pcm"></param>
		/// <param name="frame_size"></param>
		/// <param name="data"></param>
		/// <param name="maxDataLength"></param>
		/// <returns></returns>
		[DllImport(OPUS, CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		private static unsafe extern int opus_encode(IntPtr encoder, IntPtr pcm, int frame_size, IntPtr data, int maxDataLength);
		//private static extern int opus_encode(IntPtr encoder, short[] pcm, int frame_size, byte[] data, int maxDataLength);
#pragma warning restore IDE1006 // Naming Styles

		#endregion

		private readonly IntPtr OpusEncoder;

		/// <summary>
		/// The number of channels.
		/// </summary>
		public int Channels { get; }

		/// <summary>
		/// The sample rate of input audio being encoded.
		/// </summary>
		public int InputSampleRate { get; }

		/// <summary>
		/// The mode of encoding.
		/// </summary>
		public OpusApplication Mode { get; }

		/// <summary>
		/// The frame size.
		/// </summary>
		public int FrameSize { get; }

		/// <summary>
		/// Create a new wrapper.
		/// </summary>
		/// <param name="sampleRate"></param>
		/// <param name="channels"></param>
		/// <param name="application"></param>
		/// <param name="frameSize"></param>
		/// <exception cref="OpusException"></exception>
		public OpusWrapper(OpusSampleRate sampleRate = OpusSampleRate.Rate48000, OpusChannels channels = OpusChannels.Stereo, OpusApplication application = OpusApplication.OPUS_APPLICATION_AUDIO, OpusFrameSize frameSize = OpusFrameSize.Frame120) {
			IntPtr opusEncoder = opus_encoder_create((int)sampleRate, (int)channels, (int)application, out IntPtr err);
			OpusError oerr = (OpusError)err.ToInt32();
			if (oerr != OpusError.OPUS_OK) {
				throw new OpusException(oerr);
			}
			// opus_encoder_ctl(opusEncoder, 4002, 32000); // OPUS_SET_BITRATE_REQUEST 
			opus_encoder_ctl(opusEncoder, 4014, 25); // OPUS_SET_PACKET_LOSS_PERC_REQUEST 
			opus_encoder_ctl(opusEncoder, 4002, 192000); // OPUS_SET_BITRATE_REQUEST
			opus_encoder_ctl(opusEncoder, 4012, 1); // OPUS_SET_INBAND_FEC_REQUEST
			opus_encoder_ctl(opusEncoder, 4024, 3002); // OPUS_SET_SIGNAL_REQUEST = MUSIC

			OpusEncoder = opusEncoder;
			Channels = (int)channels;
			InputSampleRate = (int)sampleRate;
			Mode = application;
			FrameSize = (int)frameSize;
		}

		/// <summary>
		/// Encodes an Opus frame by grabbing a segment out of pcm. To encode the entirety of some pcm data, use <see cref="EncodeEntirePCM(short[], OpusFrameSize)"/>
		/// </summary>
		/// <param name="pcm">Input signal (interleaved if 2 channels). Its length should be <paramref name="frameSize"/>*<see cref="Channels"/>*<see langword="sizeof"/>(<see cref="short"/>)</param>
		/// <param name="data">Output payload. This must contain storage for at least maxDataLength.</param>
		/// <param name="maxDataLength">Size of the allocated memory for the output payload. This may be used to impose an upper limit on the instant bitrate, but should not be used as the only bitrate control. Use OPUS_SET_BITRATE to control the bitrate.</param>
		/// <returns>The amount of bytes written, or a negative value that can be cast into <see cref="OpusError"/></returns>
		public unsafe int EncodeFrame(ArraySegment<short> pcm, out byte[] data) {
			/*
			short[] pcmSeg = pcm.ToArray();
			data = new byte[pcmSeg.Length];
			return opus_encode(OpusEncoder, pcmSeg, FrameSize, data, data.Length);
			*/
			short[] pcmSegAr = pcm.ToArray();
			fixed (short* pcmSeg = pcmSegAr) {
				fixed (byte* dat = new byte[pcmSegAr.Length]) {
					data = new byte[pcmSegAr.Length];
					int v = opus_encode(OpusEncoder, new IntPtr(pcmSeg), FrameSize, new IntPtr(dat), pcmSegAr.Length);
					for (int i = 0; i < data.Length; i++) {
						dat[i] = data[i];
					}
					return v;
				}
			}
			
		}
		
		/// <summary>
		/// Takes an entire audio file and encodes it to a number of packets.
		/// </summary>
		/// <param name="pcm"></param>
		/// <returns></returns>
		public List<byte[]> EncodeEntirePCM(short[] pcm) {
			List<byte[]> packets = new List<byte[]>();
			int nPackets = pcm.Length / FrameSize;
			Debug.WriteLine($"Writing {nPackets} opus packets...");
			int c = 0;
			for (int idx = 0; idx < pcm.Length; ) {
				int desiredCount = FrameSize * Channels;
				int count = Math.Min(desiredCount, pcm.Length - idx);
				if (count < desiredCount) {
					return packets;
				}
				//int count = Math.Min(FrameSize, pcm.Length - idx);
				ArraySegment<short> segment = new ArraySegment<short>(pcm, idx, count);
				int written = EncodeFrame(segment, out byte[] packet);
				if (written < 0) {
					throw new OpusException((OpusError)written);
				}
				packets.Add(packet);
				// idx += FrameSize * Channels;
				idx += count;
				c++; // MMM. Funni joke
				if (c % 100 == 0) {
					Debug.WriteLine($"{c} of {nPackets}...");
				}
			}
			return packets;
		}

		public void Dispose() {
			opus_encoder_destroy(OpusEncoder);
		}
	}
}
