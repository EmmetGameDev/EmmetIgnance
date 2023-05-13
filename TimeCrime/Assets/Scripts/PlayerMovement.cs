using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("When unchecked uses world, when checked uses local")]
    public bool worldOrLocalSpaceMovement = false;
    public float stopDistance = 0.1f;
    [Header("")]

    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        if(worldOrLocalSpaceMovement && Vector2.Distance(rb.position, mousePos) > stopDistance) rb.MovePosition(rb.position + (Vector2)transform.TransformVector(movement * moveSpeed * Time.fixedDeltaTime));
        else rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;
    }
}
