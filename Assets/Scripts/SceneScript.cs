using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



    public class SceneScript : NetworkBehaviour
    {
        public Text canvasStatusText;
     

        [SyncVar(hook = nameof(OnStatusTextChanged))]
        public string statusText;

        void OnStatusTextChanged(string _Old, string _New)
        {
            //called from sync var hook, to update info on screen for all players
            canvasStatusText.text = statusText;
        }

        public void ButtonChangeScene()
        {
            if (isServer)
            {
                Scene scene = SceneManager.GetActiveScene();
                if (scene.name == "Offline") { NetworkManager.singleton.ServerChangeScene("Other"); }

                else { NetworkManager.singleton.ServerChangeScene("Offline"); }
            }
            else { Debug.Log("You are not Host."); }
        }
    }
