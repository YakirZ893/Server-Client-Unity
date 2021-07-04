using UnityEngine;
using Mirror;

public class LightSwitch : NetworkBehaviour
{
    public ServerData serverData;
    public Material emission;
    private bool lightSwitch;


    public void SetLight()
    {
        lightSwitch = !lightSwitch;
        if (lightSwitch)
        {
            emission.EnableKeyword("_EMISSION");
        }
        else
        {
            emission.DisableKeyword("_EMISSION");
        }
        var Lights = serverData.GetPlayer().GetComponentsInChildren<Light>();
        foreach (Light light in Lights)
        {
            light.enabled = lightSwitch;
        }
    }

    [ClientRpc]
    public void RPCSetLight()
    {
        SetLight();
    }

}
