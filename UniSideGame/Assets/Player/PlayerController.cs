using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH;
    public float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        axisH = Input.GetAxis("Horizontal");
        if (axisH > 0) {
            Debug.Log("right");
            transform.localScale = new Vector2(1, 1);
        } else if (axisH < 0) {
            Debug.Log("left");
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void FixedUpdate()
    {
        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
    }
}
