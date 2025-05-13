using UnityEngine;
using System.Collections;

public class OnHitColliderRadius : MonoBehaviour
{
    public GameObject gameManagerCombat;

    public GameObject character;
    public GameObject posibilityLeft;
    public GameObject posibilityRight;

    void Start()
    {

    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == character) gameManagerCombat.GetComponent<GameManagerCombat>().isMoving = false;
        else if(other.gameObject == posibilityLeft) gameManagerCombat.GetComponent<GameManagerCombat>().isMovingA = false;
        else if(other.gameObject == posibilityRight) gameManagerCombat.GetComponent<GameManagerCombat>().isMovingB = false;
    }

    void OnTriggerStay(Collider other)
    {
        
    }


}