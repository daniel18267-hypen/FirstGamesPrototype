using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyWaypoints : MonoBehaviour
{
    [SerializeField] private GameObject[] m_waypoints;
    protected float m_speed = 5;
    protected float m_health = 50;
    private bool m_coroutinerunning = false;
    protected Rigidbody2D m_rigidbody;
    private int m_waypointindex = 0;
    private Vector3 m_originalscale;
    protected BoxCollider2D m_boxcollider2d;
    public UnityEvent<int> DamagePlayer;
    protected virtual void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_boxcollider2d = GetComponent<BoxCollider2D>();
    }
    protected virtual void FixedUpdate()
    {
        // move towards next waypoint in array.
         transform.position = Vector2.MoveTowards(transform.position, m_waypoints[m_waypointindex].transform.position, (m_speed * Time.deltaTime));          
    }
    /// <summary>
    /// if touching waypoint set movement target to be next waypoint
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_boxcollider2d.IsTouching(collision) && collision.gameObject.tag == "waypoint" && m_waypointindex <= m_waypoints.Length)
        {
            m_waypointindex++;
            // if out of bounds goes back to first waypoint in array.
            if (m_waypointindex >= m_waypoints.Length)
            {
                m_waypointindex = 0;
            }
        }
    }
    /// <summary>
    ///  does damage to player
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && m_coroutinerunning == false)
        {
            // does damage to player every 0.75 seconds
            StartCoroutine(Cooldown());
            DamagePlayer?.Invoke(20);
        }
    }
 
    /// <summary>
    ///cooldown for enemy doing damage to player so it doesn't just instakill them.
    /// </summary>
    /// <returns></returns>
    private  IEnumerator Cooldown()
    {
        m_coroutinerunning = true;
        yield return new WaitForSeconds(0.75f);
        m_coroutinerunning = false;
    }
}
