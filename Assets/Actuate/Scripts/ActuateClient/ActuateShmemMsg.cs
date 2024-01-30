// All code ©2017 Blueflame Digital Ltd. All rights reserved
using System.Runtime.InteropServices;

namespace ActuateClient {
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ShmemMsg {
		[MarshalAs(UnmanagedType.I4)]
		public int MessageType;

		[MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64)]
		public byte[] Payload;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ShmemMsgPayload_Telemetry {
		[MarshalAs(UnmanagedType.U8)]
		public ulong GameTicks;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public float[] Vector;

		[MarshalAs(UnmanagedType.R4)]
		public float EngineRpm;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public float[] Rotation;

		[MarshalAs(UnmanagedType.R4)]
		public float FuelLevel;

		[MarshalAs(UnmanagedType.R4)]
		public float Temperature;

		[MarshalAs(UnmanagedType.R4)]
		public float TripCounter;

		[MarshalAs(UnmanagedType.I1)]
		public byte GearIndicator;

		[MarshalAs(UnmanagedType.I1)]
		public byte DashLights;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ShmemMsgPayload_ConfigureTelemType {
		[MarshalAs(UnmanagedType.I4)]
		public int TelemetryType;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ShmemMsgPayload_StartEffect {
		[MarshalAs(UnmanagedType.I4)]
		public int EffectType;

		[MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64)]
		public byte[] Settings;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ShmemMsgPayload_StopEffect {
		[MarshalAs(UnmanagedType.I4)]
		public int EffectType;
	}
}
