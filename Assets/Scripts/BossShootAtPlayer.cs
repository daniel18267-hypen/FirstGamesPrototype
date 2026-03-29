using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossShootAtPlayer : MonoBehaviour
{
    [SerializeField] private GameObject m_projectileprefab;
    [SerializeField] private float m_projectilespeed = 5;
    [SerializeField] private GameObject m_player;
    [SerializeField] private GameObject[] m_projectiles;
    private int m_projectileindex = 0;
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
            m_projectiles[m_projectileindex].transform.position = transform.position;
            m_projectiles[m_projectileindex].SetActive(true);
            if (m_projectiles[m_projectileindex].TryGetComponent<Rigidbody2D>(out Rigidbody2D m_projectilerigidbody))
            {
                Debug.Log(m_projectiles.Length);
                Debug.Log("has rigidbody2d");
                Debug.Log(m_player.transform.position);
                m_projectilerigidbody.AddForce((m_player.transform.position - transform.position).normalized * m_projectilespeed, ForceMode2D.Impulse);
                m_projectileindex++;
                if (m_projectileindex >= m_projectiles.Length)
                {
                    Debug.Log("reset player index");
                    m_projectileindex = 0;
                }
            }
        }
    }
    IEnumerator Cooldown()
    {
        m_coroutinerunning = true;
        yield return new  WaitForSeconds(5f);
        m_coroutinerunning = false;
    }
}
