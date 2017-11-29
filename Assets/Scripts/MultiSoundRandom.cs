using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSoundRandom : MonoBehaviour {

    [SerializeField]
    private AudioClip[] tablesounds;
    private AudioSource audioSource;
	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
	}

    public void PlaySound()
    {
        int indexSoundRandom = Random.Range(0, tablesounds.Length);
        audioSource.clip = tablesounds[indexSoundRandom];
        audioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
