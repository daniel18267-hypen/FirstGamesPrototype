using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWaypoints : MonoBehaviour
{
    [SerializeField] private GameObject[] m_waypoints;
    protected float m_speed = 5;
    private int m_waypointindex = 0;
    protected BoxCollider2D m_boxcollider2d;
    public UnityEvent<int> DamagePlayer;
    protected virtual void Awake()
    {
        m_boxcollider2d = GetComponent<BoxCollider2D>();
    }
    protected virtual void FixedUpdate()
    {
         transform.position = Vector2.MoveTowards(transform.position, m_waypoints[m_waypointindex].transform.position, (m_speed * Time.deltaTime));          
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("got to start");
        if (m_boxcollider2d.IsTouching(collision) && collision.gameObject.tag == "waypoint" &&  m_waypointindex <= m_waypoints.Length)
        {
            Debug.Log("got here");
            m_waypointindex++;
            if (m_waypointindex >= m_waypoints.Length)
            {
                m_waypointindex = 0;
            }
        }
        if (m_boxcollider2d.IsTouching(collision) && collision.gameObject.tag == "Player")
        {
            DamagePlayer?.Invoke(20);
        }
            
}
         
}
