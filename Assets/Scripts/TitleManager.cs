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
    public Button Salir;
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
        if(Salir.enabled == false) 
        {
        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
        }

        //else if(Jugar.enabled == false && !DataSave.nivelesCompletados[0]) 
    }
}
