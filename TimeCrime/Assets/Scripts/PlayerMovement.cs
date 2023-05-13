using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float stopDistance = 0.1f;
    public float moveSpeed = 5f;
    public bool canMove;

    private Rigidbody2D rb;
    public Camera cam;
    public Animator playerAnim;

    Vector2 movement;
    Vector2 mousePos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        canMove = Vector2.Distance(rb.position, mousePos) >= stopDistance;


        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Tells animator, that player is moving
        if (movement.magnitude != 0 && canMove) playerAnim.SetBool("IsWalking", true);
        else playerAnim.SetBool("IsWalking", false);
    }

    private void FixedUpdate()
    {
        if(canMove)
            rb.MovePosition(rb.position + (Vector2)transform.TransformVector(movement * moveSpeed * Time.fixedDeltaTime));

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;
    }
}
