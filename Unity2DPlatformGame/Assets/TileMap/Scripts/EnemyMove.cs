using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    Vector3 pos;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    public int nextMove;
    float maxSpeed = 2.0f;  // 이동속도
    float maxMove = 2.0f; // 좌,우로 이동 가능한 (x) 최대값

    void Start()
    {
        pos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Move
        pos.x = maxMove * Mathf.Sin(Time.time * maxSpeed);

        if(pos.x < -1)
            spriteRenderer.flipX = true;

        else if(pos.x > 1)
            spriteRenderer.flipX = false;

        transform.position = pos;
    }

    public void OnDamaged()
    {
        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Sprite Flip Y
        spriteRenderer.flipY = true;

        //Colloder Disable
        boxCollider.enabled = false;

        //die Effect Jump
        rigid.AddForce(Vector2.up * 30, ForceMode2D.Impulse);

        //Destroy
        Invoke("DeActive", 3);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
