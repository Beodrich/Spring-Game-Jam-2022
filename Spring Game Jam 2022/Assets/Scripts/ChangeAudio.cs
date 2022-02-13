using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAudio : MonoBehaviour
{
    private bool hasEntered=false;
    [SerializeField] private AudioSource music;
 void OnTriggerEnter2D(Collider2D other){
     if(other.gameObject.tag=="Player"&& !hasEntered){
         hasEntered=true;
         music.Play();

     }


 }
}
