using UnityEngine;
using UnityEngine.Events;
public class DoorDestruction : MonoBehaviour
{
    [SerializeField] private int m_switchcount;
    private int m_switchestriggered;
    public UnityEvent DoorDestroyed;
    public void DoorDestructionHandler()
    {
         m_switchestriggered += 1;
        if (m_switchcount == m_switchestriggered)
        {
            DoorDestroyed?.Invoke();
            gameObject.SetActive(false);
            
        }  
    }
    public void DoorRemoveCount()
    {
        m_switchestriggered -= 1;
    }
}
