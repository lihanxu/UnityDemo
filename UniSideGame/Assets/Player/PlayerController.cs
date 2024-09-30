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
    Animator anim;
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerDead";
    string nowAnime = "PlayerStop";
    string oldAnime = "PlayerStop";
    public static string gameState = "playing";

    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameState = "playing";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != "playing") {
            return;
        }
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
        if (gameState != "playing") {
            return;
        }
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundMask);
        if (onGround || axisH != 0) {
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }

        if (onGround && goJump) {
            Vector2 force = new(0, jump);
            rbody.AddForce(force, ForceMode2D.Impulse);
            goJump = false;
        }
        if (onGround) {
            nowAnime = axisH == 0 ? stopAnime : moveAnime;
        } else {
            nowAnime = jumpAnime;
        }
        if (nowAnime != oldAnime) {
            anim.Play(nowAnime);
            oldAnime = nowAnime;
        }
    }

    void Jump() {
        goJump = true;
        Debug.Log("按下了跳跃键");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Goal") {
            GameGoal();
        } else if (other.gameObject.tag == "Dead") {
            GameOver();
        }
    }

    void GameOver() {
        Debug.Log("游戏结束");
        anim.Play(deadAnime);
        gameState = "gameover";
        GameStop();
        GetComponent<CapsuleCollider2D>().enabled = false;
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    void GameGoal() {
        Debug.Log("游戏胜利");
        anim.Play(goalAnime);
        gameState = "gameclear";
        GameStop();
    }

    void GameStop() {
        Debug.Log("停止游戏");
        rbody.velocity = new Vector2(0, 0);
    }
}
