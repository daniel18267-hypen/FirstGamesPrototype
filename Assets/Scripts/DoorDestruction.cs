using UnityEngine;
using UnityEngine.Events;
public class DoorDestruction : MonoBehaviour
{
    [SerializeField] private int m_switchcount;
    [SerializeField] private int m_volatileswitchcount;
    private int m_switchestriggered;
    private int m_volatileswitchestriggered;
    public UnityEvent VolatileSwitchesTriggered;
    public void DoorDestructionHandler(bool m_isvolatile)
    {
        m_switchestriggered += 1;
        if (m_isvolatile == true)
        {
            m_volatileswitchestriggered++;
            if (m_volatileswitchestriggered == m_volatileswitchcount)
            {
                VolatileSwitchesTriggered?.Invoke();
            }
        }
        Debug.Log(m_switchestriggered);
        if (m_switchcount == m_switchestriggered)
        {
            gameObject.SetActive(false);
            
        }  
    }
    public void DoorRemoveCount()
    {
        m_volatileswitchestriggered -= 1;
        m_switchestriggered -= 1;
    }
}
