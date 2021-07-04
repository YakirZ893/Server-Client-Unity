using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CarFlip : NetworkBehaviour
{
    public ServerData serverData;
    private GameObject CarRef;

    public void FlipCar()
    {
        CarRef = serverData.GetPlayer();
        Vector3 eulerRotation = CarRef.transform.rotation.eulerAngles;
        CarRef. transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }

    [ClientRpc]
    public void RPCFlipCar()
    {
        FlipCar();
    }
}
