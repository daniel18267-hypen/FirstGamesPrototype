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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        m_rigidbody.linearVelocity = m_playerdirection * (Time.deltaTime * m_speed);
    }
    public void HandleMovement(InputAction.CallbackContext ctx)
    {
       if(ctx.performed)
        {
            m_playerdirection = ctx.ReadValue<Vector2>();
            transform.rotation = (0,ctx.ReadValue<Vector2>());

        }
       else if (ctx.canceled)
        {
            m_playerdirection = Vector2.zero;
        }
    }
}
