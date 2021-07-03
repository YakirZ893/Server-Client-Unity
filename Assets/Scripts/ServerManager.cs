using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ServerManager : NetworkBehaviour
{
   [SerializeField] private GameObject RemotePrefab;
    public ServerData ServerData;

    public override void OnStartServer()
    {
        if(RemotePrefab!= null)
        {
        RemotePrefab = Instantiate(RemotePrefab, transform.position, Quaternion.identity);
        NetworkServer.Spawn(RemotePrefab);
        }
    }






}
