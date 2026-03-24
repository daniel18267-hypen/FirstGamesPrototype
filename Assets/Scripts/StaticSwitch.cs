using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class StaticSwitch : MonoBehaviour
{
    public UnityEvent<bool> TriggerDoor;
    private bool m_hastriggered;
    private SpriteRenderer m_spriterenderer;
    public void TriggerStaticSwitch()
    {
        

        m_spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        if (m_hastriggered == false)
        {
            Debug.Log("SwitchTriggered");
            TriggerDoor?.Invoke(false);
            m_spriterenderer.color = Color.red;
            m_hastriggered = true;
        }
        
        
    }
}
