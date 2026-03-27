using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossShootAtPlayer : MonoBehaviour
{
    [SerializeField] private GameObject m_projectileprefab;
    [SerializeField] private float m_projectilespeed = 10;
    [SerializeField] private GameObject m_player;
    private bool m_coroutinerunning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && m_coroutinerunning == false)
        {
            m_player = collision.gameObject;
            StartCoroutine(Cooldown());
            GameObject m_bullet = Instantiate(m_projectileprefab, transform.position, Quaternion.identity);
            if (m_bullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D m_projectilerigidbody))
            {
                Debug.Log("has rigidbody2d");
                Debug.Log(m_player.transform.position);
                m_projectilerigidbody.AddForce((m_player.transform.position - transform.position) * m_projectilespeed, ForceMode2D.Impulse);
            }
        }
    }
    IEnumerator Cooldown()
    {
        m_coroutinerunning = true;
        yield return new  WaitForSeconds(0.75f);
        m_coroutinerunning = false;
    }
}
