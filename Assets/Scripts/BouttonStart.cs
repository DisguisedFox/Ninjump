using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BouttonStart : MonoBehaviour {

    [SerializeField]
    Button startbouton;
    void Start()
    {
       Button Clique = startbouton.GetComponent<Button>();
       Clique.onClick.AddListener(Load);
    }
    void Load()
    {
        SceneManager.LoadScene("FirstScene");
    }
    public void Uptdate  () {

		
	}
}
