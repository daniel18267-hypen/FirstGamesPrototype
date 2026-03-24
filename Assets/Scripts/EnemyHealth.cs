using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    private Image m_healthbar;
    private float m_maxhealth = 100;
    private float m_health = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        m_health = m_maxhealth;
        m_healthbar = this.gameObject.GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    public void RecieveDamage(float damage)
    {
        m_health = m_health - damage;
        m_healthbar.fillAmount = m_health / m_maxhealth;
        if (m_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
