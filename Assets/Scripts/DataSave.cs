using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataSave : MonoBehaviour
{
    public bool[] nivelesCompletados = new bool[12];
    public bool notPrimeraVez;
    public bool boostRadio;
    public bool boostVida;
    public bool tutorialBaraja;
    public List<Cards> CartasEnBaraja = new List<Cards>();
    public List<Cards> CartasNoEnBaraja = new List<Cards>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        

        DontDestroyOnLoad(this.gameObject);

        print(PlayerPrefs.GetInt("NotPrimeraVez"));
        if(PlayerPrefs.GetInt("NotPrimeraVez") == 2) notPrimeraVez = true;
        else if(PlayerPrefs.GetInt("NotPrimeraVez") == 1) notPrimeraVez = false;
        
        
        if(!notPrimeraVez) //Primeros Valores
        {
            notPrimeraVez = true;
            boostRadio = false;
            boostVida = false;
            tutorialBaraja = true;

            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_+0,5"));
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_+0,5"));
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_+x"));
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_+x"));
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_+7"));
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_+7"));
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_-4"));
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_-4"));
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_-2x"));
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_-2x"));
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_+1"));
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_+0,5"));
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_-4"));
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_-2x"));
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/Card_-1,25x"));

            CartasNoEnBaraja.Add(Resources.Load<Cards>("Cards/Card_x=x^2"));
            CartasNoEnBaraja.Add(Resources.Load<Cards>("Cards/Card_+7"));
            CartasNoEnBaraja.Add(Resources.Load<Cards>("Cards/Card_+7"));
            CartasNoEnBaraja.Add(Resources.Load<Cards>("Cards/Card_+x"));
            CartasNoEnBaraja.Add(Resources.Load<Cards>("Cards/Card_+x"));
            CartasNoEnBaraja.Add(Resources.Load<Cards>("Cards/Card_+1"));
            CartasNoEnBaraja.Add(Resources.Load<Cards>("Cards/Card_+1"));
            CartasNoEnBaraja.Add(Resources.Load<Cards>("Cards/Card_-4"));
            CartasNoEnBaraja.Add(Resources.Load<Cards>("Cards/Card_-4"));
            

        }

        else
        {

        
        
        if(PlayerPrefs.GetInt("BoostRadio") == 2) boostRadio = true;
        else if(PlayerPrefs.GetInt("BoostRadio") == 1) boostRadio = false;

        if(PlayerPrefs.GetInt("BoostVida") == 2) boostVida = true;
        else if(PlayerPrefs.GetInt("BoostVida") == 1) boostVida = false;

        if(PlayerPrefs.GetInt("TutorialBaraja") == 2) tutorialBaraja = true;
        else if(PlayerPrefs.GetInt("TutorialBaraja") == 1) tutorialBaraja = false;

        string tempKey = "";

        for(int i = 0; i < nivelesCompletados.Length; i++)
        {
            tempKey = "NivelCompletado" + i.ToString();
            if(PlayerPrefs.GetInt(tempKey) == 2) nivelesCompletados[i] = true;
            else if(PlayerPrefs.GetInt(tempKey) == 1) nivelesCompletados[i] = false;
        }

        tempKey = "CartasEnBaraja0";
        int j = 0;
        while(PlayerPrefs.GetString(tempKey) != "")
        {
            CartasEnBaraja.Add(Resources.Load<Cards>("Cards/" + PlayerPrefs.GetString(tempKey)));
            j++;
            tempKey = "CartasEnBaraja" + j.ToString();
        }

        tempKey = "CartasNoEnBaraja0";
        j = 0;
        while(PlayerPrefs.GetString(tempKey) != "")
        {
            CartasNoEnBaraja.Add(Resources.Load<Cards>("Cards/" + PlayerPrefs.GetString(tempKey)));
            j++;
            tempKey = "CartasNoEnBaraja" + j.ToString();
        }

        }
    
    }

    // Update is called once per frame
    void OnApplicationQuit()
    {
        while(CartasNoEnBaraja.Count < 4)
        {
            CartasNoEnBaraja.Add(CartasEnBaraja[CartasEnBaraja.Count - 1]);
            CartasEnBaraja.RemoveAt(CartasEnBaraja.Count - 1);
        }
        while(CartasEnBaraja.Count < 10)
        {
            CartasEnBaraja.Add(CartasNoEnBaraja[CartasNoEnBaraja.Count - 1]);
            CartasNoEnBaraja.RemoveAt(CartasNoEnBaraja.Count - 1);
        }
        PlayerPrefs.DeleteAll();

        
        if(CartasNoEnBaraja.Count == 9 && CartasEnBaraja.Count == 15) PlayerPrefs.SetInt("NotPrimeraVez",1);
        else PlayerPrefs.SetInt("NotPrimeraVez",2);
        print(PlayerPrefs.GetInt("NotPrimeraVez"));

        if(boostRadio) PlayerPrefs.SetInt("BoostRadio",2);
        else PlayerPrefs.SetInt("BoostRadio",1);

        if(boostVida) PlayerPrefs.SetInt("BoostVida",2);
        else PlayerPrefs.SetInt("BoostVida",1);

        if(tutorialBaraja) PlayerPrefs.SetInt("TutorialBaraja",2);
        else PlayerPrefs.SetInt("TutorialBaraja",1);

        string tempKey;
        for(int i = 0; i < nivelesCompletados.Length; i++)
        {
            tempKey = "NivelCompletado" + i.ToString();
            if(nivelesCompletados[i]) PlayerPrefs.SetInt(tempKey,2);
            else PlayerPrefs.SetInt(tempKey,1);
        }

        
        for(int i = 0; i < CartasEnBaraja.Count; i++)
        {
            tempKey = "CartasEnBaraja" + i.ToString();
            PlayerPrefs.SetString(tempKey,CartasEnBaraja[i].name);
        }
        for(int i = 0; i < CartasNoEnBaraja.Count; i++)
        {
            tempKey = "CartasNoEnBaraja" + i.ToString();
            PlayerPrefs.SetString(tempKey,CartasNoEnBaraja[i].name);
        }
    }
}


/*
if(VidaJugador <= 0)
        {
            if(!Finalizado.gameObject.activeSelf)
            {
                Finalizado.gameObject.SetActive(true);
                Finalizado.GetChild(3).gameObject.SetActive(true);
            }
            if(!Finalizado.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>().enabled)
            {
                SceneManager.LoadScene("Mapa");
            }
            
        }
        else if(VidaE1 <= 0 && VidaE2 <= 0 && VidaE3 <= 0 )
        {
            if(!Finalizado.gameObject.activeSelf)
            {
                Finalizado.gameObject.SetActive(true);
                Finalizado.GetChild(2).gameObject.SetActive(true);

                if(!isDropRango && !isDropVida)
                {

                
                int iDropRandom = Random.Range(0,DropsPosibles.Count);

                dataSave.CartasNoEnBaraja.Add(DropsPosibles[iDropRandom]);

                CartaConseguida.GetChild(1).GetComponent<RawImage>().texture = DropsPosibles[iDropRandom].sprite.texture;

                if(DropsPosibles[iDropRandom].type == "const") CartaConseguida.GetChild(0).GetComponent<RawImage>().color = new Color(1f, 0.827451f, 0.6784314f, 1f); //Color Para const
                else if(DropsPosibles[iDropRandom].type == "var") CartaConseguida.GetChild(0).GetComponent<RawImage>().color = new Color(0.6254902f, 0.8980392f, 0.8666667f, 1f); //Color Para var
                else if(DropsPosibles[iDropRandom].type == "change") CartaConseguida.GetChild(0).GetComponent<RawImage>().color = new Color(0.7764706f, 0.6392157f, 0.8588236f, 1f); //Color Para change
                }
            }
            if(!Finalizado.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>().enabled)
            {
                SceneManager.LoadScene("Mapa");
            }
            
        }
        */