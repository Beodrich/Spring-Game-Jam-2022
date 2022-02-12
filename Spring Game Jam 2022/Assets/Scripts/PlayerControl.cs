using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AnimatorController))]
public class PlayerControl : MonoBehaviour
{

    [SerializeField] private int speed;
    [SerializeField]private Camera mainCamera;


    [SerializeField]LineRenderer lineRenderer;

    private AnimatorController animatorLogic;

    private const string UP="up";

    private const string DOWN="down";

    private const string LEFT="left";

    private const string RIGHT="right";
   
    void Start(){
        animatorLogic=GetComponent<AnimatorController>();
    }
    // Update is called once per frame
    void Update()
    {
        float horz= Input.GetAxisRaw("Horizontal");
        float vert= Input.GetAxisRaw("Vertical");
        Vector2 playerDirection= new Vector2(horz,vert);
        transform.Translate(playerDirection*speed*Time.deltaTime);
        //DrawLine();

        if(Input.GetKeyDown(KeyCode.W)){
            animatorLogic.ChangeAnimationState(UP);
        }
        if(Input.GetKeyDown(KeyCode.D)){
            animatorLogic.ChangeAnimationState(RIGHT);
        }
        if(Input.GetKeyDown(KeyCode.A)){
            animatorLogic.ChangeAnimationState(LEFT);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            animatorLogic.ChangeAnimationState(DOWN);
        }
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

  void OnTriggerEnter2D(Collider2D other){
      if(other.gameObject.tag=="EnemyProjectile"){
          GameManager.instance.DoAction(1,GameManager.value.currentPlayerHp,false);
      }
  }

}
