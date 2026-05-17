using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class BaseMovement : MonoBehaviour
{
    public UnityEvent<bool> GrappleStateChanged;
    private Rigidbody2D m_rigidbody;
    private float m_speed = 10f;
    private Vector2 m_hitlocation;
    private Vector2 m_playerdirection;
    private Vector2 m_lastdirection;
    private bool m_ismoving;
    private Animator m_animator;
    private Sprite m_sprite;
    private SpriteRenderer m_spriterenderer;
    [SerializeField] private int m_playerdamage;
    [SerializeField] private LayerMask m_RayLayer;
    [SerializeField] private LineRenderer m_lineRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        m_sprite = GetComponent<Sprite>();
        m_spriterenderer = GetComponent<SpriteRenderer>();
       m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_lineRenderer.positionCount = 2;
    }

    private void Start()
    {
       
    }

    /// <summary>
    /// handles basic movement
    /// </summary>
    /// <param name="ctx"></param>
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
    /// <summary>
    /// handles rotation, and if grappling changes line renderer for grapple to players current position.
    /// </summary>
    private void FixedUpdate()
    {
        double m_lookrotation = Math.Atan2(m_lastdirection.y, m_lastdirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis((float)m_lookrotation, (Vector3.forward));
       
        if (m_ismoving == true)
        {
            Debug.Log(m_hitlocation);
            m_lineRenderer.SetPosition(0, transform.position);
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
            RaycastHit2D[] hits;
            Debug.Log("Raycast");
            hits = Physics2D.RaycastAll(transform.position, m_lastdirection, 25, m_RayLayer);
            
                for (int i = 0; i < hits.Length; i++)
                {
                Debug.Log(hits[i]);
                    if (hits[i] && !hits[i].collider.isTrigger)
                    {

                        m_lineRenderer.enabled = true;
                        m_lineRenderer.SetPosition(0, transform.position);
                        m_lineRenderer.SetPosition(1, hits[i].point);
                        string m_tag = hits[i].collider.tag;
                    // uses switch statement to check each tag and do behaviours accordingly, uses trygetcomponent for defensive programming
                        switch (m_tag)
                        {
                            case "breakgrapple":
                                break;
                            case "volatileswitch":

                                Debug.Log("detected tag");
                                if (hits[i].transform.gameObject.TryGetComponent<VolatileSwitch>(out VolatileSwitch m_volatileswitch))
                                {
                                    Debug.Log("got volatile switch component");
                                    m_volatileswitch.TriggerVolatileSwitch();

                                }
                                continue;

                            case "grapplepoint":
                                {
                                    m_hitlocation = hits[i].transform.position;
                                    ChangeState(true);
                                }
                                break;

                            case "staticswitch":
                                if (hits[i].transform.gameObject.TryGetComponent<StaticSwitch>(out StaticSwitch m_staticswitch))
                                {
                                    Debug.Log("got staticswitch component!");
                                    m_staticswitch.TriggerStaticSwitch();

                                }
                                break;
                            case "Enemy":
                                if (hits[i].transform.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth m_enemyhealth))
                                {
                                    m_enemyhealth.RecieveDamage(m_playerdamage);
                                }
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

