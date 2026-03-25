using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
public class LavaDamage : MonoBehaviour
{
    public UnityEvent<int> DamagePlayer;
    private int m_damage = 1;
    private bool m_isgrappling;
    private bool m_coroutinerunning;
    private void OnTriggerStay2D(Collider2D collision)
    {
        

        if (collision.gameObject.tag == "Player"  && m_isgrappling == false && m_coroutinerunning == false)
        {
            Debug.Log("collided with lava tile");
            StartCoroutine(Cooldown());
            DamagePlayer?.Invoke(m_damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "volatileswitch")
        {
            
            if (collision.gameObject.TryGetComponent<VolatileSwitch>(out VolatileSwitch m_volatileswitch))
            {
                m_volatileswitch.ResetGameObject();
            }
        }
    }

    public void GrappleStateHandler(bool grapplestate)
    {
        m_isgrappling = grapplestate;
    }
    private IEnumerator Cooldown()
    {
        m_coroutinerunning = true;
        yield return new WaitForSeconds(1);
        m_coroutinerunning = false;
        Debug.Log("End Coroutine");
    }
}
