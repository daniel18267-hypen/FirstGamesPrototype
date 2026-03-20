using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyWaypoints : MonoBehaviour
{
    [SerializeField] private GameObject[] m_waypoints;
    protected float m_speed = 5;
    protected float m_health = 50;
    protected Image m_healthbar;
    private int m_waypointindex = 0;
    private Vector3 m_originalscale;
    protected BoxCollider2D m_boxcollider2d;
    public UnityEvent<int> DamagePlayer;
    public UnityEvent<float> ChangeHealthBar;
    protected virtual void Awake()
    {
        m_healthbar = this.gameObject.GetComponentInChildren<Image>();
        m_boxcollider2d = GetComponent<BoxCollider2D>();
    }
    protected virtual void FixedUpdate()
    {
         transform.position = Vector2.MoveTowards(transform.position, m_waypoints[m_waypointindex].transform.position, (m_speed * Time.deltaTime));          
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("got to start");
        if (m_boxcollider2d.IsTouching(collision) && collision.gameObject.tag == "waypoint" && m_waypointindex <= m_waypoints.Length)
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
    protected virtual void RecieveDamage(float damage)
    {
        m_health = m_health - damage;
        m_healthbar.fillAmount = m_health / 100;
        if (m_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
