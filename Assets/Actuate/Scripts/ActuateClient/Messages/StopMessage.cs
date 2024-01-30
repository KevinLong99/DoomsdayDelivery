// All code ©2017 Blueflame Digital Ltd. All rights reserved

namespace ActuateClient.Messages {
	public class StopMessage : Message {
		public StopMessage() {
			this.messageType = MessageType.STOP;
		}
	}
}

