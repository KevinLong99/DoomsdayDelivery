// All code ©2017 Blueflame Digital Ltd. All rights reserved

using ActuateClient.Messages;
using UnityEngine;

namespace ActuateClient {
	public abstract class ActuateClientBase : MonoBehaviour {
		public abstract void Connect();
		public abstract void Disconnect();
		public abstract void PostMessage(Message message);
		public abstract int PostAndAckMessage(Message message, int timeout);
	}
}
