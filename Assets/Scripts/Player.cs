using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Playgama;
using Playgama.Modules.Advertisement;

public class Player : MonoBehaviour, ILiving
{
    public int Point;

    public static Player Instance;

    [SerializeField] float maxHealth;
    [SerializeField] GameInput gameInput;
    [SerializeField] float Speed = 1;


    GameController gameController;
    float health;
    float attackSpeed = 1f/4f,attackingTimer;
    bool isSprinting,isAttacking;
    Rigidbody2D rb2D;
    Animator animator,armAnimator,weaponAnimator;
    GameObject attackArea;

    private void Awake()
    {
        health = maxHealth;
        Instance = this;
        animator = GetComponent<Animator>();
        armAnimator = transform.Find("Arm").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Time.timeScale = 1f;
        gameController = GameController.Instance;
        gameInput.SprintOff += GameInput_SprintOff;
        gameInput.SprintOn += GameInput_SprintOn;
        gameInput.Attack += GameInput_Attack;
        attackArea = transform.Find("AttackArea").gameObject;
        SetPointVisual();

    }

    private void GameInput_Attack(object sender, System.EventArgs e)
    {
        if (!isAttacking)
        {
            armAnimator.SetTrigger("Attack");
            weaponAnimator.SetTrigger("Attack");
            animator.SetTrigger("Attack");
            isAttacking = true;
        }
    }
    private void GameInput_SprintOn(object sender, System.EventArgs e)
    {
        isSprinting = true;
    }
    private void GameInput_SprintOff(object sender, System.EventArgs e)
    {
        isSprinting = false;
    }
    private void Update()
    {
        Movement();
    }
    void Movement()
    {
        
        Vector2 movDir = gameInput.GetInputVector();
        if (isSprinting)
        {
            movDir = movDir * 2;
        }

        if (isAttacking)
        {
            if (attackingTimer <= 0)
            {
                attackingTimer = attackSpeed;
            }
            attackingTimer -= Time.deltaTime ;
            
            if (attackingTimer <= 0)
            {

                attackArea.GetComponent<PlayerAttack>().Attack();
                isAttacking = false;

            }
        }
        Animate(movDir);
        if (isAttacking)
        {
            movDir = Vector2.zero;
        }

        rb2D.linearVelocity = movDir * Speed * Time.deltaTime * 100;
    }
   
    
    void Animate(Vector2 movementVector)
    {
        if (movementVector != Vector2.zero)
        {
            animator.SetFloat("X", movementVector.x);
            animator.SetFloat("Y", movementVector.y);
            armAnimator.SetFloat("X", movementVector.x);
            armAnimator.SetFloat("Y", movementVector.y);
            weaponAnimator.SetFloat("X", movementVector.x);
            weaponAnimator.SetFloat("Y", movementVector.y);
            attackArea.transform.localPosition = movementVector;
        }
        
            animator.SetFloat("Speed", movementVector.sqrMagnitude);
        
        
    }

    public void Damage(float damage)
    {
        health -= damage;
        animator.SetTrigger("Hurt");
        gameController.SetHealtBarVisual(health/maxHealth);
        if (health <= 0)
        {
            Bridge.advertisement.ShowInterstitial();

            PlayerPrefs.SetFloat("Point", Point);

            SceneManager.LoadScene(0);
        }
    }
    public void SetPointVisual()
    {
        gameController.SetScoreVisual(Point);
    } 
}
