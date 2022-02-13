using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AnimatorController))]
public class PlayerControl : MonoBehaviour
{

    [SerializeField] private int speed;
    [SerializeField]private Camera mainCamera;


    private bool canTakeDamage=true;

    [SerializeField] private float iFrameTime=1f;


    [SerializeField]LineRenderer lineRenderer;

    private AnimatorController animatorLogic;

    private const string UP="up";

    private const string DOWN="down";

    private const string LEFT="left";

    private const string RIGHT="right";


    private const string DEATH="death";
   
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


  public  void PlayerDealthLogic(){
        animatorLogic.ChangeAnimationState(DEATH);
    }
 


  void OnTriggerEnter2D(Collider2D other){
      if(other.gameObject.tag=="EnemyProjectile" && canTakeDamage){
          canTakeDamage=false;
          GameManager.instance.DoAction(1,GameManager.value.currentPlayerHp,false);
          StartCoroutine(IFrames());

      }

      IEnumerator IFrames(){
          yield return new WaitForSeconds(iFrameTime);
          canTakeDamage=true;
      }
  }

}
