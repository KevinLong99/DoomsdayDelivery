// All code ©2022 Blueflame Digital Ltd. All rights reserved

using System;
using UnityEngine;
using ActuateClient;
using ActuateClient.Messages;

namespace Actuate {
	public class ActuateAgent : MonoBehaviour {
		public enum ClientType {
			UDP,
			SHARED_MEMORY,
			TCP
		}

		public ClientType clientType = ClientType.SHARED_MEMORY;
		public bool motionEnabled = false;
		public int ackTimeout = 250;
		private GameObject motionSource;
		private IActuateExtrasSource extrasSource;
		private ActuateClientBase ActuateClient;
		private Vector3 currentPosition;
		private Vector3 previousPosition;

		private TelemetryMessage telemetryMessage = new TelemetryMessage (
			0, 
			Quaternion.identity, 
			new Vector3()
		);

		private ActuateExtraData extraData = new ActuateExtraData {
			engineRpm = 0f,
			fuelLevel = 0f,
			temperature = 0f,
			tripCounter = 0f,
			gearIndicator = ' ',
			dashLights = (DashLights)0
		};

		void Awake() {
			Connect();
		}

		public void Connect() {
			if (!motionEnabled)
				return;
			
			FindSelectedActuateClient();
			if (ActuateClient == null)
				Debug.LogError("Unable to find selected ActuateClient");

			ActuateClient.Connect();
		}

		public void Disconnect() {
			if (!motionEnabled)
				return;
			
			SetMotionSource(null);
			ActuateClient.Disconnect();
		}

		private void FindSelectedActuateClient() {
			switch (clientType) {
				case ClientType.UDP:
					ActuateClient = GetComponentInChildren<ActuateUdpClient>();
					break;
				case ClientType.TCP:
					ActuateClient = GetComponentInChildren<ActuateTcpClient>();
					break;					
				default:
					ActuateClient = GetComponentInChildren<ActuateShmemClient>();
					break;
			}
		}

		public GameObject GetMotionSource() {
			return motionSource;
		}

		public void SetMotionSource(GameObject motionSource, IActuateExtrasSource extrasSource = null) {
			if (!motionEnabled)
				return;

			try {
				// stop posting data
				if (ActuateClient.PostAndAckMessage(new StopMessage(), ackTimeout) < 0)
					Debug.Log("Timeout posting Stop message");

			} 
			catch (DllNotFoundException) {
				LibraryNotFound();
				return;
			}
			catch (Exception) {}

			// switch source object(s)
			this.motionSource = motionSource;
			this.extrasSource = extrasSource;

			// no motionSource so we're done here
			if (this.motionSource == null)
				return;

			previousPosition = motionSource.transform.position;

			// tell the server that we're using velocity telemetry
			try {
				ConfigureTelemTypeMessage configureMessage = new ConfigureTelemTypeMessage(Message.TelemetryType.VELOCITY);
				if (ActuateClient.PostAndAckMessage(configureMessage, ackTimeout) < 0)
					Debug.Log("Timeout posting Reconfigure message");
			} 
			catch (DllNotFoundException) {
				LibraryNotFound();
				return;
			}
			catch (Exception ex) {
				Debug.Log(string.Format("Error sending Reconfigure message: {0}", ex.Message));
			}

			try {
				// start posting data
				if (ActuateClient.PostAndAckMessage(new StartMessage(), ackTimeout) < 0)
					Debug.Log("Timeout posting Start message");
			}
			catch (DllNotFoundException) {
				LibraryNotFound();
				return;
			}
			catch (Exception) { }
		}

		void FixedUpdate() {
			if (!motionEnabled)
				return;

			if (ActuateClient == null || motionSource == null)
				return;

			// collect extra telemetry data if a source is provided
			if (extrasSource != null)
				extraData = extrasSource.GetExtras();

			currentPosition = motionSource.transform.position;

			telemetryMessage.SetValues(
					Convert.ToUInt64((Time.fixedTime * 1000)),
					motionSource.transform.rotation, // world rotation
					(currentPosition - previousPosition) / Time.fixedDeltaTime,
					extraData.engineRpm,
					extraData.fuelLevel,
					extraData.temperature,
					extraData.tripCounter,
					extraData.gearIndicator,
				extraData.dashLights );

			ActuateClient.PostMessage(telemetryMessage);

			previousPosition = currentPosition;
		}

		void OnDestroy() {
			Disconnect();
		}

		private void LibraryNotFound() {
			Debug.LogWarning("Unable to find library, is Actuate installed?");
			Debug.LogWarning("Disabling Motion.");
			motionEnabled = false;
		}
	}
}
