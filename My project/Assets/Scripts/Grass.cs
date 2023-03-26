using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
   
   public ParticleSystem FxHit;
   private bool isCut = false;

   void GetHit( int amaunt ){

    if( !isCut ){

    transform.localScale = new Vector3( 1f, 1f, 1f );
    FxHit.Emit(10);
    isCut = true;
    }
   }
}
