using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;

    //Header serve para aparecer o titulo como caixinha na unity
    [Header("Player Config")]
    public float MovementSpeed = 3f;

    void Start(){

        controller = GetComponent<CharacterController>();
    }
    void Update(){
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3( horizontal, 0, vertical ).normalized;

        controller.Move( direction * MovementSpeed * Time.deltaTime );
    }
}
