using UnityEngine;

public class EnemyChase : EnemyWaypoints
{
    private bool m_patrolstate = true;
    private GameObject m_player;
    private CircleCollider2D m_circlecollider2d;

    protected override void Awake()
    {
        base.Awake();
        m_circlecollider2d = GetComponent<CircleCollider2D>();
    }
    protected override void FixedUpdate()
    {
    if (m_patrolstate == true)
        {
             base.FixedUpdate();
        }
    if (m_patrolstate == false)
        {
            transform.position = Vector2.MoveTowards(transform.position,m_player.transform.position, (m_speed * Time.deltaTime));
        }
    
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Player")
        {
            m_player = collision.gameObject;
            m_patrolstate = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !m_circlecollider2d.IsTouching(collision))
        {
            m_patrolstate = true;
        }
    }
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
    }
}

