using NUnit.Framework.Constraints;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
   
