using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileDamage : MonoBehaviour
{
    public UnityEvent<int> DamagePlayer;
    [SerializeField] private int m_projectiledamage;
    private GameObject m_player;
    private PlayerHealth m_healthscript;
    private Rigidbody2D m_rigidbody;
    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
               // gets player and players health script
        m_player = collision.gameObject;
        m_healthscript = m_player.GetComponent<PlayerHealth>();
                // adds players health script as a listener to the damage player event
          DamagePlayer.AddListener(m_healthscript.DamageHandler);
            
            // damages players
            DamagePlayer?.Invoke(m_projectiledamage);
            transform.position = new Vector3(10000, 100000, 10000);
        }
    }
    /// <summary>
    /// called when spawning projectile for object pooling, resets all rigidbody values to 0 to ensure in right direction when called again
    /// </summary>
    public void ResetRB()
    {
        m_rigidbody.linearVelocity = Vector2.zero;
        m_rigidbody.angularVelocity = 0.0f;
        m_rigidbody.totalForce = Vector2.zero;
        m_rigidbody.linearDamping = 0.0f;
        m_rigidbody.angularDamping = 0.0f;
    }

}
