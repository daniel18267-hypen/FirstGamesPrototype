using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
public class LavaDamage : MonoBehaviour
{
    public UnityEvent<int> DamagePlayer;
    private int damage;
    private bool isgrappling;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isgrappling == false)
        {
            DamagePlayer?.Invoke(damage);
        }
    }

    private void GrappleStateHandler(bool grapplestate)
    {
        isgrappling = grapplestate;
    }
}
