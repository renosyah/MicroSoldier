using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{

    public float SpeedX;
    public float JumpSpeedY;

    bool facingRight, Jumping, attackNow;
    float Speed;
    Transform leftWayPoint, RightWayPoint;

    Rigidbody2D rd;
    Animator anim;

    bool moveRight;
    Text score;

    // Use this for initialization
    void Start()
    {

        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingRight = true;
        Jumping = false;

        leftWayPoint = GameObject.Find("WAYPOINT_LEFT").GetComponent<Transform>();
        RightWayPoint = GameObject.Find("WAYPOINT_RIGHT").GetComponent<Transform>();
        attackNow = true;
        moveRight = true;
        Speed = SpeedX;

        score = GameObject.Find("GameScore").GetComponent<Text>();
    }


    // Update is called once per frame
    void Update()
    {

        MovePlayer(Speed);

        if (transform.position.x > RightWayPoint.position.x)
       
        {

            Speed = -SpeedX;
            

        }


        if (transform.position.x < leftWayPoint.position.x)
        {
           
            Speed = SpeedX;
        }
        




        if (attackNow)
        {
            anim.SetInteger("StatePlayer", 4);
        }



    }

    void MovePlayer(float speed)
    {
        if (speed < 0 && !Jumping || speed > 0 && !Jumping)
        {
            anim.SetInteger("StatePlayer", 2);
            flip();
        }

        if (speed == 0 && !Jumping)
        {
            anim.SetInteger("StatePlayer", 1);
        }
        rd.velocity = new Vector3(speed, rd.velocity.y, 0);

    }

    void flip()
    {
        if (Speed < 0 && facingRight || Speed > 0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }



    int HP = 5;
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "GROUND")
        {
            Jumping = false;

            anim.SetInteger("StatePlayer", 1);
        }
        if (other.gameObject.tag == "GameItem")
        {
            Jumping = false;

            anim.SetInteger("StatePlayer", 1);
        }
        if (other.gameObject.tag == "PLAYERSWORD")
        {
            HP--;
            Jumping = false;
            if (HP == 0)
            {
                Destroy(this.gameObject);
                score.text = (int.Parse(score.text) + 200) + "";
            }

            else if (HP <= 3|| HP == 3 || HP == 4)
            {
                Destroy(GameObject.Find("Enemy(Clone)/arms_1/shield"));


            }
            else if (HP <= 2 || HP == 2 || HP == 1)
            {
                Destroy(GameObject.Find("Enemy(Clone)/head/helm"));
            }

        }
        
    }

}