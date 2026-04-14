using UnityEngine;

public class EnemyChase : EnemyWaypoints
{
    // optimise to not have a trigger and not a trigger and just one box collider!!!
    private bool m_patrolstate = true;
    private GameObject m_player;
    private CircleCollider2D m_circlecollider2d;

    protected override void Awake()
    {
        base.Awake();
        m_circlecollider2d = GetComponent<CircleCollider2D>();
    }
    /// <summary>
    /// if not patrolling move towards the player, patrolstate is set when player gets near enemy.
    /// </summary>
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
    /// <summary>
    /// when player nearby sets patrol state to false which in fixedupdate means it will start chasing them.
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Player")
        {
            m_player = collision.gameObject;
            m_patrolstate = false;
        }
    }
    /// <summary>
    /// starts patrolling again once not near enemy
    /// </summary>
    /// <param name="collision"></param>
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

