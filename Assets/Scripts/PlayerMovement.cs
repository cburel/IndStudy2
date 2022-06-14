using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // save for efficiency
    private Rigidbody2D rb2d;   // keep private so no other scripts can get this

    // Start is called before the first frame update
    private void Start()
    {
        // get rb2d component for use later
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // get movement in x direction
        float dirX = Input.GetAxisRaw("Horizontal");

        // move in x direction
        rb2d.velocity = new Vector2(dirX * 7f, rb2d.velocity.y);    // keeps y-vel of frame before

        // jump controls
        if (Input.GetKeyDown("space"))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 14f);
        }

        // invert gravity controls
        if (Input.GetKeyDown("v"))
        {
            rb2d.gravityScale *= -1;
        }
    }
}
