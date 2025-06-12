using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 5;
    private Vector2 direction;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.Instance.IsInputBlocked) return;

        HandleInventoryToggle();  // Will only open if input is allowed
        TakeInput();
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (direction != Vector2.zero)
        {
            SetAnimatorMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }

    private void HandleInventoryToggle()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.ToggleInventory();
        }

    }

    private void TakeInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) direction += Vector2.up;
        if (Input.GetKey(KeyCode.A)) direction += Vector2.left;
        if (Input.GetKey(KeyCode.S)) direction += Vector2.down;
        if (Input.GetKey(KeyCode.D)) direction += Vector2.right;
    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
    }
}
