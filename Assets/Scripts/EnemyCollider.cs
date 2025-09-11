using UnityEngine;

public class EnemyCollider : MonoBehaviour, ILiving
{
    public void Damage(float damage)
    {
        transform.parent.GetComponent<Slime>().Damage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player" && !transform.parent.GetComponent<Slime>().isDead)
        {
            collision.GetComponent<ILiving>().Damage(1);
        }

    }
}
