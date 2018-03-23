using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float SpeedX;
    public float JumpSpeedY;

    bool facingRight, Jumping, attackNow;
    float Speed;

    Rigidbody2D rd;
    Animator anim;


	// Use this for initialization
	void Start () {

        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingRight = true;
        Jumping = false;
        


    }
	
	// Update is called once per frame
	void Update () {
        MovePlayer(Speed);


        if (Input.GetKeyDown(KeyCode.D))
        {
            Speed = SpeedX;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            Speed = 0;
            
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Speed = -SpeedX;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            Speed = 0;
            
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            attackNow = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            attackNow = false;
            anim.SetInteger("StatePlayer", 1);

        }

        if (attackNow)
        {
            anim.SetInteger("StatePlayer", 4);
        }



        if (Input.GetKeyDown(KeyCode.Space) && !Jumping)
        {
            Jumping = true;
            rd.velocity = new Vector2(rd.velocity.x, JumpSpeedY);
            anim.SetInteger("StatePlayer", 3);

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




   void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "GROUND")
        {
            Jumping = false;
            
            anim.SetInteger("StatePlayer", 1);
        }
    }


    public void MoveLeft()
    {
        Speed = -SpeedX; 
    }
    public void MoveRight()
    {
        Speed = SpeedX;
    }
    public void AttackEvent()
    {
        attackNow = true;
        
    }
    public void Jump()
    {

        if (!Jumping)
        {
            Jumping = true;
            rd.velocity = new Vector2(rd.velocity.x, JumpSpeedY);
            anim.SetInteger("StatePlayer", 3);
        }
       
    }

    public void StopMove()
    {
        Speed = 0;
        attackNow = false;
        anim.SetInteger("StatePlayer", 1);
    }
}
