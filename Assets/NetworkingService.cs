using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using ENet;

public class NetworkingService : MonoBehaviour {

    public ushort port;
    private readonly int maxClients = 4;
    public void StartServer()
    {
        // ENet.Library.Initialize();
        
        // using (Host server = new Host())
        // {
        //     Address address = new Address();

        //     address.Port = port;
        //     server.Create(address, maxClients);
        //     ENet.Event netEvent;

        //     while (true)
        //     {
        //         server.Service(15, out netEvent);

        //         switch (netEvent.Type)
        //         {
        //             case ENet.EventType.None:
        //                 break;

        //             case ENet.EventType.Connect:
        //                 Debug.Log("Client connected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
        //                 break;

        //             case ENet.EventType.Disconnect:
        //                 Debug.Log("Client disconnected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
        //                 break;

        //             case ENet.EventType.Timeout:
        //                 Debug.Log("Client timeout - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
        //                 break;

        //             case ENet.EventType.Receive:
        //                 Debug.Log("Packet received from - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP + ", Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);
        //                 netEvent.Packet.Dispose();
        //                 break;
        //         }
        //     }

        //     server.Flush();
        // }
    }

    public void StartClient(string ip)
    {
        // using (Host client = new Host())
        // {
        //     Address address = new Address();

        //     address.SetHost(ip);
        //     address.Port = port;
        //     client.Create();

        //     Peer peer = client.Connect(address);
        //     ENet.Event netEvent;
        //     while (true)
        //     {
        //         client.Service(15, out netEvent);

        //         switch (netEvent.Type)
        //         {
        //             case ENet.EventType.None:
        //                 break;

        //             case ENet.EventType.Connect:
        //                 Debug.Log("Client connected to server - ID: " + peer.ID);
        //                 break;

        //             case ENet.EventType.Disconnect:
        //                 Debug.Log("Client disconnected from server");
        //                 break;

        //             case ENet.EventType.Timeout:
        //                 Debug.Log("Client connection timeout");
        //                 break;

        //             case ENet.EventType.Receive:
        //                 Debug.Log("Packet received from server - Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);
        //                 netEvent.Packet.Dispose();
        //                 byte[] buffer = new byte[1024];

        //                 netEvent.Packet.CopyTo(buffer);
        //                 // Cast buffer to .......
        //                 break;
        //         }
        //     }

            // client.Flush();
        // }
    }

    
}
