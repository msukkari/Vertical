using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [HideInInspector] public bool jump = false;
    public LayerMask whatIsGround;
    public float moveForce = 0.0001f;
    public float maxSpeed = 0.00000001f;
    public float jumpForce = 0.4f;
    public float friction = 0.1f;
    public Transform groundCheck;


    private bool grounded = false;
    private Rigidbody2D rb2d;


    // Use this for initialization
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);
        
        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h == 0)
            rb2d.velocity = new Vector2(rb2d.velocity.x * friction, rb2d.velocity.y);

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);
        Debug.Log(grounded);
        if (Input.GetAxis("Vertical") > 0 && grounded && !jump && rb2d.velocity.y <= 0)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }
}
