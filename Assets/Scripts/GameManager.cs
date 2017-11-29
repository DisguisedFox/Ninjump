using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private int lifes = 3;
    private int coins = 0;
    private int CountEnemys = 3;
    [SerializeField] private Text textLifes;
    [SerializeField] private Text textcoins;
    [SerializeField] private Text textennemis;

    private const string TEXT_LIFES = "Lifes : ";
    private const string TEXT_COINS = "Coins : ";
    private const string TEXT_ENNEMIS = "Ennemis : ";

    // Use this for initialization
    void Start () {
        CountEnemys = GameObject.FindGameObjectsWithTag("Enemys").Length;
        textLifes.text = TEXT_LIFES + lifes;
        textcoins.text = TEXT_COINS + coins;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerWin()
    {
            SceneManager.LoadScene("WinMenu");
    }
    public void Vie()
    {
        lifes++;
        textLifes.text = TEXT_LIFES + lifes;
    
    }
    public void Coins()
    {
        coins++;
        textcoins.text = TEXT_COINS + coins;

    }
    public void PlayerDie()
    {
        
        lifes--;
        if(lifes > 0)
        {
            textLifes.text = TEXT_LIFES + lifes;
        }
        else
        {
            SceneManager.LoadScene("DieMenu");
        }
    }
    
}
