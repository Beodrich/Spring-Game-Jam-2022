using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [SerializeField] private int speed;
    [SerializeField]private Camera mainCamera;


    [SerializeField]LineRenderer lineRenderer;


    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        float horz= Input.GetAxisRaw("Horizontal");
        float vert= Input.GetAxisRaw("Vertical");
        Vector2 playerDirection= new Vector2(horz,vert);
        transform.Translate(playerDirection*speed*Time.deltaTime);
        //DrawLine();
    }
    public Vector2 GetMousePos(){
        return Input.mousePosition;
        
    }

   void DrawLine(){
       lineRenderer.enabled=true;
       List<Vector3> pos= new List<Vector3>();
       pos.Add(gameObject.transform.position);
       pos.Add(GetMousePos());
      
       lineRenderer.SetPositions(pos.ToArray());
       lineRenderer.startWidth=0.8f;
       lineRenderer.endWidth=0.8f;

       //lineRenderer.useWorldSpace=true;

   }
   
  

}
