using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAudio : MonoBehaviour
{
    private bool hasEntered=false;
    [SerializeField] private AudioSource jukebox;
    [SerializeField] private AudioClip areaMusic;
    [SerializeField] private AudioClip bossMusic;
    private void Start()
    {
        jukebox.clip = areaMusic;
        jukebox.Play();
    }

    void OnTriggerEnter2D(Collider2D other){
     if(other.gameObject.tag=="Player"&& !hasEntered)
        {
            hasEntered = true;
            jukebox.clip = bossMusic;
            jukebox.Play();

        }


 }
}
