using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public float speed =5;
    private Vector2 direction;
    private Animator animator;

    public static bool isPaused = false; // <--- Add this flag
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Update()
    {
        if (isPaused) return; // <--- Stop movement if paused
        TakeInput();
        Move();

    }
    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        if (direction.x != 0 || direction.y != 0)
        {
            SetAnimatoeMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1 , 0);
        }
    }

    private void TakeInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) 
        { 
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }

    private void SetAnimatoeMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir" ,direction.x);
        animator.SetFloat("yDir", direction.y);
        print(animator.GetFloat("xDir"));
    }
}
