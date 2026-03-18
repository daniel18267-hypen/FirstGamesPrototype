using Unity.VisualScripting;
using UnityEngine;

public class EnemyWaypoints : MonoBehaviour
{
    [SerializeField] private GameObject[] m_waypoints;
    private float m_speed = 5;
    private int m_waypointindex = 0;
    private void FixedUpdate()
    {

        {
            transform.position = Vector2.MoveTowards(transform.position, m_waypoints[m_waypointindex].transform.position, (m_speed * Time.deltaTime));



        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("got to start");
        if (collision.gameObject.tag == "waypoint" && m_waypointindex <= m_waypoints.Length)
        {
            Debug.Log("got here");
            m_waypointindex++;
            if (m_waypointindex >= m_waypoints.Length)
            {
                m_waypointindex = 0;
            }
        }
    }
}
