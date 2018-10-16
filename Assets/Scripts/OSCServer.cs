using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uOSC
{
	[RequireComponent(typeof(uOscServer))]
	public class OSCServer : MonoBehaviour
	{
		private string _prefix = "/1/push";
		private int _id;
		
		public int ID{
			get { return _id; }
			private set { _id = value; }
		}

		
		void Start()
		{
			var server = GetComponent<uOscServer>();
			server.onDataReceived.AddListener(OnDataReceived);
		}

		void OnDataReceived(Message message)
		{
			/*
			// address
			var msg = message.address + ": ";

			// timestamp
			msg += "(" + message.timestamp.ToLocalTime() + ") ";

			// values
			foreach (var value in message.values)
			{
				msg += value.GetString() + " ";
			}
			*/
			string address = message.address;
			address = address.Replace(_prefix,"");
			ID = int.Parse(address);
			Debug.Log(ID);
		}
	}

}