using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    Vector3 pos;
    SpriteRenderer spriteRenderer;
    public int nextMove;
    float maxSpeed = 2.0f;  // 이동속도
    float maxMove = 2.0f; // 좌,우로 이동 가능한 (x) 최대값

    void Start()
    {
        pos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
}
