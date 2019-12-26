using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip[] songs = new AudioClip[5];
    public int currentSong = 0;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        setSong();
	}

    public void setSong()
    {
        audioSource.Stop();
        if (currentSong >= 5) currentSong = 0;
        audioSource.clip = songs[currentSong];
        audioSource.Play();
        currentSong++;
    }
	
}
