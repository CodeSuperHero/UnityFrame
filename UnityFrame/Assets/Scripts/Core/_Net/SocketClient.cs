using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System;
using System.IO;
using System.Text;

namespace CodeSuperHero.UF.Net
{
	public class SocketClient 
	{
		private Socket _socket;	

		private MemoryStream _stream;
		private BinaryReader _reader;

		private string _address;

		public string address { get { return _address; } }

		private string _port;

		public string port { get { return _port; } }

		private byte[] _receiveBuffer;		//用于读取socket缓冲区的数组
		private byte[] _sendBuffer;			//用于发送socket的数组

		public const int MAX_LENGTH 	= 2048;	//_receiveBuffer的最大长度
		public const int PACKAGE_LENGTH = 4;	//包长度是Int32型，固定占用4个byte长度

		public bool isConnected
		{
			get{ return _socket.Connected; }
		}

		public SocketClient(ProtocolType type) 
		{
			_receiveBuffer = new byte[MAX_LENGTH];
			_sendBuffer = new byte[MAX_LENGTH];

			_socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, type);
			_stream = new MemoryStream ();
			_reader = new BinaryReader (_stream);
		}

		public void Connect(IPAddress adress, int port)
		{
			if (isConnected) 
			{
				DebugLog.instance.LogError (string.Format ("Socket is connected. address : {0}. port : {1}", _address, _port));
				return;
			}
			_address = adress.ToString();
			_port = port.ToString();
			_socket.BeginConnect(adress, port, EndConnect, null);
		}

		public void Connect(string ip, int port)
		{
			if (isConnected) 
			{
				DebugLog.instance.LogError (string.Format ("Socket is connected. address : {0}. port : {1}", _address, _port));
				return;
			}

			IPAddress adress = null;
			if (!IPAddress.TryParse (ip, out adress)) 
			{
				throw new Exception ("IP string is error!");
				return;
			}
			Connect (adress, port);
		}

		public void ConnectByHost(string host, int port)
		{
			if (isConnected) 
			{
				DebugLog.instance.LogError (string.Format ("Socket is connected. address : {0}. port : {1}", _address, _port));
				return;
			}

			_address = host;
			_port = port.ToString();
			_socket.BeginConnect (host, port, EndConnect, null);
		}

		public void Send(byte[] buffer)
		{
			int length = buffer.Length + PACKAGE_LENGTH;	//加上length的四个字节长度。
			int netLength = IPAddress.HostToNetworkOrder (length);
			byte[] lengthBytes = BitConverter.GetBytes (netLength);

			byte[] sendBuffer = new byte[length];
			Array.Copy (lengthBytes, sendBuffer, PACKAGE_LENGTH);
			Array.Copy (buffer, 0, sendBuffer, PACKAGE_LENGTH, buffer.Length);

			_socket.BeginSend(sendBuffer, 0, length, SocketFlags.None, EndSend, null);
		}

		void EndSend(IAsyncResult ar)
		{
			int length = _socket.EndSend (ar);
			DebugLog.instance.LogNormal (" Socket Send End length : {0}. address : {1}. port : {2}.", length, _address, _port);
		}

		void EndConnect(IAsyncResult ar)
		{
			if (!ar.IsCompleted) 
			{
				throw new Exception (string.Format ("Async Socket Connect Failure. address : {0}. port : {1}", _address, _port));
			}
			//TODO SEND GLOBAL EVENT ==> SOCKET CONNECTED SUCCESS.

			BeginReceive ();
		}

		void BeginReceive()
		{
			_socket.BeginReceive (_receiveBuffer, 0, MAX_LENGTH, SocketFlags.None, OnReceive, null);
		}

		void OnReceive(IAsyncResult ar)
		{
			int length = _socket.EndReceive (ar);

			if (length < 1) {
				Disconnect ("接收包长度 < 1");
				return;
			}
			try {
				HandleMsg (_receiveBuffer, length);
				Array.Clear (_receiveBuffer, 0, MAX_LENGTH);
				BeginReceive ();	
			} catch (Exception ex) {
				Disconnect (ex.Message);
			}
		}

		void HandleMsg(byte[] buffer, int length)
		{
			_stream.Seek (0, SeekOrigin.End);
			_stream.Write (buffer, 0, length);
			_stream.Seek (0, SeekOrigin.Begin);

			while (RemainBuffLength () > PACKAGE_LENGTH) 				//包头四个字节用来定义包的长度
			{
				int packageLength = IPAddress.NetworkToHostOrder (_reader.ReadInt32 ());
				int remainLength = packageLength - PACKAGE_LENGTH;		//去掉自身包长数据长度

				if (remainLength > RemainBuffLength ()) {	//buffer数据长度小于了包的长度，丢入缓冲区继续接收包
					_stream.Position -= PACKAGE_LENGTH;					//恢复到读取长度操作前的数据
					break;
				}

				byte[] package = _reader.ReadBytes (remainLength);
				//TODO 发送数据包。
			}

			byte[] leftBytes = _reader.ReadBytes ((int)RemainBuffLength ());
			_stream.SetLength (0);
			_stream.Write (leftBytes, 0, leftBytes.Length);

		}

		long RemainBuffLength()
		{
			return _stream.Length - _stream.Position;
		}

		public void Disconnect(string msg)
		{
			if (isConnected)
				_socket.Disconnect (false);

			_socket = null;

			//TODO 发送断开连接的事件。
		}
	}
}
