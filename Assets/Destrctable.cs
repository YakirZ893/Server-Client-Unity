using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Destrctable : NetworkBehaviour
{
    public GameObject destroyedObject;
    public ServerData serverData;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == serverData.GetPlayer())
        {
            Instantiate(destroyedObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        

        
    }


}
