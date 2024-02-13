using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float runSpeed;
    public float jumpHeight;
    public Vector2 direction;
    public bool isGrounded;
    public bool jump;

    public float groundDetectionRange;

    public LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jump && isGrounded){
            Jump();
        }
        FlipBehavior();
    }

    void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    void FlipBehavior(){
        if (direction.x != 0){
            Vector3 currentScale = transform.localScale;

            transform.localScale = 
                new Vector3(Mathf.Abs(currentScale.x) * Mathf.Sign(direction.x), currentScale.y, currentScale.z);
        }
    }

    private void FixedUpdate(){
        GroundDetection();
        rb.velocity = new Vector2(direction.x * runSpeed, rb.velocity.y);
    }

    void GroundDetection(){
        RaycastHit2D groundRay = Physics2D.Raycast(transform.position, Vector2.down, groundDetectionRange, groundMask);
        isGrounded = groundRay.collider != null && rb.velocity.y <= 0;
    }
}
