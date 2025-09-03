using Unity.VisualScripting;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] GameInput gameInput;
    [SerializeField] float Speed = 1;

    bool isSprinting = false;

    Rigidbody2D rb2D;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        gameInput.SprintOff += GameInput_SprintOff;
        gameInput.SprintOn += GameInput_SprintOn;
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
        rb2D.linearVelocity = movDir * Speed * Time.deltaTime * 100;
        Animate(movDir);
    }
    void Animate(Vector2 movementVector)
    {
        if (movementVector != Vector2.zero)
        {
            animator.SetFloat("X", movementVector.x);
            animator.SetFloat("Y", movementVector.y);
        }
        animator.SetFloat("Speed", movementVector.sqrMagnitude);
    }
}
