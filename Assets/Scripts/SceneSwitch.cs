using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class SceneSwitch : NetworkBehaviour
{
    public void ButtonChangeScene()
    {
        if (isServer)
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Offline")
            { 
                NetworkManager.singleton.ServerChangeScene("new"); 
            }

            else 
            { 
                NetworkManager.singleton.ServerChangeScene("Offline"); 
            }
        }
      
    }
}
