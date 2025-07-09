using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Opciones : MonoBehaviour
{
    public GameObject OpcionesObject;
    public Transform OpcionesButton;
    public Transform IrAlMapa;
    public Transform IrAlTítulo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        OpcionesButton = GameObject.Find("OpcionesButton").transform;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OpcionesButton = GameObject.Find("OpcionesButton").transform;
        if(SceneManager.GetActiveScene().name != "Mapa") IrAlMapa.GetChild(0).GetComponent<Image>().color = new Color32(0,147,0,255);
        else IrAlMapa.GetChild(0).GetComponent<Image>().color = new Color32(147,147,147,255);
        if(SceneManager.GetActiveScene().name != "TitleScene") IrAlTítulo.GetChild(0).GetComponent<Image>().color = new Color32(0,147,0,255);
        else IrAlTítulo.GetChild(0).GetComponent<Image>().color = new Color32(147,147,147,255);
    }
    void Update()
    {
        if(OpcionesButton == null) OpcionesButton = GameObject.Find("OpcionesButton").transform;
        else
        {
            if(!OpcionesButton.GetComponent<Button>().enabled)
            {
                OpcionesButton.GetComponent<Button>().enabled = true;
                OpcionesObject.SetActive(true);
            }
        }
        if(!IrAlMapa.GetChild(0).GetComponent<Button>().enabled)
        {
            IrAlMapa.GetChild(0).GetComponent<Button>().enabled = true;
            if(SceneManager.GetActiveScene().name != "Mapa")
            {
                SceneManager.LoadScene("Mapa");
                OpcionesObject.SetActive(false);
            }
            
        }
        if(!IrAlTítulo.GetChild(0).GetComponent<Button>().enabled)
        {
            IrAlTítulo.GetChild(0).GetComponent<Button>().enabled = true;
            if(SceneManager.GetActiveScene().name != "TitleScene")
            {
                SceneManager.LoadScene("TitleScene");
                OpcionesObject.SetActive(false);
            }
            
        }
    }
}
