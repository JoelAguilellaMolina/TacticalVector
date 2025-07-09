using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject TodoOscuro;
    public Button Jugar;
    public Button Créditos;
    public DataSave DataSave;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Jugar.enabled == false && DataSave.nivelesCompletados[0])
        {
            SceneManager.LoadScene("Mapa");
        }
        else if(Jugar.enabled == false && !DataSave.nivelesCompletados[0]) SceneManager.LoadScene("Nivel1");
        //else if(Jugar.enabled == false && !DataSave.nivelesCompletados[0]) 
    }
}
