using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeIA : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    private Vector3 destination;
    private GameManager _GM;
    private bool isDie;
    private bool isWalk;
    private bool isAlert;
    public bool isAttack = false;
    private int idWayPoint;
    private bool isPlayerVisible;

    public int HP = 3;
    public enemyState state;
    

    void Start(){

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        _GM = FindObjectOfType(typeof(GameManager)) as GameManager;

        ChangeState(state);
    }
    void Update() {
        
        StateManager();

        if( agent.desiredVelocity.magnitude >= 0.1f){

            isWalk = true;
        }
        else{

            isWalk = false;
        }

        anim.SetBool("IsWalk", isWalk);
    }

    void OnTriggerEnter(Collider other) {
        
        if( other.gameObject.tag == "Player" ){
            
            isPlayerVisible = true;

            if( state == enemyState.IDLE || state == enemyState.PATROL ){

            ChangeState( enemyState.ALERT );
            }
        } 
    }

    void OnTriggerExit(Collider other) {
        
        if( other.gameObject.tag == "Player" ){

            isPlayerVisible = false;
        }
    }

#region MeusMetodos

    void Ataque(){

        if ( !isAttack && isPlayerVisible == true ){

            isAttack = true;
            anim.SetTrigger("Attack");
        }
        AttackIsDoneSlime();
        
    }
    void AttackIsDoneSlime(){

        StartCoroutine("AttackDelay");
    }

    void GetHit( int amount){

        if(isDie) return;

        HP -= amount;
        
        if( HP > 0){

            ChangeState( enemyState.FURY );
            anim.SetTrigger("GetHit");
        }
        else{
        
            anim.SetTrigger("Die");
            StartCoroutine("Died");
        }
    }

    void LookAt(){

        
        Vector3 lookDirection = ( _GM.Player.position - transform.position ).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp( transform.rotation, lookRotation, _GM.SlimeLookAtSpeed * Time.deltaTime );
    }

    int randomize(){

        int rand = Random.Range( 0, 100 );
        return rand; 
    }

    void StayStill(int chance){

        if( randomize() < chance ){

            ChangeState(enemyState.IDLE);
        }
        else{

            ChangeState(enemyState.PATROL);
        }
    }
    
    void StateManager(){

        switch( state ){
            
            case enemyState.ALERT:

                if( isPlayerVisible ){

                    LookAt();
                    StartCoroutine("ALERT");
                    ChangeState( enemyState.FOLLOW );
                }
                else{

                    StayStill(10);
                }
                

            break;

            case enemyState.FOLLOW: 
                
                if( isPlayerVisible ){
                    
                    LookAt();

                    destination = _GM.Player.position;
                    agent.destination = destination;
                    
                    if( agent.remainingDistance <= agent.stoppingDistance){
                        Ataque();
                    }
                }
                else{

                    StayStill(10);
                }
                
            break;
            
            case enemyState.FURY:
               
                LookAt();

                destination = _GM.Player.position;
                agent.destination = destination;
                if( agent.remainingDistance <= agent.stoppingDistance){
                    Ataque();
                }
                
            break;
        }
    }

    void ChangeState( enemyState newState ){

        //Para todas as corotinas
        StopAllCoroutines();
        state = newState;
        isAlert = false;
        isAttack = false;

        switch( state ){
            
            case enemyState.ALERT:

                destination = transform.position;
                agent.stoppingDistance = 0;
                agent.destination = destination;
                isAlert = true;
                anim.SetBool("IsAlert", isAlert);

                StartCoroutine("ALERT");
            break;
            case enemyState.FOLLOW:

                agent.stoppingDistance = _GM.SlimeDistanceToAttack;                    

                StartCoroutine("FOLLOW");
            break;
            case enemyState.FURY:

                agent.stoppingDistance = _GM.SlimeDistanceToAttack;

                StartCoroutine("FURY");
            break;
            case enemyState.IDLE:

                destination = transform.position;
                agent.stoppingDistance = 0;
                agent.destination = destination;

                StartCoroutine("IDLE");
            break;
            case enemyState.PATROL:

                idWayPoint = Random.Range( 0, _GM.SlimeWayPoints.Length );
                destination = _GM.SlimeWayPoints[idWayPoint].position;
                agent.stoppingDistance = 0;
                agent.destination = destination;

                StartCoroutine("PATROL");
            break;
        }
    }

    IEnumerator ALERT(){

        yield return new WaitForSeconds( _GM.SlimeAlertTime );

    }

    IEnumerator FOLLOW(){

        yield return new WaitForSeconds(1);
    }

    IEnumerator FURY(){

        yield return new WaitForSeconds(10);
    }

    IEnumerator IDLE(){

        yield return new WaitForSeconds(_GM.SlimeIdleWaitTime);

        StayStill( 50 );
        
    }

    IEnumerator PATROL( ){

        yield return new WaitUntil( () => agent.remainingDistance <= 0.5 );
        agent.stoppingDistance = 0;
        StayStill( 30 );

        }

    IEnumerator Died(){

        isDie = true;
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

    IEnumerator AttackDelay(){

        yield return new WaitForSeconds(_GM.SlimeAttackDelay);
        isAttack = false;
    }

#endregion

}
