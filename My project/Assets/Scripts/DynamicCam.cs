using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCam : MonoBehaviour
{
    [Header("Camera")]
    public GameObject CamB;

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
