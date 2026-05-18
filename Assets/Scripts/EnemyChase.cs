using UnityEngine;

public class EnemyChase : EnemyWaypoints
{
    private bool m_patrolstate = true;
    private GameObject m_player;
    private CircleCollider2D m_circlecollider2d;
    protected override void Awake()
    {
        // initialises components
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
            // moves between waypoints
             base.FixedUpdate();
        }
    if (m_patrolstate == false)
        {
            // moves to player
            transform.position = Vector2.MoveTowards(transform.position,m_player.transform.position, (m_speed * Time.deltaTime));
        }
    }
    /// <summary>
    /// when player nearby sets patrol state to false which in fixedupdate means it will start chasing them.
    /// </summary>
    /// <param name="collision"></param>
    protected  void OnTriggerStay2D(Collider2D collision)
    {
        
        // chases player if they enter trigger box
        // this is done in ontriggerstay as when enemy collided with player with box collider it stopped chasing player if ontriggerenter was used
        if (collision.gameObject.tag == "Player")
        {
            m_player = collision.gameObject;
            m_patrolstate = false;
        }
    }
    /// <summary>
    /// goes to next waypoint if hit waypoint
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // base ontriggerenter will go to next waypoint in array if enemy hits waypoint, then they move towards in fixedupdate.
        base.OnTriggerEnter2D (collision);
    }
    /// <summary>
    /// starts patrolling again once not near player
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // if player exits trigger box goes back to patrolling
        if (collision.gameObject.tag == "Player") 
        {
            m_patrolstate = true;
        }
    }
    /// <summary>
    /// calls damage in base script, damages player with cooldown of 0.75 seconds.
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
    }
}

