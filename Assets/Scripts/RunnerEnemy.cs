using Pathfinding;
using UnityEngine;

public class RunnerEnemy : MonoBehaviour , ILiving
{
    [SerializeField] float Speed = 1;

    Vector3 target;


    float health = 5;
    Player player;
    AIPath path;

    public bool IsAlive { get; private set; } = true;

    private void Start()
    {
        player = Player.Instance;
        path = GetComponent<AIPath>();
        
    }
    private void Update()
    {
        if (IsAlive)
        {
            target = Player.Instance.gameObject.transform.position;
            path.maxSpeed = Speed;
            path.destination = target;
        }
    }
    public void Damage(float damage)
    {
        health -= damage;

        if (IsAlive)
        {
            if (health <= 0)
            {
                path.maxSpeed = 0f;
                
                IsAlive = false;
                Destroy(gameObject, 1.3f);
                player.Point++;
                player.SetPointVisual();
            }
            else
            {
                
            }
        }
    }
}
