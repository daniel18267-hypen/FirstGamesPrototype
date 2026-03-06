using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseMovement : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private float m_speed = 1000f;
    private Vector2 m_playerdirection;
    private Vector2 m_lastdirection;
    [SerializeField] private LayerMask m_RayLayer;
    [SerializeField] private LineRenderer m_lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_lineRenderer.positionCount = 2;
    }

    public void HandleMovement(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {

            m_playerdirection = (ctx.ReadValue<Vector2>());
            if (m_rigidbody != null) // defensive programming
            {
                m_rigidbody.linearVelocity = m_playerdirection * (Time.deltaTime * m_speed);
            }
            m_lastdirection = m_playerdirection;
        }
        else if (ctx.canceled)
        {
            if (m_rigidbody != null) // defensive programming
            {

                m_rigidbody.linearVelocity = Vector2.zero;
            }
            
            m_playerdirection = Vector2.zero;
        }  
    }

    
    public void HandleGrapple(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Raycast");
            RaycastHit2D hit = Physics2D.Raycast(transform.position,m_lastdirection,100, m_RayLayer);

            if (hit)
            {
                Debug.Log("hit");
                Debug.DrawRay(transform.position, (m_lastdirection * 100), Color.green);
                m_lineRenderer.enabled = true;
                m_lineRenderer.SetPosition(0, transform.position);
                m_lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                m_lineRenderer.enabled = false;
                Debug.DrawRay(transform.position, (m_lastdirection * 100), Color.red);
            }
        }
        // if ctx cancelled line renderer false
    }
}
