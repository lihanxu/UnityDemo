using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH;
    public float speed = 3f;
    public float jump = 9.0f;
    public LayerMask groundMask;
    bool onGround = false;
    bool goJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");
        if (axisH > 0) {
            Debug.Log("right");
            transform.localScale = new Vector2(1, 1);
        } else if (axisH < 0) {
            Debug.Log("left");
            transform.localScale = new Vector2(-1, 1);
        }
        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
    }

    void FixedUpdate()
    {
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundMask);
        if (onGround || axisH != 0) {
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }

        if (onGround && goJump) {
            Vector2 force = new(0, jump);
            rbody.AddForce(force, ForceMode2D.Impulse);
            goJump = false;
        }
    }

    void Jump() {
        goJump = true;
        Debug.Log("按下了跳跃键");
    }
}
