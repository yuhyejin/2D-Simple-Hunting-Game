using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManger;
    public float maxSpeed;
    public float jumpPower;

    private enum State { idle, running, jumping, falling}
    private State state = State.idle;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private CapsuleCollider2D capColl;
    private LayerMask plattform;

    void Start()
    {
        capColl = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Jump
        if (Input.GetButton("Jump") && capColl.IsTouchingLayers(plattform))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, maxSpeed * jumpPower);

            if (rigid.velocity.y < .1f)
                state = State.falling;
        }
        else if (state == State.falling)
        {
            if (capColl.IsTouchingLayers(plattform))
            {
                state = State.idle;
            }
        }

        VeloctityState();
        anim.SetInteger("state", (int)state);

        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //Direction Sprite
        if(Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

    }

    void FixedUpdate()
    {
        //Move Speed
        float h = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(maxSpeed * h, rigid.velocity.y);

        //Landing Platform
        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if(rayHit.collider != null)
            {
                if (rayHit.distance < 0.9f)
                    state = State.jumping;
            }
        }
    }
    
    private void VeloctityState()
    {
        if(state == State.jumping)
        {

        }

        if (rigid.velocity.normalized.x == 0)
            state = State.idle;
        else
            state = State.running;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            //Point
            bool isCherry = collision.gameObject.name.Contains("Cherry");
            bool isGem = collision.gameObject.name.Contains("Gem");
            if (isCherry)
                gameManger.stagePoint += 20;

            else if (isGem)
                gameManger.stagePoint += 50;

            //Deactive Item
            collision.gameObject.SetActive(false);
        } 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //Attack
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            } else
                OnDamaged(collision.transform.position);
        }
    }

    void OnAttack(Transform enemy)
    {
        //Point
        gameManger.stagePoint += 30;

        // Reaction Force
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //Enemy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }

    void OnDamaged(Vector2 targetPos)
    {
        // Health Down
        gameManger.HealthDown();
        // Change Layer
        gameObject.layer = 11;

        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 20, ForceMode2D.Impulse);

        // Animation
        anim.SetTrigger("doDamaged");

        Invoke("OffDamaged", 3);
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Sprite Flip Y
        spriteRenderer.flipY = true;

        //Colloder Disable
        capColl.enabled = false;

        //die Effect Jump
        rigid.AddForce(Vector2.up * 30, ForceMode2D.Impulse);
    }
}
