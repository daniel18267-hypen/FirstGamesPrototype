
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BaseMovement : MonoBehaviour
{
    public UnityEvent<bool> GrappleStateChanged;
    private Rigidbody2D m_rigidbody;
    private float m_speed = 10f;
    private Vector2 m_hitlocation;
    private Vector2 m_playerdirection;
    private Vector2 m_lastdirection;
    private double m_lookrotation;
    private bool m_ismoving;
    [SerializeField] private int m_playerdamage;
    [SerializeField] private LayerMask m_RayLayer;
    [SerializeField] private LineRenderer m_lineRenderer;
    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        // two positions on line renderer component - meaning straight line
        m_lineRenderer.positionCount = 2;
    }
    /// <summary>
    /// handles basic movement
    /// </summary>
    /// <param name="ctx"></param>
    public void HandleMovement(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            // reads player direction in from WASD input
            m_playerdirection = (ctx.ReadValue<Vector2>());
            if (m_rigidbody != null) // defensive programming
            {
                // player direction (read in from WASD movement) affects velocity of player
                m_rigidbody.linearVelocity = m_playerdirection * m_speed;
            }
            // this is done so the grapple will grapple in last direction faced
            m_lastdirection = m_playerdirection;
        }
        else if (ctx.canceled)
        {
            if (m_rigidbody != null) // defensive programming
            {
                // if input stopped player stops moving
                m_rigidbody.linearVelocity = Vector2.zero;
            }
        }
    }
    /// <summary>
    /// handles rotation, and if grappling changes line renderer for grapple to players current position.
    /// </summary>
    private void FixedUpdate()
    {
        // essentially combines the players last direction in the x and y values to be one angle
        m_lookrotation = Math.Atan2(m_lastdirection.y, m_lastdirection.x) * Mathf.Rad2Deg;
        // makes the player look at angle just formed, so faces in direction of movement.
        transform.rotation = Quaternion.AngleAxis((float)m_lookrotation, (Vector3.forward));
        // this is set to true if the player has hit a grapple point and is moving towards it
        if (m_ismoving == true)
        {
            // sets the position of the first element of the line renderer to be the player
            m_lineRenderer.SetPosition(0, transform.position);
            // player moves towards the grapple point
            transform.position = Vector2.MoveTowards(transform.position, m_hitlocation, (m_speed * Time.deltaTime));
        }
    }

    /// <summary>
    /// Function for handling grapple, triggered when left mouse button is pressed, fires a grapple, line renderer used for visuals, raycast used for interacting with other objects
    /// </summary>
    /// <param name="ctx"></param>
    public void HandleGrapple(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            // everything that is hit is stored in an array
            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(transform.position, m_lastdirection, 25, m_RayLayer);
            // loops through everything that was hit
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i] && !hits[i].collider.isTrigger)
                    {
                    // line renderer is enabled and it's position is set to be a straight line between the player and the object hit
                        m_lineRenderer.enabled = true;
                        m_lineRenderer.SetPosition(0, transform.position);
                        m_lineRenderer.SetPosition(1, hits[i].point);
                        string m_tag = hits[i].collider.tag;
                    // uses switch statement to check each tag and do behaviours accordingly, uses trygetcomponent for defensive programming
                        switch (m_tag)
                        {
                        // used for things you want the grapple to stop at
                            case "breakgrapple":
                                break;
                            case "volatileswitch":
                                // how trygetcomponent works is if the component VolatileSwitch is on what was hit it'll be set to the m_volatileswitch variable.
                                if (hits[i].transform.gameObject.TryGetComponent<VolatileSwitch>(out VolatileSwitch m_volatileswitch))
                                {
                                    // triggers switch
                                    m_volatileswitch.TriggerVolatileSwitch();
                                }
                                // used so the raycast goes through this switch and doesn't stop at it
                                continue;

                            case "grapplepoint":
                                {
                                // when hit will change state, which in fixedupdate will start player moving towards hit point
                                    m_hitlocation = hits[i].transform.position;
                                    ChangeState(true);
                                }
                                break;
                            case "staticswitch":
                                if (hits[i].transform.gameObject.TryGetComponent<StaticSwitch>(out StaticSwitch m_staticswitch))
                                {
                                    // triggers switch
                                    m_staticswitch.TriggerStaticSwitch();
                                }
                                break;
                            case "Enemy":
                                if (hits[i].transform.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth m_enemyhealth))
                                {
                                // damages enemy
                                    m_enemyhealth.RecieveDamage(m_playerdamage);
                                }
                                // used so line goes through enemy and doesn't stop at it
                                continue;
                        }
                        break;
                    }
                }
        }
        // if cancelled stop displaying line renderer
        if (ctx.canceled)
        {
            ChangeState(false);
            m_lineRenderer.enabled = false;
        }
    }
    /// <summary>
    /// handles changing from grappling to not grappling, used to tell lava script if it should do damage or not, called when start / stop grappling
    /// </summary>
    /// <param name="m_isgrappling"></param>
    private void ChangeState(bool m_isgrappling)
    {
        GrappleStateChanged?.Invoke(m_isgrappling);
        m_ismoving = m_isgrappling;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // cancels grapple if touching grapple point
        if (collision.gameObject.tag == "grapplepoint")
        {
            ChangeState(false);
            m_lineRenderer.enabled = false;
        }
    }
    
}

