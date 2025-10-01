using Pathfinding;
using UnityEngine;

public class Slime : MonoBehaviour, ILiving
{
    public float jumpForce = 5f;
    Vector3 targetPos;


    float health = 5;
    Player player;
    AIPath path;
    Animator animator;

    public bool IsAlive {  get; private set; } = true;

    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        path = GetComponent<AIPath>();
        player = Player.Instance;
        targetPos = player.transform.position;
        

        // 2 saniye sonra ba�las�n, her 5 saniyede bir z�plas�n
        InvokeRepeating(nameof(JumpTowardsPlayer), 0f, 2.5f);
    }

    private void Update()
    {
        path.destination = targetPos;
        Vector2 dir = path.desiredVelocity.normalized;

        if (dir.x > 0.1f) transform.localScale = new Vector3(1, 1, 1);
        if (dir.x < -0.1f) transform.localScale = new Vector3(-1, 1, 1);
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
                animator.SetTrigger("Death");
                CancelInvoke(nameof(JumpTowardsPlayer)); // �l�nce z�plamay� durdur
                Destroy(gameObject, 1.3f);
                player.Point++;
                player.SetPointVisual();
            }
            else
            {
                animator.SetTrigger("Hurt");
            }
        }
    }

    private void JumpTowardsPlayer()
    {
        if (!IsAlive) return;

        

        // 1. Animasyonu tetikle
        animator.SetTrigger("Jump");

        // 2. 0.7 saniye sonra hareket ba�las�n
        Invoke(nameof(StartJumpMove), 0.72f);

        // 3. Hareket ba�lad�ktan .2 saniye sonra duracak
        Invoke(nameof(StopJump), 0.7f + .8f);
    }

    private void StartJumpMove()
    {
        if (!IsAlive) return;

        
        targetPos = Player.Instance.transform.position;
        path.maxSpeed = jumpForce;
    }

    private void StopJump()
    {
        if (!IsAlive) return;

        
        path.maxSpeed = 0f;
    }
}
