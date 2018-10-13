using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;


public class OscManager : MonoBehaviour
{
    private string _prefix = "/1/toggle";
    private int _id;

    

    public int ID{
        get { return _id; }
        private set { _id = value; }
    }


    void Start () {
	    OSCHandler.Instance.Init();
	}
	
	void Update () {
		listenToOSC();
	}
	
	void listenToOSC(){
		OSCHandler.Instance.UpdateLogs ();
		Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
		servers = OSCHandler.Instance.Servers;

		foreach (KeyValuePair<string, ServerLog> item in servers) {
			if (item.Value.log.Count > 0) {
				//Debug.Log ("count is more than zero");
                int lastPacketIndex = item.Value.packets.Count - 1;


                if (item.Value.packets[lastPacketIndex].Address.Contains(_prefix))
                {
                    //string s = item.Value.packets [lastPacketIndex].Data [0].ToString ();
                    //Debug.Log(item.Value.packets[lastPacketIndex].Address);
                    string address = item.Value.packets[lastPacketIndex].Address;
                    address = address.Replace(_prefix,"");
                    ID = int.Parse(address);
                }
			}else{
				//Debug.Log("Not Conect");
			}
		}
	    OSCHandler.Instance.UpdateLogs ();
	}
}
