using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager _GM;
    private Animator anim;
    private CharacterController controller;
    private Vector3 direction;
    private float horizontal;
    private Collider[] hitinfo;
    private bool isAtack;
    private bool isHurt = false;
    private bool isDie;
    private bool isWalk;
    private float vertical;

    
    [Header("Player Config")]
    public int HP = 15;
    public float MovementSpeed = 3f;

    [Header("Attack Config")]
    public int AmountDmg = 10;
    public ParticleSystem FxAtk;
    public LayerMask HitMask;
    public Transform HitBox;
    [Range(0.2f, 1f)]
    public float HitRange;
    



    void Start(){

        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        _GM = FindObjectOfType(typeof(GameManager)) as GameManager;
    }
    void Update(){
        
        Inputs();       
        MoveChar();
        UpdateAnimator();
        
    }

    private void OnTriggerEnter(Collider other) {
        
        if( other.gameObject.tag == "TakeDamage" ){

            GetHit( _GM.SlimeHitDamage );
        }
    }

#region MeusMetodos

    //Ataque
    void Attack(){

        isAtack = true;
        anim.SetTrigger("Attack");
        FxAtk.Emit(1);

        hitinfo = Physics.OverlapSphere(HitBox.position, HitRange, HitMask);

        foreach( Collider c in hitinfo){

            c.gameObject.SendMessage("GetHit", AmountDmg, SendMessageOptions.DontRequireReceiver);
        }
    }

    void AttackIsDone(){

        isAtack = false;
    }

    void GetHit( int AmountDmg ){

        if( isDie || isHurt ) return;

        HP -= AmountDmg;
        isHurt = true;        
        StartCoroutine("IsHurt");

        if( HP > 0 ){

            anim.SetTrigger("GetHit");
        }
        else{

            isDie = true;
            anim.SetTrigger("Die");
        }

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

        anim.SetBool("isWalk", isWalk);
    }

    IEnumerator IsHurt(){

        yield return new WaitForSeconds(_GM.PlayerGetHitDelay);
        isHurt = false;
    }

#endregion

    private void OnDrawGizmos() {
        
        Gizmos.DrawWireSphere(HitBox.position, HitRange);
    }

}

