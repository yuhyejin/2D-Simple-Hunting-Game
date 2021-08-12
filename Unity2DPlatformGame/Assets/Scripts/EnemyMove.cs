using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //Rigidbody2D rigid;
    Vector3 pos;
    //SpriteRenderer spriteRenderer;
    public int nextMove;
    float maxSpeed = 2.0f;  // 이동속도
    float maxMove = 2.0f; // 좌,우로 이동 가능한 (x) 최대값

    void Start()
    {
        pos = transform.position;
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Move
        pos.x = maxMove * Mathf.Sin(Time.time * maxSpeed);
        int a;
        if(pos.x == maxMove)
        {
            pos.x *= -1;
            Debug.Log("성공");
        } else
            Debug.Log("실패");


        transform.position = pos;
    }
}
