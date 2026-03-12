using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class BaseMovement : MonoBehaviour
{
    public UnityEvent<bool> GrappleStateChanged;
    private Rigidbody2D m_rigidbody;
    private float m_speed = 10f;
    private Vector2 m_hitlocation;
    private Vector2 m_playerdirection;
    private Vector2 m_lastdirection;
    private bool m_ismoving;
    [SerializeField] private StaticSwitch m_staticswitch;
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
                m_rigidbody.linearVelocity = m_playerdirection * m_speed;
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
    private void FixedUpdate()
    {

        if (m_ismoving == true)
        {
            Debug.Log(m_hitlocation);
            m_lineRenderer.SetPosition(0, transform.position);
            transform.position = Vector2.MoveTowards(transform.position, m_hitlocation, (m_speed * Time.deltaTime));
        }
    }


    public void HandleGrapple(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            RaycastHit2D[] hits;
            Debug.Log("Raycast");
            hits = Physics2D.RaycastAll(transform.position, m_lastdirection, 100, m_RayLayer);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i])
                {

                    m_lineRenderer.enabled = true;
                    m_lineRenderer.SetPosition(0, transform.position);
                    m_lineRenderer.SetPosition(1, hits[i].point);
                    if (hits[i].collider.tag == "grapplepoint")
                    {
                        m_hitlocation = hits[i].transform.position;
                        ChangeState(true);
                    }
                    else if (hits[i].collider.tag == "staticswitch")
                    {
                        Debug.Log("detected tag");
                        if (hits[i].transform.gameObject.TryGetComponent<StaticSwitch>(out StaticSwitch m_staticswitch))
                        {
                            Debug.Log("got staticswitch component!");
                            m_staticswitch.TriggerStaticSwitch();
                        }
                    }
                }
            }
        }
        if (ctx.canceled)
        {
            ChangeState(false);
            m_lineRenderer.enabled = false;
        }
    }
    private void ChangeState(bool m_isgrappling)
    {
        GrappleStateChanged?.Invoke(m_isgrappling);
        m_ismoving = m_isgrappling;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "grapplepoint")
        {
            ChangeState(false);
            m_lineRenderer.enabled = false;
        }
    }
}

