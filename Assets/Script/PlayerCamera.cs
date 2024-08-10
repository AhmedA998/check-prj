using UnityEngine;
using Mirror;

public class PlayerCamera : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        // aa
        Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("LocalPlayerCards"));
    }

    public override void OnStartClient()
    {
        if (!isLocalPlayer)
        {
            Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("LocalPlayerCards"));
        }
    }
}
