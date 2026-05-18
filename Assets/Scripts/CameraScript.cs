using NUnit.Framework.Constraints;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private void LateUpdate()
    {
        // this is done so, while the camera is a player of child, it doesn't rotate with the player as that's super jarring, instead rotation is essentially reset every frame
        transform.rotation = Quaternion.identity;
    }
}
   
