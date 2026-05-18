using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossShootAtPlayer : MonoBehaviour
{
    [SerializeField] private GameObject m_projectileprefab;
    [SerializeField] private float m_projectilespeed = 5;
    [SerializeField] private GameObject m_player;
    [SerializeField] private ProjectileDamage[] m_projectiles;
    private int m_projectileindex = 0;
    private bool m_coroutinerunning = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        // if player is within trigger box and cooldown is inactive, shoots at player
        if (collision.tag == "Player" && m_coroutinerunning == false)
        {
            m_player = collision.gameObject;
            // resets all rigidbody values to ensure projectile goes in right direction.
            m_projectiles[m_projectileindex].ResetRB();
            // gets projectiles from array, used object pooling for this for optimisation, sets position to be position of the boss
            m_projectiles[m_projectileindex].transform.position = transform.position;
            // if projectiles have rigidbody, add force to them, in direction of player
            if (m_projectiles[m_projectileindex].TryGetComponent<Rigidbody2D>(out Rigidbody2D m_projectilerigidbody))
            {
                m_projectilerigidbody.AddForce((m_player.transform.position - transform.position).normalized * m_projectilespeed, ForceMode2D.Impulse);
                // increase index to get next projectile
                m_projectileindex++;
                // if index goes out of bounds set it back to 0
                if (m_projectileindex >= m_projectiles.Length)
                {
                    m_projectileindex = 0;
                }
            }
            StartCoroutine(Cooldown());
        }
    }
    IEnumerator Cooldown()
    {
        // 5 second cooldown for bullets
        m_coroutinerunning = true;
        yield return new  WaitForSeconds(5f);
        m_coroutinerunning = false;
    }
}
