using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeIA : MonoBehaviour
{
    private Animator anim;
    private GameManager _GM;
    private bool isDie;
    private NavMeshAgent agent;
    private Vector3 destination;
    private int idWayPoint;

    public int HP = 3;
    public enemyState state;

    //Constantes de tempo da corotina
    public const float IdleWaitTime = 3f;
    public const float PatrolWaitTime = 5f;
    

    void Start(){

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        _GM = FindObjectOfType(typeof(GameManager)) as GameManager;

        ChangeState(state);
    }


#region MeusMetodos
    void GetHit( int amount){

        if(isDie) return;

        HP -= amount;
        
        if( HP > 0){

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

            break;
            case enemyState.IDLE:

            break;
            case enemyState.PATROL:

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
                StartCoroutine("FURY");
            break;
            case enemyState.IDLE:

                destination = transform.position;
                agent.destination = destination;

                StartCoroutine("IDLE");
            break;
            case enemyState.PATROL:

                idWayPoint = Random.Range( 0, _GM.slimeWayPoints.Length );
                destination = _GM.slimeWayPoints[idWayPoint].position;
                agent.destination = destination;

                StartCoroutine("PATROL");
            break;
        }
    }

    IEnumerator ALERT(){

        yield return new WaitForSeconds(IdleWaitTime);
    }

    IEnumerator EXPLORE(){

        yield return new WaitForSeconds(IdleWaitTime);
    }

    IEnumerator FOLLOW(){

        yield return new WaitForSeconds(IdleWaitTime);
    }

    IEnumerator FURY(){

        yield return new WaitForSeconds(IdleWaitTime);
    }

    IEnumerator IDLE(){

        yield return new WaitForSeconds(IdleWaitTime);

        StayStill( 50 );
        
    }

    IEnumerator PATROL( ){

        yield return new WaitForSeconds(PatrolWaitTime);
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
