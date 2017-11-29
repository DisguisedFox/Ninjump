using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
    private float life = 3;
    private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Heart")
        {
            life++;
            Debug.Log("Message : you won one life");
        }
    }

// Use this for initialization
void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
