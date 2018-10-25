using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

namespace uOSC
{
	[RequireComponent(typeof(uOscServer))]
	[DefaultExecutionOrder(-10)]
	public class OSCServer : MonoBehaviour
	{
		private string _prefix = "/1/push";
		private int _id;
		
		public int ID{
			get { return _id; }
			private set { _id = value; }
		}

		private int _bufferID;
		private int _frame;
		

		
		void Start()
		{
			var server = GetComponent<uOscServer>();
			server.onDataReceived.AddListener(OnDataReceived);
		}

		private void FixedUpdate()
		{
			if (Time.frameCount % 3 == 0) ID = 0;
			//Debug.Log(Time.frameCount + "Frame:" + "Fixed Update :" + ID);
		}

		void Update()
		{	
			
		}

		void OnDataReceived(Message message)
		{
			
			/*
			string address = message.address;
			address = address.Replace(_prefix,"");
			ID = int.Parse(address);
			*/
			
		
			//- 同じ値が600フレーム続いたら強制的にidをリフレッシュ
			// 
			//- 同じ値の間だけカウンターを進めて行く
			
			string address = message.address;
			address = address.Replace(_prefix,"");
			ID = int.Parse(address);
			//Debug.Log(Time.frameCount + "Frame:" + "Event Update :" + ID);
		}
	}
}