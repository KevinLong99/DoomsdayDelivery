// All code ©2017 Blueflame Digital Ltd. All rights reserved
using ActuateClient.Messages;
using System.Runtime.InteropServices;
using System.Security;

namespace ActuateClient {
	public class ActuateShmemClient : ActuateClientBase  {
		[SuppressUnmanagedCodeSecurity]
		[DllImport("libshmemclient", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Send")]
		public static extern void Send(ref ShmemMsg msg);

		[SuppressUnmanagedCodeSecurity]
		[DllImport("libshmemclient", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SendAndAck")]
		public static extern int SendAndAck(ref ShmemMsg msg, int timeout);

		private ShmemMsg shmemMsg;

		public override void Connect() {
			shmemMsg = CreateShmemMsg();
		 }

		public override void PostMessage(Message message) {
			message.WriteToShmemMsg(ref shmemMsg);
			Send(ref shmemMsg);
		}

		public override int PostAndAckMessage(Message message, int timeout) {
			message.WriteToShmemMsg(ref shmemMsg);
			return SendAndAck(ref shmemMsg, timeout);
		}

		private ShmemMsg CreateShmemMsg() {
			ShmemMsg msg = new ShmemMsg();
			msg.Payload = new byte[64];
			return msg;
		}

		public override void Disconnect() { }
	}
}
