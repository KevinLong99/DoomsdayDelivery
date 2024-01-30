// All code ©2017 Blueflame Digital Ltd. All rights reserved

using UnityEngine;
using System.IO;
using System;

namespace ActuateClient.Messages {
	[Flags]
	public enum DashLights : byte {
		LeftIndicatorLight = 1,
		RightIndicatorLight = 2,
		OilLight = 4,
		BrakeSystemLight = 8,
		BatteryLight = 16,
		EspLight = 32,
		DippedBeamLight = 64,
		MainBeamLight = 128
	}

	public class TelemetryMessage : Message {
		private ulong gameTicks;
		private Vector3 vector;
		private float engineRpm;
		private Quaternion rotation;
		private float fuelLevel;
		private float temperature;
		private float tripCounter;
		private char gearIndicator;
		private DashLights dashLights;

		public TelemetryMessage(
				ulong gameTicks,
				Quaternion rotation, 
				Vector3 vector, 
				float engineRpm = 0,
				float fuelLevel = 0,
				float temperature = 0,
				float tripCounter = 0,
				char gearIndicator = ' ',
				DashLights dashLights = 0) {
			this.messageType = MessageType.TELEMETRY;
			this.gameTicks = gameTicks;
			this.rotation = rotation;
			this.vector = vector;
			this.engineRpm = engineRpm;
			this.fuelLevel = fuelLevel;
			this.temperature = temperature;
			this.tripCounter = tripCounter;
			this.gearIndicator = gearIndicator;
			this.dashLights = dashLights;
		}

		public void SetValues (
				ulong gameTicks,
				Quaternion rotation,
				Vector3 vector,
				float engineRpm = 0,
				float fuelLevel = 0,
				float temperature = 0,
				float tripCounter = 0,
				char gearIndicator = ' ',
				DashLights dashLights = 0 ) {
			this.messageType = MessageType.TELEMETRY;
			this.gameTicks = gameTicks;
			this.rotation = rotation;
			this.vector = vector;
			this.engineRpm = engineRpm;
			this.fuelLevel = fuelLevel;
			this.temperature = temperature;
			this.tripCounter = tripCounter;
			this.gearIndicator = gearIndicator;
			this.dashLights = dashLights;
		}

		public override void WriteToTcpStream(StreamWriter streamWriter) {
			streamWriter.Write(ToString());
		}

		public override void WriteToUdpPacket (ref UdpPacket packet) {
			ResetUdpPacket(ref packet);
			packet.MessageType = (int)messageType;
			UdpPacketPayload_Telemetry payload = new UdpPacketPayload_Telemetry();
			payload.Vector = new float[3];
			payload.Rotation = new float[4];
			payload.GameTicks = gameTicks;
			payload.Vector[0] = vector.x;
			payload.Vector[1] = vector.y;
			payload.Vector[2] = vector.z;
			payload.EngineRpm = engineRpm;
			payload.Rotation[0] = rotation.w;
			payload.Rotation[1] = rotation.x;
			payload.Rotation[2] = rotation.y;
			payload.Rotation[3] = rotation.z;
			payload.FuelLevel = fuelLevel;
			payload.Temperature = temperature;
			payload.TripCounter = tripCounter;
			payload.GearIndicator =  Convert.ToByte(gearIndicator);
			payload.DashLights = (byte)dashLights;
			PackStruct(ref packet.Payload, payload);
		}

		public override void WriteToShmemMsg(ref ShmemMsg msg) {
			ResetShmemMsg(ref msg);
			msg.MessageType = (int)messageType;
			ShmemMsgPayload_Telemetry payload = new ShmemMsgPayload_Telemetry();
			payload.Vector = new float[3];
			payload.Rotation = new float[4];
			payload.GameTicks = gameTicks;
			payload.Vector[0] = vector.x;
			payload.Vector[1] = vector.y;
			payload.Vector[2] = vector.z;
			payload.EngineRpm = engineRpm;
			payload.Rotation[0] = rotation.w;
			payload.Rotation[1] = rotation.x;
			payload.Rotation[2] = rotation.y;
			payload.Rotation[3] = rotation.z;
			payload.FuelLevel = fuelLevel;
			payload.Temperature = temperature;
			payload.TripCounter = tripCounter;
			payload.GearIndicator =  Convert.ToByte(gearIndicator);
			payload.DashLights = (byte)dashLights;
			PackStruct(ref msg.Payload, payload);
		}

		public override string ToString() {
			string m = string.Format ("{0:D}/{1:D}/{2:N6}/{3:N6}/{4:N6}/{5:N6}/{6:N6}/{7:N6}/{8:N6}/{9:N6}/{10:N6}/{11:N6}/{12:N6}/{13}/{14:D}/|", 
				messageType,
				gameTicks,
				vector.x,
				vector.y,
				vector.z,
				engineRpm,
				rotation.w,
				rotation.x,
				rotation.y,
				rotation.z,
				fuelLevel,
				temperature,
				tripCounter,
				gearIndicator,
				(byte)dashLights);

			return m;
		}
	}
}
