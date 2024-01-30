// All code ©2017 Blueflame Digital Ltd. All rights reserved

using System;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using ActuateClient.Messages;

namespace ActuateClient {
	public class ActuateTcpClient : ActuateClientBase  {
		public string Host = "localhost";
		public int Port = 8080;
		private bool tcpClientReady = false;
		private TcpClient tcpClient;
		private StreamWriter streamWriter;
		private StreamReader streamReader;

		public override void Connect() {
			if (tcpClientReady)
				return;
			
			try {
				tcpClient = new TcpClient(Host, Port);
				NetworkStream networkStream = tcpClient.GetStream();
				streamWriter = new StreamWriter(networkStream);
				streamReader = new StreamReader(networkStream);
				tcpClientReady = true;
			} 
			catch (Exception e) {
				Debug.Log("Error setting up TcpClient for MotionServer:" + e);
			}
		}

		public override void PostMessage(Message message) {
			if (!tcpClientReady)
				return;

			try {
				message.WriteToTcpStream(streamWriter);
				streamWriter.Flush();
			} 
			catch (Exception e) {
				Debug.Log("Error posting, disconnecting :" + e);
				Disconnect();
			}			
		}

		public override int PostAndAckMessage(Message message, int timeout) {
			// not available for TCP, just post
			PostMessage(message);
			return 0;
		}

		public override void Disconnect() {
			if (!tcpClientReady)
				return;

			streamWriter.Close();
			streamReader.Close();
			tcpClient.Close();
			tcpClientReady = false;
		}
	}
}
