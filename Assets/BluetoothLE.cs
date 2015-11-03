using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;

public class BluetoothLE : MonoBehaviour {

    private TcpClient client;
    private NetworkStream stream;
    private float pitch, roll, yaw;
    // Use this for initialization
    void Start () {
        //サーバーに接続 
        Int32 port = 9999;
        client = new TcpClient("127.0.0.1", port);
        stream = client.GetStream();
        pitch = 0;
        roll = 0;
        yaw = 0;
    }
	
	// Update is called once per frame
	void Update () {
        Byte[] data=new Byte[1];
        data[0] = 1;
        stream.Write(data, 0, data.Length);
        //Serverからの応答データ受信
        Byte[] rdata = new Byte[64];
        // サーバからの応答データを受信
        Int32 bytes = stream.Read(rdata, 0, rdata.Length);
        float bufpitch = BitConverter.ToSingle(rdata, 0);
        float bufroll = BitConverter.ToSingle(rdata, 4);
        float bufyaw = BitConverter.ToSingle(rdata, 8);
        pitch = bufpitch;
        roll = bufroll;
        yaw = bufyaw;
        Debug.Log(pitch.ToString() + " " + roll.ToString()+" "+yaw.ToString());

        transform.eulerAngles=new Vector3(pitch, 0, roll);
    }
}
