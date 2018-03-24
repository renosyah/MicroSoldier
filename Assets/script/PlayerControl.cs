using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour {

    public float SpeedX;
    public float JumpSpeedY;

    bool facingRight, Jumping, attackNow;
    float Speed;

    Rigidbody2D rd;
    Animator anim;

    Text score;
    Text HEALTHPLAYER;
    
    GameObject playerObj;
    GameObject ButtonPlayAggain;

 
    AudioSource audioPlayer;


    // Use this for initialization
    void Start () {

        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingRight = true;
        Jumping = false;
        score = GameObject.Find("GameScore").GetComponent<Text>();
        HEALTHPLAYER = GameObject.Find("HEALTHPLAYER").GetComponent<Text>();
        playerObj = GameObject.Find("player");
        ButtonPlayAggain = GameObject.Find("ButtonPlayAgain");

        audioPlayer = GetComponent<AudioSource>();


        ButtonPlayAggain.SetActive(false);
    }
    
	
	// Update is called once per frame
	void Update () {
        MovePlayer(Speed);
        if (playerObj == null)
        {
            score.text = "GAME OVER, Score " + score.text;
        }

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



    int HP = 5;
    void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.tag == "GROUND")
        {
            Jumping = false;
            
            anim.SetInteger("StatePlayer", 1);
        }
        if (other.gameObject.tag == "GameItem")
        {
            Jumping = false;

            anim.SetInteger("StatePlayer", 1);
        }
        if (other.gameObject.tag == "ENEMYSWORD")
        {
            Jumping = false;
            HP--;
           
            
 

            if (HP == 0)
            {
                score.text = "GAME OVER! \nYour Score " + score.text;
                Destroy(this.gameObject);
                ButtonPlayAggain.SetActive(true);

            }
            else if (HP <= 3 || HP == 3 || HP == 4)
            {
                Destroy(GameObject.Find("player/arms_1/shield"));


            }
            else if (HP <= 2 || HP == 2 || HP == 1)
            {
                Destroy(GameObject.Find("player/head/helm"));
            }
            HEALTHPLAYER.text = "x "+ HP + "";
             
        }
    }

    public void RestoreHp()
    {
        HP = 5;
        HEALTHPLAYER.text = "x " + HP + "";
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
        
        anim.SetInteger("StatePlayer", 1);
    }

    public void StopAttack()
    {
        
        attackNow = false;
    }

    

    public void PlaySwingSword()
    {
        audioPlayer.Play();
       
    }
    public void StopSwingSword()
    {
        audioPlayer.Stop();
    }

}
