using UnityEngine;
using UnityEngine.Events;
public class DoorDestruction : MonoBehaviour
{
    [SerializeField] private int m_switchcount;
    [SerializeField] private int m_volatileswitchcount;
    private int m_switchestriggered = 0;
    private int m_volatileswitchestriggered = 0;
    public UnityEvent VolatileSwitchesTriggered;
    public void DoorDestructionHandler(bool m_isvolatile)
    {
        m_switchestriggered++;
        if (m_isvolatile == true)
        {
           
            m_volatileswitchestriggered++;
            Debug.Log($"volatile switches triggered,{m_volatileswitchestriggered}");
            if (m_volatileswitchestriggered == m_volatileswitchcount)
            {
                Debug.Log("GOT TO IMPORTANT");
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
        m_volatileswitchestriggered--;
        m_switchestriggered--;
    }
}
