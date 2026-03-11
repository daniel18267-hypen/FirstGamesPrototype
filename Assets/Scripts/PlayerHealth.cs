using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private float m_maxhealth = 100;
    private float m_health = 100;
    [SerializeField] private Image m_healthbar;
    
  public void DamageHandler(int m_amount)
    {
        m_health -= m_amount;
        UpdatehealthBar(m_health);
        Debug.Log(m_health);
        if (m_health <= 0)
        {
            PlayerDeath();
        }
    }
  public void HealHandler(int m_amount)
    {
        m_health += m_amount;
        if (m_health > m_maxhealth) // clamps health to max health
        {
            m_health = 100;
        }
        UpdatehealthBar(m_health);
    }
  private void UpdatehealthBar(float m_health)
    {
        Debug.Log(m_healthbar.fillAmount);
        m_healthbar.fillAmount = m_health / 100;
    }
  private void PlayerDeath()
    {

    }
}

 
