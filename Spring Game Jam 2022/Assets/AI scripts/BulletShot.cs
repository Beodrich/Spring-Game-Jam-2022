using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
  public float copespeedX = 0f;
   public float copespeedY = 0f;
   public float angle = 0 * Mathf.Rad2Deg;
  /*        Vector2 VectorFromAngle (float theta) {
     return new Vector2 (Mathf.Cos(theta), Mathf.Sin(theta)); 
     
      
  
 }
 transform.Translate (VectorFromAngle (theta)); */
    
    
    // Start is called before the first frame update
    void awake()
    {

        copespeedX = +1f;
        copespeedY = +1f;
        copespeedX = -1f;
        copespeedY = -1f;

        Vector2 MovDir = new Vector2(copespeedX, copespeedY).normalized;
        Vector2 MovAng = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        //Vector2 MovAng = new Vector2(Mathf.Cos(theta),Mathf.Sin(theta))
        float movespeed = 6f; 
        //float theta = 15 * Mathf.Rad2Deg;
        transform.Translate (MovDir *  movespeed * MovAng);
    
   
    } 

    //pull me

    // Update is called once per frame
    /*
    void Update()
    {
        
    } */

    
}
