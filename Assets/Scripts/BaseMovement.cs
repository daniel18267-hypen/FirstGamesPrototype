using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseMovement : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private float m_speed = 1000f;
    private Vector2 m_playerdirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
 
    public void HandleMovement(InputAction.CallbackContext ctx)
    {
       if(ctx.performed)
        {

            m_playerdirection = (ctx.ReadValue<Vector2>().normalized);
            m_rigidbody.linearVelocity = m_playerdirection * (Time.deltaTime * m_speed);
            Debug.Log(m_playerdirection);
            if (m_playerdirection.y != 0)
            {
                transform.localEulerAngles = new Vector3(0, 0, (m_playerdirection.y * 90));
            }
            else if (m_playerdirection.x > 0)
            {
                transform.localEulerAngles = new Vector3(0, 0, (m_playerdirection.x * 360));
            }
            else if (m_playerdirection.x < 0)
            {
                transform.localEulerAngles = new Vector3(0, 0, (m_playerdirection.x * -180));
            }

        }
       else if (ctx.canceled)
        {
            
            m_rigidbody.linearVelocity = Vector2.zero;
            m_playerdirection = Vector2.zero;
        }
    }
}
