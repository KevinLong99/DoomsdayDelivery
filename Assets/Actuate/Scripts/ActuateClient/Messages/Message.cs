// All code ©2022 Blueflame Digital Ltd. All rights reserved

using System.IO;
using System.Runtime.InteropServices;
using System;

namespace ActuateClient.Messages {
	public abstract class Message {
		public enum MessageType {
			START = 0,
			STOP = 1,
			TELEMETRY = 2,
			CONFIGURETELEMTYPE = 3,
			START_EFFECT = 4,
			STOP_EFFECT = 5
		}

		public enum TelemetryType {
			VELOCITY = 0,
			ACCELERATION = 1
		}

		protected MessageType messageType = MessageType.START;

		public virtual void WriteToTcpStream(StreamWriter streamWriter) {
			streamWriter.Write(string.Format ("{0:D}|", messageType));
		}

		protected void ResetUdpPacket(ref UdpPacket packet) {
			packet.MessageType = (int)MessageType.TELEMETRY;
			Array.Clear(packet.Payload, 0, packet.Payload.Length);
		}

		public virtual void WriteToUdpPacket(ref UdpPacket packet) {
			ResetUdpPacket(ref packet);
			packet.MessageType = (int)messageType;
		}

		protected void ResetShmemMsg(ref ShmemMsg msg) {
			msg.MessageType = (int)MessageType.TELEMETRY;
			Array.Clear(msg.Payload, 0, msg.Payload.Length);
		}

		public virtual void WriteToShmemMsg(ref ShmemMsg msg) {
			ResetShmemMsg(ref msg);
			msg.MessageType = (int)messageType;
		}

		protected void PackStruct(ref byte[] dst, object src) {
			int structSize = Marshal.SizeOf(src);
			if (structSize > 64)
				throw new Exception("payload too large");

			IntPtr ptr = Marshal.AllocHGlobal(structSize);
			Marshal.StructureToPtr(src, ptr, true);
			Marshal.Copy(ptr, dst, 0, structSize);
			Marshal.FreeHGlobal(ptr);
		}

		public override string ToString() {
			return string.Format ("{0:D} |", messageType);
		}
	}
}
