using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [HideInInspector] public bool jump = false;
    public LayerMask whatIsGround;
    public float moveForce = 0.1f;
    public float maxSpeed = 0.3f;
    public float jumpForce = 0.4f;
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
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);
        Debug.Log(grounded);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (jump)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }
}
