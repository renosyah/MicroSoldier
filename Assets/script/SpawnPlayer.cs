using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {
    public GameObject objectSpawn;
    Vector2 whereSpawn;
    public float SpawnRate;
    float randomF;
    float NextSpawn = 0.0f;


	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > NextSpawn)
        {
            NextSpawn = Time.time + SpawnRate;
            randomF = Random.Range(-8.4f, 8.4f);
            whereSpawn = new Vector2(randomF, transform.position.x);
            Instantiate(objectSpawn, whereSpawn, Quaternion.identity);
        }
       

    }
}
