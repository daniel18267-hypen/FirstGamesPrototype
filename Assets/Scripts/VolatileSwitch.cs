using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class VolatileSwitch : MonoBehaviour
{
    public UnityEvent<bool> TriggerDoor;
    public UnityEvent SubtractDoorCount;
    [SerializeField] private GameObject m_volatileswitch;
    private SpriteRenderer m_spriterenderer;
    private Rigidbody2D m_rigidbody;
    private Color m_red = new Color(1f, 0f, 0f, 0.58823529411f);
    private Color m_green = new Color(0f, 1f, 0f, 0.58823529411f);
    private bool m_hastriggered;
    private bool m_allswitchestriggered;
    private bool m_coroutinerunning;
    private Vector3 m_starttransform;
    private void Awake()
    {
        m_starttransform = transform.position;
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    public void TriggerVolatileSwitch()
    {

        m_spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        if (m_hastriggered == false)
        {
            Debug.Log("SwitchTriggered");
            TriggerDoor?.Invoke(true);
            m_hastriggered = true;
            m_spriterenderer.color = m_red;
            StartCoroutine(ResetSwitch());

        }
    }
    private IEnumerator ResetSwitch()
    {
        yield return new WaitForSeconds(0.1f);
        if (m_allswitchestriggered == false)
        {
            SubtractDoorCount?.Invoke();
            m_spriterenderer.color = m_green;
            m_hastriggered = false;
        }
        else if (m_allswitchestriggered == true)
        {
            Debug.Log("got here");
            m_spriterenderer.color = m_red;
            m_hastriggered = true;
        }

    }


    public void SwitchTriggeredHandler() // function called from event that permanently sets switch to red if the door was succesfully destroyed
    {
        m_allswitchestriggered = true;
    }
    public void ResetGameObject()
    {
        Debug.Log("Got here");
        transform.position = m_starttransform;
        m_rigidbody.linearVelocity = Vector3.zero;
    }

}


