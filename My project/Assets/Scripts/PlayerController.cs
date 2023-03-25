using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private bool isWalk;
    private Vector3 direction;

    //Header serve para aparecer o titulo como caixinha na unity
    [Header("Player Config")]
    public float MovementSpeed = 3f;

    [Header("Camera")]
    public GameObject CamB;

    void Start(){

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void Update(){
        
        //Direcao de movimento
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Ataque
        if( Input.GetButtonDown("Fire1") ){

            animator.SetTrigger("Attack");
        }

        direction = new Vector3( horizontal, 0, vertical ).normalized;

        //Rotação e verificação de isWalk (esta andando)
        if( direction.magnitude > 0.1f){
            
            float targetAngle = Mathf.Atan2( direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler( 0, targetAngle, 0);
            isWalk = true;
        }
        else{

            isWalk = false;
        }

        //Movimentação
        controller.Move( direction * MovementSpeed * Time.deltaTime );

        animator.SetBool("isWalk", isWalk);
    }

    private void OnTriggerEnter(Collider other) {

        switch( other.gameObject.tag ){

            case "CamTrigger":
                CamB.SetActive(true);
                break;
        }       
    }
    private void OnTriggerExit(Collider other) {
        
        switch( other.gameObject.tag ){

            case "CamTrigger":
                CamB.SetActive(false);
                break;
        }
    }
}
