// All code ©2017 Blueflame Digital Ltd. All rights reserved

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ActuateClient.Messages {
	public class ConfigureTelemTypeMessage : Message {
		private TelemetryType telemetryType = TelemetryType.VELOCITY;

		public ConfigureTelemTypeMessage(TelemetryType telemetryType) {
			this.messageType = MessageType.CONFIGURETELEMTYPE;
			this.telemetryType = telemetryType;
		}

		public override void WriteToTcpStream(StreamWriter streamWriter) {
			streamWriter.Write(ToString());
		}

		public override void WriteToUdpPacket(ref UdpPacket packet) {
			ResetUdpPacket(ref packet);
			packet.MessageType = (int)messageType;
			UdpPacketPayload_ConfigureTelemType payload = new UdpPacketPayload_ConfigureTelemType();
			payload.TelemetryType = (int)telemetryType;
			PackStruct(ref packet.Payload, payload);
		}

		public override void WriteToShmemMsg(ref ShmemMsg msg) {
			ResetShmemMsg(ref msg);
			msg.MessageType = (int)messageType;
			ShmemMsgPayload_ConfigureTelemType payload = new ShmemMsgPayload_ConfigureTelemType();
			payload.TelemetryType = (int)telemetryType;
			PackStruct(ref msg.Payload, payload);
		}


		public override string ToString() {
			return "";
		}
	}
}
