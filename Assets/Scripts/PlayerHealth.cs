using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private float m_health = 100;
    [SerializeField] private Image m_healthbar;
    /// <summary>
    /// called via event when player takes damage, updates health bar and triggers death if health <= 0
    /// </summary>
    /// <param name="m_amount"></param>
  public void DamageHandler(int m_amount)
    {
        m_health -= m_amount;
        UpdatehealthBar(m_health);
        if (m_health <= 0)
        {
            PlayerDeath();
        }
    }
    /// <summary>
    /// updates healthbar ui, called when player takes damage
    /// </summary>
    /// <param name="m_health"></param>
  private void UpdatehealthBar(float m_health)
    {
        m_healthbar.fillAmount = m_health / 100;
    }
    /// <summary>
    /// called when player health <0, reloads scene.
    /// </summary>
  private void PlayerDeath()
    {
        SceneManager.LoadScene(0);
    }
}

 
