using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;

    public PlayerMove player;

    public GameObject[] Stages;
    public Image[] UIhealth;
    public Text UIScore;
    public GameObject RestartBtn;


    public void NextStage()
    {
        if(stagePoint == 300)
        {
            stageIndex++;
            totalPoint += stagePoint;
            stagePoint = 0;
        } 
        else
        {
            //Restart
            stagePoint = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Health Down
            health--;

            //Player Repositon
            collision.attachedRigidbody.velocity = Vector2.zero;
            collision.transform.position = new Vector3(-10, 0, -1);
        }
            
    }

    void Update()
    {
        UIScore.text = (totalPoint + stagePoint).ToString();
    }

    public void HealthDown()
    {
        
    }
}
