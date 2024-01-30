// All code ©2017 Blueflame Digital Ltd. All rights reserved
/*
using System;
using System.IO;
using System.Runtime.InteropServices;
using ActuateClient.CustomData;

namespace ActuateClient.Messages {
	public abstract class EffectMessage : Message {
		public enum EffectType {
			RandomVerticalAcc,
			Impact
		}

		protected byte[] customData;

		public EffectMessage() {
			this.messageType = MessageType.START_EFFECT;
		}

		protected void PackEffectCustomData(EffectType effectType, object settingsDataStruct) {
			EffectCustomData effectCustomData = new EffectCustomData();

			effectCustomData.EffectType = (int)effectType;

			// pack settings struct into settings field
			int structSize = Marshal.SizeOf(settingsDataStruct);
			if (structSize > 60)
				throw new Exception("settings structure too large");

			effectCustomData.Settings = new byte[structSize];
			IntPtr ptr = Marshal.AllocHGlobal(structSize);
			Marshal.StructureToPtr(settingsDataStruct, ptr, true);
			Marshal.Copy(ptr, effectCustomData.Settings, 0, structSize);
			Marshal.FreeHGlobal(ptr);

			// pack effects message into customData
			structSize = Marshal.SizeOf(effectCustomData);
			if (structSize > 64)
				throw new Exception("custom data structure too large");

			customData = new byte[structSize];
			ptr = Marshal.AllocHGlobal(structSize);
			Marshal.StructureToPtr(effectCustomData, ptr, true);
			Marshal.Copy(ptr, customData, 0, structSize);
			Marshal.FreeHGlobal(ptr);
		}

		public override void WriteToTcpStream(StreamWriter streamWriter) {
			streamWriter.Write(ToString());
		}

		public override void WriteToUdpPacket(ref ActuateUdpPacket packet) {
			ResetUdpPacket(ref packet);
			packet.MessageType = (int)messageType;
			packet.CustomData = customData;
		}

		public override void WriteToShmemMsg(ref ActuateShmemMsg msg) {
			ResetShmemMsg(ref msg);
			msg.MessageType = (int)messageType;
			//msg.CustomData = customData;
		}

		public override string ToString() {
			return "";
		}
	}
}
*/
