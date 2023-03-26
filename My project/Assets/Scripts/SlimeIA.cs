using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIA : MonoBehaviour
{
    private Animator anim;
    private bool isDie;
    public int HP = 3;

    void Start(){

        anim = GetComponent<Animator>();
    }


    IEnumerator Died(){

        isDie = true;
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
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

#endregion

}
