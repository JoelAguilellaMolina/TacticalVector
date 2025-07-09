using UnityEngine;
using UnityEngine.UI;

public class OnHitAttack : MonoBehaviour
{
    public GameObject gameManagerCombat;

    public GameObject character;
    public GameObject character2;
    public GameObject character3;
    public GameObject attack;
    public GameObject attack2;
    public GameObject attack3;
    public GameObject VidaJugador;
    public GameObject VidaE1;
    public GameObject VidaE2;
    public GameObject VidaE3;
    public GameObject Problemas;

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == character || other.gameObject == character2 || other.gameObject == character3) gameManagerCombat.GetComponent<GameManagerCombat>().isMoving = false;
        else if(other.gameObject == attack || other.gameObject == attack2 || other.gameObject == attack3) 
        {
            GameManagerCombat gm = gameManagerCombat.GetComponent<GameManagerCombat>();
            gm.isMoving = false;
            gm.onProblem = true;
            VidaJugador.SetActive(false);
            VidaE1.SetActive(false);
            VidaE2.SetActive(false);
            VidaE3.SetActive(false);
            Problemas.SetActive(true);

            if(!gm.isEnemyTurn)
            {
                if(this.name == "SimpleCharacter (1)") gm.enemyHit = 1;
                else if(this.name == "SimpleCharacter (2)") gm.enemyHit = 2;
                else if(this.name == "SimpleCharacter (3)") gm.enemyHit = 3;
            }
            else
            {
                if(other.gameObject.name == "AtaqueE1") gm.enemyHit = 1;
                else if(other.gameObject.name == "AtaqueE2") gm.enemyHit = 2;
                else if(other.gameObject.name == "AtaqueE3") gm.enemyHit = 3;
            }
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        
    }
}
