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
        Debug.Log(m_volatileswitchestriggered);
        if (m_switchcount == m_switchestriggered)
        {
            gameObject.SetActive(false);
            
        }  
    }
    public void DoorRemoveCount()
    {
        Debug.Log("door remove conut triggered");
        m_volatileswitchestriggered--;
        m_switchestriggered--;
    }
}
