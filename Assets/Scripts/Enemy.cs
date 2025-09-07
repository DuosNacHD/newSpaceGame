using UnityEngine;
public class Enemy : MonoBehaviour, ILiving
{
    float health = 5;
    Player player;
    private void Start()
    {
        player = Player.Instance;
    }
    public void Damage(float damage)
    {
        health -= damage;
        
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        if (health <= 0)
        {
            Destroy(gameObject);
            player.Point++;
            player.SetPointVisual();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            collision.GetComponent<ILiving>().Damage(1);
        }
    }

}