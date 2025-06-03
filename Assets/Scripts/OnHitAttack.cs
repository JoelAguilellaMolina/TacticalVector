using UnityEngine;
using UnityEngine.UI;

public class OnHitAttack : MonoBehaviour
{
    public GameObject gameManagerCombat;

    public GameObject character;
    public GameObject attack;
    public GameObject VidaJugador;
    public GameObject VidaE1;
    public GameObject Problemas;

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == character) gameManagerCombat.GetComponent<GameManagerCombat>().isMoving = false;
        else if(other.gameObject == attack) 
        {
            gameManagerCombat.GetComponent<GameManagerCombat>().isMoving = false;
            gameManagerCombat.GetComponent<GameManagerCombat>().onProblem = true;
            VidaJugador.SetActive(false);
            VidaE1.SetActive(false);
            Problemas.SetActive(true);
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        
    }
}
