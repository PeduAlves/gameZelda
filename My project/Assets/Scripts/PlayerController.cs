using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private Vector3 direction;
    private float horizontal;
    private bool isAtack;
    private bool isWalk;
    private float vertical;

    
    [Header("Player Config")]
    public float MovementSpeed = 3f;

    [Header("Attack Config")]
    public ParticleSystem FxAtk;



    void Start(){

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void Update(){
        
        Inputs();       
        MoveChar();
        UpdateAnimator();
        
    }


#region MeusMetodos

    //Ataque
    void Attack(){

        isAtack = true;
        animator.SetTrigger("Attack");
        FxAtk.Emit(1);
    }

    void AttackIsDone(){

        isAtack = false;
    }

    //Recebe inputs do sistema
    void Inputs(){

        //Direcao de movimento
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //Ataque
        if( Input.GetButtonDown("Fire1") && !isAtack ){

            Attack();
        }
    }

    //Rotação e alteração de isWalk (esta andando)
    void MoveChar(){

        direction = new Vector3( horizontal, 0, vertical ).normalized;
       
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
    }

    //Atualiza a variavel de movimento
    void UpdateAnimator(){

        animator.SetBool("isWalk", isWalk);
    }

#endregion

}

