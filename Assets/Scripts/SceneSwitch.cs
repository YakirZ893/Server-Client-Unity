﻿using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class SceneSwitch : NetworkBehaviour
{

    public void ButtonChangeScene()
    {
        if (isServer)
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Desert")
            {
                NetworkManager.singleton.ServerChangeScene("HighWay");
            }
            else
            {
                NetworkManager.singleton.ServerChangeScene("Desert");
            }

        }

    }
}
