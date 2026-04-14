using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    private Image m_healthbar;
   [SerializeField] private float m_maxhealth = 100;
    private float m_health = 0;
    private void Awake()
    {
        m_health = m_maxhealth;
        m_healthbar = this.gameObject.GetComponentInChildren<Image>();
    }
    /// <summary>
    /// takes damage, passes in damage, sets the enemy healthbar to reflect current health
    /// </summary>
    /// <param name="damage"></param>
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
