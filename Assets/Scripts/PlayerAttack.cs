using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
     List<GameObject> livings;
    private void Start()
    {
        livings = new List<GameObject>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ILiving>() != null)
        {
            livings.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ILiving>() != null)
        {
            livings.Remove(collision.gameObject);
        }
    }

    public void Attack()
    {
        if (livings.Count > 0)
        {
            for (int i = livings.Count - 1; i >= 0; i--)
            {
                GameObject living = livings[i];

                if (living != null)
                {
                    living.GetComponent<ILiving>().Damage(3);
                }

                // living null olmuþsa listeden çýkar
                if (living == null)
                {
                    livings.RemoveAt(i);
                }
            }
        }
    }
}
