// All code ©2017 Blueflame Digital Ltd. All rights reserved
using ActuateClient.Messages;

namespace Actuate {
	public struct ActuateExtraData {
		public float engineRpm;
		public float fuelLevel;
		public float temperature;
		public float tripCounter;
		public char gearIndicator;
		public DashLights dashLights;
	};

	public interface IActuateExtrasSource {
		 ActuateExtraData GetExtras();
	}
}
