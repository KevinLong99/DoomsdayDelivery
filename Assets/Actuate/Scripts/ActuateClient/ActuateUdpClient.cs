// All code ©2017 Blueflame Digital Ltd. All rights reserved

using System;
using UnityEngine;
using System.Net.Sockets;
using ActuateClient.Messages;
using System.Runtime.InteropServices;

namespace ActuateClient {
	public class ActuateUdpClient : ActuateClientBase  {
		public string Host = "localhost";
		public int Port = 8080;
		public int maxFailCount = 10;
		private bool udpClientReady = false;
		private UdpClient udpClient;
		private int packetSize;
		private int failCount = 0;
		private UdpPacket packet;

		public override void Connect() {
			if (udpClientReady)
				return;
			
			packet = CreateUdpPacket();
			
			try {
				packetSize = Marshal.SizeOf(typeof(UdpPacket));
				udpClient = new UdpClient(Host, Port);
				failCount = 0;
				udpClientReady = true;
			} catch (Exception e) {
				Debug.Log("Error setting up UdpClient for MotionServer:" + e);
			}
		}

		public override void PostMessage(Message message) {
			if (!udpClientReady)
				return;

			// if maxFailCount messages have failed, then lets assume the server isn't there and stop trying
			if (failCount >= maxFailCount)
				return;

			try {
				message.WriteToUdpPacket(ref packet);
				udpClient.Send(PacketToByteArray(packet), packetSize);
			} catch (Exception) {
				failCount++;
			}
		}

		public override int PostAndAckMessage(Message message, int timeout) {
			// not available for UDP, just post
			PostMessage(message);
			return 0;
		}

		private UdpPacket CreateUdpPacket() {
			UdpPacket packet = new UdpPacket();
			packet.Payload = new byte[64];
			return packet;
		}

		private byte[] PacketToByteArray(UdpPacket packet) {
			byte[] bytes = new byte[packetSize];
			IntPtr ptr = Marshal.AllocHGlobal(packetSize);
			Marshal.StructureToPtr(packet, ptr, true);
			Marshal.Copy(ptr, bytes, 0, packetSize);
			Marshal.FreeHGlobal(ptr);
			return bytes;
		}

		public override void Disconnect() {
			if (!udpClientReady)
				return;

			udpClient.Close();
			udpClientReady = false;
		}
	}
}
