using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private int m_maxhealth = 100;
    private int m_health = 100;
    private Image m_healthbar;
    
  private void DamageHandler(int m_amount)
    {
        m_health -= m_amount;
        UpdatehealthBar(m_health);
        if (m_health <= 0)
        {
            PlayerDeath();
        }
    }
  private void HealHandler(int m_amount)
    {
        m_health += m_amount;
        if (m_health > m_maxhealth) // clamps health to max health
        {
            m_health = 100;
        }
        UpdatehealthBar(m_health);
    }
  private void UpdatehealthBar(int m_health)
    {
        m_healthbar.fillAmount = m_health / 100;
    }
  private void PlayerDeath()
    {

    }
}

 
