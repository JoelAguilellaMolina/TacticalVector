using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActualizarPuntosMapa : MonoBehaviour
{
    public Transform Vectores;
    public Transform NivelesPuntos;
    public Transform DescripcionesNiveles;
    public GameObject TodoOscuro;
    public GameObject Trofeo;
    public bool[] nivelesCompletados;
    public bool[] nivelesActivos;
    public DataSave dataSave;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dataSave = GameObject.Find("DataSave").GetComponent<DataSave>();
        nivelesCompletados = dataSave.nivelesCompletados;
        int nivelesHechos = 0;
        for(int i = 0; i < nivelesCompletados.Length; i++)
        {
            
            if(nivelesCompletados[i] == true)
            {
                nivelesHechos++;
                NivelesPuntos.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color32(0,185,0,255);
                if(i == 0)
                {
                    nivelesActivos[1] = true;
                    nivelesActivos[2] = true;

                    //Activa Vector

                    Vectores.Find("AB").GetChild(0).gameObject.SetActive(true);
                    Vectores.Find("AB").GetChild(1).gameObject.SetActive(true);
                    Vectores.Find("AB").GetChild(2).gameObject.SetActive(false);

                    Vectores.Find("AC").GetChild(0).gameObject.SetActive(true);
                    Vectores.Find("AC").GetChild(1).gameObject.SetActive(true);
                    Vectores.Find("AC").GetChild(2).gameObject.SetActive(false);
                }
                else if(i == 1)
                {
                    nivelesActivos[8] = true;
                    nivelesActivos[3] = true;

                    Vectores.Find("BI").GetChild(0).gameObject.SetActive(true);
                    Vectores.Find("BI").GetChild(1).gameObject.SetActive(true);
                    Vectores.Find("BI").GetChild(2).gameObject.SetActive(false);

                    Vectores.Find("BD").GetChild(0).gameObject.SetActive(true);
                    Vectores.Find("BD").GetChild(1).gameObject.SetActive(true);
                    Vectores.Find("BD").GetChild(2).gameObject.SetActive(false);
                }
                else if(i == 2)
                {
                    nivelesActivos[3] = true;

                    Vectores.Find("CD").GetChild(0).gameObject.SetActive(true);
                    Vectores.Find("CD").GetChild(1).gameObject.SetActive(true);
                    Vectores.Find("CD").GetChild(2).gameObject.SetActive(false);
                }
                else if(i == 3)
                {
                    nivelesActivos[4] = true;
                    nivelesActivos[5] = true;

                    Vectores.Find("DE").GetChild(0).gameObject.SetActive(true);
                    Vectores.Find("DE").GetChild(1).gameObject.SetActive(true);
                    Vectores.Find("DE").GetChild(2).gameObject.SetActive(false);

                    Vectores.Find("DF").GetChild(0).gameObject.SetActive(true);
                    Vectores.Find("DF").GetChild(1).gameObject.SetActive(true);
                    Vectores.Find("DF").GetChild(2).gameObject.SetActive(false);
                }
                else if(i == 4)
                {
                    nivelesActivos[6] = true;

                    Vectores.Find("EG").GetChild(0).gameObject.SetActive(true);
                    Vectores.Find("EG").GetChild(1).gameObject.SetActive(true);
                    Vectores.Find("EG").GetChild(2).gameObject.SetActive(false);
                }
                else if(i == 5)
                {
                    nivelesActivos[6] = true;
                    nivelesActivos[10] = true;

                    Vectores.Find("FG").GetChild(0).gameObject.SetActive(true);
                    Vectores.Find("FG").GetChild(1).gameObject.SetActive(true);
                    Vectores.Find("FG").GetChild(2).gameObject.SetActive(false);

                    Vectores.Find("FK").GetChild(0).gameObject.SetActive(true);
                    Vectores.Find("FK").GetChild(1).gameObject.SetActive(true);
                    Vectores.Find("FK").GetChild(2).gameObject.SetActive(false);
                }
                else if(i == 6)
                {
                    nivelesActivos[7] = true;

                    Vectores.Find("GH").GetChild(0).gameObject.SetActive(true);
                    Vectores.Find("GH").GetChild(1).gameObject.SetActive(true);
                    Vectores.Find("GH").GetChild(2).gameObject.SetActive(false);
                }
                else if(i == 8)
                {
                    nivelesActivos[9] = true;

                    Vectores.Find("IJ").GetChild(0).gameObject.SetActive(true);
                    Vectores.Find("IJ").GetChild(1).gameObject.SetActive(true);
                    Vectores.Find("IJ").GetChild(2).gameObject.SetActive(false);
                }
                else if(i == 10)
                {
                    nivelesActivos[11] = true;

                    Vectores.Find("KL").GetChild(0).gameObject.SetActive(true);
                    Vectores.Find("KL").GetChild(1).gameObject.SetActive(true);
                    Vectores.Find("KL").GetChild(2).gameObject.SetActive(false);
                }
            }
        }

        if(nivelesHechos == nivelesCompletados.Length) Trofeo.SetActive(true);

        for(int i = 0; i < nivelesActivos.Length; i++)
        {
            if(nivelesActivos[i])
            {
                NivelesPuntos.GetChild(i).GetChild(0).GetComponent<Button>().enabled = true;
                NivelesPuntos.GetChild(i).GetComponent<Animator>().SetBool("isActive", true);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < DescripcionesNiveles.childCount; i++)
        {
            if(!DescripcionesNiveles.GetChild(i).GetChild(2).GetChild(0).GetChild(0).GetComponent<Button>().enabled)
            {
                SceneManager.LoadScene("Nivel" + (i+1).ToString());
            }
        }
    }
}
