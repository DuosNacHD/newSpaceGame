
using UnityEngine;

public class EnemyCollider : MonoBehaviour, ILiving
{
    public bool IsAlive { get; private set; }
    public void Damage(float damage)
    {
        transform.parent.GetComponent<ILiving>().Damage(damage);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && transform.parent.GetComponent<ILiving>().IsAlive)
        {
            collision.GetComponent<ILiving>().Damage(1);
        }

    }
}
