using UnityEngine;
public class DoorDestruction : MonoBehaviour
{
    [SerializeField] private int m_switchcount;
    private int m_switchestriggered;
    private bool m_hastriggered;
    public void DoorDestructionHandlerStatic()
    {
         m_switchestriggered += 1;
        if (m_switchcount == m_switchestriggered)
        {
           gameObject.SetActive(false);
        }  
    }
}
