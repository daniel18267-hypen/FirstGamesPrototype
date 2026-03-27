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
            {
                m_player = collision.gameObject;
                m_healthscript = m_player.GetComponent<PlayerHealth>();
                DamagePlayer.AddListener(m_healthscript.DamageHandler);
            }
            DamagePlayer?.Invoke(m_projectiledamage);
            transform.position = new Vector3(10000, 100000, 10000);
            m_rigidbody.linearVelocity = Vector3.zero;
            m_rigidbody.angularVelocity = 0;
        }
    }

}
