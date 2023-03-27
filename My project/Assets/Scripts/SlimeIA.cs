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
    private int idWayPoint;

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


#region MeusMetodos
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
    
    void StateManager(){

        switch( state ){
            
            case enemyState.ALERT:

            break;
            case enemyState.EXPLORE:

            break;
            case enemyState.FOLLOW:

            break;
            case enemyState.FURY:

            destination = _GM.Player.position;
            agent.destination = destination;
            break;
        }
    }

    void ChangeState( enemyState newState ){

        //Para todas as corotinas
        StopAllCoroutines();
        state = newState;
        print(state);

        switch( state ){
            
            case enemyState.ALERT:
                StartCoroutine("ALERT");
            break;
            case enemyState.EXPLORE:
                StartCoroutine("EXPLORE");
            break;
            case enemyState.FOLLOW:
                StartCoroutine("FOLLOW");
            break;
            case enemyState.FURY:

                destination = _GM.Player.position;
                agent.stoppingDistance = _GM.SlimeDistanceToAttack;
                agent.destination = destination;

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

        yield return new WaitForSeconds(1);
    }

    IEnumerator EXPLORE(){

        yield return new WaitForSeconds(1);
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

        yield return new WaitUntil( () => agent.remainingDistance <= 0 );
        StayStill( 30 );

        }

    IEnumerator Died(){

        isDie = true;
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }


    void StayStill(int chance){

        if( randomize() < chance ){

            ChangeState(enemyState.IDLE);
        }
        else{

            ChangeState(enemyState.PATROL);
        }
    }
    int randomize(){

        int rand = Random.Range( 0, 100 );
        return rand; 
    }

#endregion

}
