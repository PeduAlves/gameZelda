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

    void Start(){

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void Update(){
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if( Input.GetButtonDown("Fire1") ){

            animator.SetTrigger("Attack");
        }

        direction = new Vector3( horizontal, 0, vertical ).normalized;

        if( direction.magnitude > 0.1f){
            
            float targetAngle = Mathf.Atan2( direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler( 0, targetAngle, 0);
            isWalk = true;
        }
        else{

            isWalk = false;
        }

        controller.Move( direction * MovementSpeed * Time.deltaTime );

        animator.SetBool("isWalk", isWalk);
    }
}
