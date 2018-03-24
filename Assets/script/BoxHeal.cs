using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxHeal : MonoBehaviour
{

    Rigidbody2D rd;
    Animator anim;
    bool itsHit;
    Text score;
   

    // Use this for initialization
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        itsHit = false;
        score = GameObject.Find("GameScore").GetComponent<Text>();
        

    }

    // Update is called once per frame
    void Update()
    {

    }
    int Hp = 0;

    private void OnCollisionEnter2D(Collision2D other)
    {


        if (other.gameObject.tag == "PLAYERSWORD")
        {
            anim.SetInteger("ItsHit", 2);
            if (Hp == 2)
            {
                Destroy(this.gameObject);
                GameObject.Find("player").GetComponent<PlayerControl>().RestoreHp();

            }
            Hp++;

        }
        else
        {
            anim.SetInteger("ItsHit", 1);
        }

    }
}