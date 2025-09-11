using Pathfinding;
using UnityEngine;

public class Slime : MonoBehaviour, ILiving
{
    public float jumpForce = 5f;
    Vector3 targetPos;

    public bool isDead = false;
    float health = 5;
    Player player;
    AIPath path;
    Animator animator;

    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        path = GetComponent<AIPath>();
        targetPos = Player.Instance.transform.position;
        player = Player.Instance;

        // 2 saniye sonra baþlasýn, her 5 saniyede bir zýplasýn
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

        if (!isDead)
        {
            if (health <= 0)
            {
                path.maxSpeed = 0f;
                isDead = true;
                animator.SetTrigger("Death");
                CancelInvoke(nameof(JumpTowardsPlayer)); // Ölünce zýplamayý durdur
                Destroy(gameObject, 1.3f);
                player.Point++;
                player.SetPointVisual();
            }
            else
            {
                animator.SetTrigger("Hurt");
                path.maxSpeed = 0f;
            }
        }
    }

    private void JumpTowardsPlayer()
    {
        if (isDead) return;

        

        // 1. Animasyonu tetikle
        animator.SetTrigger("Jump");

        // 2. 0.7 saniye sonra hareket baþlasýn
        Invoke(nameof(StartJumpMove), 0.72f);

        // 3. Hareket baþladýktan .2 saniye sonra duracak
        Invoke(nameof(StopJump), 0.72f + .8f);
    }

    private void StartJumpMove()
    {
        if (isDead) return;

        
        targetPos = Player.Instance.transform.position;
        path.maxSpeed = jumpForce;
    }

    private void StopJump()
    {
        if (isDead) return;

        
        path.maxSpeed = 0f;
    }
}
