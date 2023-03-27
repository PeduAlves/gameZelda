using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyState{

    IDLE, ALERT, EXPLORE, PATROL, FOLLOW, FURY

}

public class GameManager : MonoBehaviour
{
    
    [Header("Slime IA")]
    public Transform[] slimeWayPoints;

}
