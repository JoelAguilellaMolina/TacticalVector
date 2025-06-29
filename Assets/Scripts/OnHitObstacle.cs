using UnityEngine;
using System.Collections;

public class OnHitObstacle : MonoBehaviour
{
    public GameObject gameManagerCombat;

    public GameObject character;
    public GameObject attack;
    public GameObject posibilityLeft;
    public GameObject posibilityRight;

    public GameObject character1;
    public GameObject attack1;
    public GameObject posibilityLeft1;
    public GameObject posibilityRight1;
    private const float pushDifference = -500000f;
    private const float secsToWait = 0.1f;
    public const float VELOCIDAD = 0.02f;


    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        

        if(other.gameObject == character ||other.gameObject == character1) 
        {
            if(gameManagerCombat.GetComponent<GameManagerCombat>().isMoveState)
            {
                gameManagerCombat.GetComponent<GameManagerCombat>().isMoving = false;
                StartCoroutine(Wait(secsToWait, other));
            }
        }
        else if(other.gameObject == attack || other.gameObject == attack1) 
        {
            gameManagerCombat.GetComponent<GameManagerCombat>().isMoving = false;
        }
        
        else if(other.gameObject == posibilityLeft || other.gameObject == posibilityLeft1 ) 
        {
            gameManagerCombat.GetComponent<GameManagerCombat>().isMovingA = false;
        }
        else if(other.gameObject == posibilityRight || other.gameObject == posibilityRight1) gameManagerCombat.GetComponent<GameManagerCombat>().isMovingB = false;
    }

    void OnTriggerStay(Collider other)
    {

        if(other.gameObject == character ||other.gameObject == character1) 
        {
            if(gameManagerCombat.GetComponent<GameManagerCombat>().isMoveState)
            {
                gameManagerCombat.GetComponent<GameManagerCombat>().isMoving = false;
                StartCoroutine(Wait(secsToWait, other));
            }
            
            
        }
        else if(other.gameObject == attack || other.gameObject == attack1) 
        {
            gameManagerCombat.GetComponent<GameManagerCombat>().isMoving = false;
            
        }
        else if(other.gameObject == posibilityLeft || other.gameObject == posibilityLeft1 ) 
        {
            gameManagerCombat.GetComponent<GameManagerCombat>().isMovingA = false;
        }
        else if(other.gameObject == posibilityRight || other.gameObject == posibilityRight1) gameManagerCombat.GetComponent<GameManagerCombat>().isMovingB = false;
    }

    void OnTriggerExit(Collider other)
    {

        gameManagerCombat.GetComponent<GameManagerCombat>().aplicarMovimiento = true;
        gameManagerCombat.GetComponent<GameManagerCombat>().isMoving = false;
    }

    IEnumerator Wait(float n, Collider other)
    {
        GameManagerCombat gm = gameManagerCombat.GetComponent<GameManagerCombat>();
        Vector3 force = transform.position - other.transform.position;
        
            // normalize force vector to get direction only and trim magnitude
        force.Normalize();
        print(force.z);
        if(force.z > 0 && gm.n > 3)
        {
            /*
            if(other.gameObject == character) gm.avatarPosition = gm.temporalPosition;
            else if(other.gameObject == character1) gm.enemigoPosition = gm.temporalPositionEnemigo1;
            gm.isMoving = false;
            gm.aplicarMovimiento = true;
            */
            
        }
        other.gameObject.transform.GetComponent<Rigidbody>().AddForce(force * pushDifference);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(n);

        
        other.gameObject.transform.GetComponent<Rigidbody>().AddForce(- force * pushDifference);

        if(other.gameObject == character)
        {
            gm.posibilityLeftPos.position = gm.avatarPosition.position;
            gm.posibilityRightPos.position = gm.avatarPosition.position;
            gm.RadioGeneral.position = new Vector3(gm.avatarPosition.position.x, VELOCIDAD ,gm.avatarPosition.position.z);
            gm.temporalPosition.position = gm.avatarPosition.position;
            gm.ataquePosition.position =  new Vector3(gm.avatarPosition.position.x, -1 ,gm.avatarPosition.position.z);
        }

        if(other.gameObject == character1)
        {
            gm.posibilityLeftPosEnemigo1.position = gm.enemigoPosition.position;
            gm.posibilityRightPosEnemigo1.position = gm.enemigoPosition.position;
            gm.RadioGeneralEnemigo1.position = new Vector3(gm.enemigoPosition.position.x, VELOCIDAD ,gm.enemigoPosition.position.z);
            gm.temporalPositionEnemigo1.position = gm.enemigoPosition.position;
            gm.ataqueEnemigoPosition.position =  new Vector3(gm.enemigoPosition.position.x, -1 ,gm.enemigoPosition.position.z);
        }

    }
}
