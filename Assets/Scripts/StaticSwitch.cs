using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class StaticSwitch : MonoBehaviour
{
    public UnityEvent<bool> TriggerDoor;
    private bool m_hastriggered;
    private SpriteRenderer m_spriterenderer;
    private void Awake()
    {
        // gets sprite renderer
        m_spriterenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public void TriggerStaticSwitch()
    {
        // if not triggered already, triggers door, passes in false so door script knows its not a volatile switch
        if (m_hastriggered == false)
        {
            TriggerDoor?.Invoke(false);
            // set colour to red and set has triggered to true so can't hit the same switch twice
            m_spriterenderer.color = Color.red;
            m_hastriggered = true;
        }
    }
}
