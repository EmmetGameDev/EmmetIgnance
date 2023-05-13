using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float stopDistance = 0.1f;
    public float moveSpeed = 5f;
    bool canMove;
    public float stopVelocity = 0.1f;

    [Range(0, 1)] public float rotationSmoothing;
    [Range(0, 1)] public float movementSmoothing;

    private Rigidbody2D rb;
    public Camera cam;
    public Animator playerAnim;

    float movement;
    Vector2 mousePos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        canMove = Vector2.Distance(rb.position, mousePos) >= stopDistance;


        movement = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Tells animator, that player is moving
        if (rb.velocity.magnitude > stopVelocity && canMove) playerAnim.SetBool("IsWalking", true);
        else playerAnim.SetBool("IsWalking", false);
    }

    private void FixedUpdate()
    {
        if (canMove)
            rb.velocity = Vector2.Lerp((Vector2)transform.up * movement * moveSpeed * Time.fixedDeltaTime, rb.velocity, movementSmoothing);
        else rb.velocity = Vector2.zero;

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = Mathf.LerpAngle(angle, rb.rotation, rotationSmoothing);
    }
}
