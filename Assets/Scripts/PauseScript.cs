using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseScript : MonoBehaviour {

    [SerializeField]
    private GameObject PanelPause;
    [SerializeField]
    private GameObject uiGamePanel;
    [SerializeField]
    private GameObject ResumeButton;
    private bool isInPause = false;
    private event Action onPauseEvent;
    private static PauseScript instance;


    public static PauseScript Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PauseScript>();
            }
            return instance;
        }
    }

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Pause") && !isInPause)
        {
            isInPause = true;
            PanelPause.SetActive(true);
            ResumeButton.SetActive(true);
            uiGamePanel.SetActive(false);
            Time.timeScale = 0;
        }
	}

    public void OnResumeGameButtonDown()
    {
        isInPause = false;
        PanelPause.SetActive(false);
        ResumeButton.SetActive(true);
        uiGamePanel.SetActive(true);
        Time.timeScale = 1;
    }
}
