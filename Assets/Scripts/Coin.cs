using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    private float coin = 0;
    private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            coin++;
            Debug.Log("Message : you won a coin");
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
