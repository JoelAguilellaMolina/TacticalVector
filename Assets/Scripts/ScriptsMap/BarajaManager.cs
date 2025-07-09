using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BarajaManager : MonoBehaviour
{
    public Transform BarajaUsandoFondo;
    public Transform BarajaNoUsandoFondo;
    public Transform SlotsCartasUsando;
    public Transform SlotsCartasNoUsando;
    public List<Cards> CartasEnBaraja = new List<Cards>();
    public List<Cards> CartasNoEnBaraja = new List<Cards>();
    public int minCardVisibleBaraja;
    public int minCardVisibleNoBaraja;
    public int posibleCartaQuitarBaraja;
    public int posibleCartaQuitarNoBaraja;
    public Transform CartaUsandoTemp;
    public Transform CartaNoUsandoTemp;
    public Transform BotonMasUsable;
    public Transform BotonMenosUsable;
    public Transform CantidadCartas;
    public Transform CantidadCartasDisponibles;
    public Transform Volver;
    public GameObject TodoOscuro;
    public GameObject DataSave;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DataSave = GameObject.Find("DataSave");
        if(!DataSave.GetComponent<DataSave>().tutorialBaraja) BarajaUsandoFondo.parent.Find("TutorialBaraja").gameObject.SetActive(false);

        CartasEnBaraja = DataSave.GetComponent<DataSave>().CartasEnBaraja;
        CartasNoEnBaraja = DataSave.GetComponent<DataSave>().CartasNoEnBaraja;

        posibleCartaQuitarBaraja = -1;
        posibleCartaQuitarNoBaraja = -1;
        minCardVisibleBaraja = 0;
        minCardVisibleNoBaraja = 0;
        for(int i = 0; i < SlotsCartasUsando.childCount; i++)
        {
            CartaUsandoTemp = SlotsCartasUsando.GetChild(i);
            CartaNoUsandoTemp = SlotsCartasNoUsando.GetChild(i);
            CartaUsandoTemp.GetComponent<Image>().enabled = false;
            CartaNoUsandoTemp.GetComponent<Image>().enabled = false;

            
            CartaUsandoTemp.GetChild(2).GetComponent<Image>().sprite = CartasEnBaraja[i].sprite;
            CartaNoUsandoTemp.GetChild(2).GetComponent<Image>().sprite = CartasNoEnBaraja[i].sprite;

            CantidadCartas.GetComponent<TMP_Text>().text = CartasEnBaraja.Count.ToString() + " / 20";
            CantidadCartas.GetChild(0).GetComponent<TMP_Text>().text = CartasEnBaraja.Count.ToString() + " / 20";
            

            if(CartasEnBaraja[i].type == "const") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(1f, 0.827451f, 0.6784314f, 1f); //Color Para const
            else if(CartasEnBaraja[i].type == "var") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.6254902f, 0.8980392f, 0.8666667f, 1f); //Color Para var
            else if(CartasEnBaraja[i].type == "change") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.7764706f, 0.6392157f, 0.8588236f, 1f); //Color Para Change

            if(CartasNoEnBaraja[i].type == "const") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(1f, 0.827451f, 0.6784314f, 1f); //Color Para const
            else if(CartasNoEnBaraja[i].type == "var") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.6254902f, 0.8980392f, 0.8666667f, 1f); //Color Para var
            else if(CartasNoEnBaraja[i].type == "change") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.7764706f, 0.6392157f, 0.8588236f, 1f); //Color Para Change

        }
        
        CantidadCartas.GetComponent<TMP_Text>().text = CartasEnBaraja.Count.ToString() + " / 20";
        CantidadCartas.GetChild(0).GetComponent<TMP_Text>().text = CartasEnBaraja.Count.ToString() + " / 20";
        
        CantidadCartasDisponibles.GetComponent<TMP_Text>().text = CartasNoEnBaraja.Count.ToString();
        CantidadCartasDisponibles.GetChild(0).GetComponent<TMP_Text>().text = CartasNoEnBaraja.Count.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        if(BarajaUsandoFondo.parent.Find("TutorialBaraja").gameObject.activeSelf == false) DataSave.GetComponent<DataSave>().tutorialBaraja = false;
        // Revisar Flechas

        //IzquierdaUsando
        if(BarajaUsandoFondo.Find("FlechaIzq").GetComponent<Button>().enabled == false)
        {
            BarajaUsandoFondo.Find("FlechaIzq").GetComponent<Button>().enabled = true;
            if(minCardVisibleBaraja > 0)
            {
                minCardVisibleBaraja--;
                for(int i = 0; i < SlotsCartasUsando.childCount; i++)
                {
                    CartaUsandoTemp = SlotsCartasUsando.GetChild(i);
                    CartaUsandoTemp.GetChild(2).GetComponent<Image>().sprite = CartasEnBaraja[minCardVisibleBaraja + i].sprite;

                    if(CartasEnBaraja[minCardVisibleBaraja + i].type == "const") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(1f, 0.827451f, 0.6784314f, 1f); //Color Para const
                    else if(CartasEnBaraja[minCardVisibleBaraja + i].type == "var") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.6254902f, 0.8980392f, 0.8666667f, 1f); //Color Para var
                    else if(CartasEnBaraja[minCardVisibleBaraja + i].type == "change") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.7764706f, 0.6392157f, 0.8588236f, 1f); //Color Para Change
                }
            }
            
        }
        //Izquierda No
        if(BarajaNoUsandoFondo.Find("FlechaIzq").GetComponent<Button>().enabled == false)
        {
            BarajaNoUsandoFondo.Find("FlechaIzq").GetComponent<Button>().enabled = true;
            if(minCardVisibleNoBaraja > 0)
            {
                minCardVisibleNoBaraja--;
                for(int i = 0; i < SlotsCartasNoUsando.childCount; i++)
                {
                    CartaNoUsandoTemp = SlotsCartasNoUsando.GetChild(i);
                    CartaNoUsandoTemp.GetChild(2).GetComponent<Image>().sprite = CartasNoEnBaraja[minCardVisibleNoBaraja + i].sprite;

                    if(CartasNoEnBaraja[minCardVisibleNoBaraja + i].type == "const") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(1f, 0.827451f, 0.6784314f, 1f); //Color Para const
                    else if(CartasNoEnBaraja[minCardVisibleNoBaraja + i].type == "var") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.6254902f, 0.8980392f, 0.8666667f, 1f); //Color Para var
                    else if(CartasNoEnBaraja[minCardVisibleNoBaraja + i].type == "change") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.7764706f, 0.6392157f, 0.8588236f, 1f); //Color Para Change
                }
            }
            
        }
        //Der
        if(BarajaUsandoFondo.Find("FlechaDer").GetComponent<Button>().enabled == false)
        {
            BarajaUsandoFondo.Find("FlechaDer").GetComponent<Button>().enabled = true;
            if(minCardVisibleBaraja + 4 < CartasEnBaraja.Count)
            {
                minCardVisibleBaraja++;
                for(int i = 0; i < SlotsCartasUsando.childCount; i++)
                {
                    CartaUsandoTemp = SlotsCartasUsando.GetChild(i);
                    CartaUsandoTemp.GetChild(2).GetComponent<Image>().sprite = CartasEnBaraja[minCardVisibleBaraja + i].sprite;

                    if(CartasEnBaraja[minCardVisibleBaraja + i].type == "const") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(1f, 0.827451f, 0.6784314f, 1f); //Color Para const
                    else if(CartasEnBaraja[minCardVisibleBaraja + i].type == "var") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.6254902f, 0.8980392f, 0.8666667f, 1f); //Color Para var
                    else if(CartasEnBaraja[minCardVisibleBaraja + i].type == "change") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.7764706f, 0.6392157f, 0.8588236f, 1f); //Color Para Change
                }
            }
            
        }
        //Der No
        if(BarajaNoUsandoFondo.Find("FlechaDer").GetComponent<Button>().enabled == false)
        {
            BarajaNoUsandoFondo.Find("FlechaDer").GetComponent<Button>().enabled = true;
            if(minCardVisibleNoBaraja + 4 < CartasNoEnBaraja.Count)
            {
                minCardVisibleNoBaraja++;
                for(int i = 0; i < SlotsCartasNoUsando.childCount; i++)
                {
                    CartaNoUsandoTemp = SlotsCartasNoUsando.GetChild(i);
                    CartaNoUsandoTemp.GetChild(2).GetComponent<Image>().sprite = CartasNoEnBaraja[minCardVisibleNoBaraja + i].sprite;

                    if(CartasNoEnBaraja[minCardVisibleNoBaraja + i].type == "const") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(1f, 0.827451f, 0.6784314f, 1f); //Color Para const
                    else if(CartasNoEnBaraja[minCardVisibleNoBaraja + i].type == "var") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.6254902f, 0.8980392f, 0.8666667f, 1f); //Color Para var
                    else if(CartasNoEnBaraja[minCardVisibleNoBaraja + i].type == "change") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.7764706f, 0.6392157f, 0.8588236f, 1f); //Color Para Change
                }
            }
            
        }

        //Selección de cartas
        for(int i = 0; i < SlotsCartasNoUsando.childCount; i++)
            {
                CartaUsandoTemp = SlotsCartasUsando.GetChild(i);
                CartaNoUsandoTemp = SlotsCartasNoUsando.GetChild(i);

                if(CartaUsandoTemp.GetComponent<Button>().enabled == false)
                {
                    CartaUsandoTemp.GetComponent<Button>().enabled = true;
                    if(CartaUsandoTemp.GetChild(0).GetComponent<Image>().color == Color.black && CartaUsandoTemp.GetChild(3).gameObject.activeSelf == false) 
                    {
                        CartaUsandoTemp.GetChild(0).GetComponent<Image>().color = Color.green;
                        posibleCartaQuitarBaraja = minCardVisibleBaraja + i; // Guarda índice para modificar la lista

                        BotonMenosUsable.GetComponent<Image>().enabled = true;

                        BarajaUsandoFondo.Find("FlechaDer").gameObject.SetActive(false);
                        BarajaNoUsandoFondo.Find("FlechaDer").gameObject.SetActive(false);
                        BarajaUsandoFondo.Find("FlechaIzq").gameObject.SetActive(false);
                        BarajaNoUsandoFondo.Find("FlechaIzq").gameObject.SetActive(false);
                        Volver.gameObject.SetActive(false);

                        for(int j = 0; j < SlotsCartasUsando.childCount; j++)
                        {
                            if(SlotsCartasUsando.GetChild(j) != CartaUsandoTemp) SlotsCartasUsando.GetChild(j).GetChild(3).gameObject.SetActive(true);
                            SlotsCartasNoUsando.GetChild(j).GetChild(3).gameObject.SetActive(true);
                            
                        }
                    }
                    else if(CartaUsandoTemp.GetChild(0).GetComponent<Image>().color == Color.green && CartaUsandoTemp.GetChild(3).gameObject.activeSelf == false)
                    {
                        CartaUsandoTemp.GetChild(0).GetComponent<Image>().color = Color.black;
                        posibleCartaQuitarBaraja = -1; // Guarda índice para modificar la lista

                        BarajaUsandoFondo.Find("FlechaDer").gameObject.SetActive(true);
                        BarajaNoUsandoFondo.Find("FlechaDer").gameObject.SetActive(true);
                        BarajaUsandoFondo.Find("FlechaIzq").gameObject.SetActive(true);
                        BarajaNoUsandoFondo.Find("FlechaIzq").gameObject.SetActive(true);
                        Volver.gameObject.SetActive(true);

                        BotonMenosUsable.GetComponent<Image>().enabled = false;
                        for(int j = 0; j < SlotsCartasUsando.childCount; j++)
                        {
                            SlotsCartasUsando.GetChild(j).GetChild(3).gameObject.SetActive(false);
                            SlotsCartasNoUsando.GetChild(j).GetChild(3).gameObject.SetActive(false);
                        }
                    }
                }
                else if(CartaNoUsandoTemp.GetComponent<Button>().enabled == false)
                {
                    CartaNoUsandoTemp.GetComponent<Button>().enabled = true;

                    
                    if(CartaNoUsandoTemp.GetChild(0).GetComponent<Image>().color == Color.black && CartaNoUsandoTemp.GetChild(3).gameObject.activeSelf == false)
                    {
                        CartaNoUsandoTemp.GetChild(0).GetComponent<Image>().color = Color.green;
                        posibleCartaQuitarNoBaraja = minCardVisibleNoBaraja + i; // Guarda índice para modificar la lista 

                        BotonMasUsable.GetComponent<Image>().enabled = true;

                        BarajaUsandoFondo.Find("FlechaDer").gameObject.SetActive(false);
                        BarajaNoUsandoFondo.Find("FlechaDer").gameObject.SetActive(false);
                        BarajaUsandoFondo.Find("FlechaIzq").gameObject.SetActive(false);
                        BarajaNoUsandoFondo.Find("FlechaIzq").gameObject.SetActive(false);
                        Volver.gameObject.SetActive(false);

                        for(int j = 0; j < SlotsCartasNoUsando.childCount; j++)
                        {
                            if(SlotsCartasNoUsando.GetChild(j) != CartaNoUsandoTemp) SlotsCartasNoUsando.GetChild(j).GetChild(3).gameObject.SetActive(true);
                            SlotsCartasUsando.GetChild(j).GetChild(3).gameObject.SetActive(true);
                        }
                    }
                    else if(CartaNoUsandoTemp.GetChild(0).GetComponent<Image>().color == Color.green && CartaNoUsandoTemp.GetChild(3).gameObject.activeSelf == false)
                    {
                        CartaNoUsandoTemp.GetChild(0).GetComponent<Image>().color = Color.black;
                        posibleCartaQuitarNoBaraja = -1; 

                        BotonMasUsable.GetComponent<Image>().enabled = false;

                        BarajaUsandoFondo.Find("FlechaDer").gameObject.SetActive(true);
                        BarajaNoUsandoFondo.Find("FlechaDer").gameObject.SetActive(true);
                        BarajaUsandoFondo.Find("FlechaIzq").gameObject.SetActive(true);
                        BarajaNoUsandoFondo.Find("FlechaIzq").gameObject.SetActive(true);
                        Volver.gameObject.SetActive(true);

                        for(int j = 0; j < SlotsCartasNoUsando.childCount; j++)
                        {
                            SlotsCartasNoUsando.GetChild(j).GetChild(3).gameObject.SetActive(false);
                            SlotsCartasUsando.GetChild(j).GetChild(3).gameObject.SetActive(false);
                        }
                    }
                }

            }

            //Boton mas
        if(BotonMasUsable.GetComponent<Button>().enabled == false)
        {
            BotonMasUsable.GetComponent<Button>().enabled = true;
            if(BotonMasUsable.GetComponent<Image>().enabled == true && CartasEnBaraja.Count < 20 && posibleCartaQuitarNoBaraja != -1)
            {

                if(CartasEnBaraja.Count < 4)
                {
                    CartaUsandoTemp = SlotsCartasUsando.GetChild(CartasEnBaraja.Count);
                    CartaUsandoTemp.GetComponent<Image>().enabled = false;
                    CartaUsandoTemp.GetChild(0).gameObject.SetActive(true);
                    CartaUsandoTemp.GetChild(1).gameObject.SetActive(true);
                    CartaUsandoTemp.GetChild(2).gameObject.SetActive(true);
                    CartaUsandoTemp.GetChild(3).gameObject.SetActive(false);
                    CartaUsandoTemp.GetChild(3).GetComponent<Image>().enabled = true;
                }
                

                CartasEnBaraja.Add(CartasNoEnBaraja[posibleCartaQuitarNoBaraja]);

                CartasNoEnBaraja.RemoveAt(posibleCartaQuitarNoBaraja);
                if(minCardVisibleNoBaraja > 0) minCardVisibleNoBaraja--;

                for(int i = 0; i < SlotsCartasNoUsando.childCount; i++)
                {
                    SlotsCartasNoUsando.GetChild(i).GetChild(3).gameObject.SetActive(false);
                    SlotsCartasUsando.GetChild(i).GetChild(3).gameObject.SetActive(false);
                    CartaNoUsandoTemp = SlotsCartasNoUsando.GetChild(i);

                    if(i > CartasNoEnBaraja.Count - 1)
                    {
                    CartaNoUsandoTemp.GetComponent<Image>().enabled = true;
                    CartaNoUsandoTemp.GetChild(0).gameObject.SetActive(false);
                    CartaNoUsandoTemp.GetChild(1).gameObject.SetActive(false);
                    CartaNoUsandoTemp.GetChild(2).gameObject.SetActive(false);
                    CartaNoUsandoTemp.GetChild(3).gameObject.SetActive(true);
                    CartaNoUsandoTemp.GetChild(3).GetComponent<Image>().enabled = false;
                    }

                    else
                    {
                    CartaNoUsandoTemp.GetChild(2).GetComponent<Image>().sprite = CartasNoEnBaraja[minCardVisibleNoBaraja + i].sprite;

                    if(CartasNoEnBaraja[minCardVisibleNoBaraja + i].type == "const") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(1f, 0.827451f, 0.6784314f, 1f); //Color Para const
                    else if(CartasNoEnBaraja[minCardVisibleNoBaraja + i].type == "var") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.6254902f, 0.8980392f, 0.8666667f, 1f); //Color Para var
                    else if(CartasNoEnBaraja[minCardVisibleNoBaraja + i].type == "change") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.7764706f, 0.6392157f, 0.8588236f, 1f); //Color Para Change
                    }

                    




                    if(SlotsCartasNoUsando.GetChild(i).GetChild(0).GetComponent<Image>().color == Color.green)
                    {
                        CartaNoUsandoTemp.GetChild(0).GetComponent<Image>().color = Color.black;
                    }
                }

                if(CartasEnBaraja.Count <= 4)
                {
                    CartaUsandoTemp = SlotsCartasUsando.GetChild(CartasEnBaraja.Count - 1);
                    CartaUsandoTemp.GetChild(2).GetComponent<Image>().sprite = CartasEnBaraja[CartasEnBaraja.Count - 1].sprite;

                    if(CartasEnBaraja[CartasEnBaraja.Count - 1].type == "const") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(1f, 0.827451f, 0.6784314f, 1f); //Color Para const
                    else if(CartasEnBaraja[CartasEnBaraja.Count - 1].type == "var") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.6254902f, 0.8980392f, 0.8666667f, 1f); //Color Para var
                    else if(CartasEnBaraja[CartasEnBaraja.Count - 1].type == "change") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.7764706f, 0.6392157f, 0.8588236f, 1f); //Color Para Change
                }
                
                posibleCartaQuitarNoBaraja = -1;

                CantidadCartas.GetComponent<TMP_Text>().text = CartasEnBaraja.Count.ToString() + " / 20";
                CantidadCartas.GetChild(0).GetComponent<TMP_Text>().text = CartasEnBaraja.Count.ToString() + " / 20";
                
                CantidadCartasDisponibles.GetComponent<TMP_Text>().text = CartasNoEnBaraja.Count.ToString();
                CantidadCartasDisponibles.GetChild(0).GetComponent<TMP_Text>().text = CartasNoEnBaraja.Count.ToString();

                BotonMenosUsable.GetComponent<Image>().enabled = false;
                BotonMasUsable.GetComponent<Image>().enabled = false;

                BarajaUsandoFondo.Find("FlechaDer").gameObject.SetActive(true);
                BarajaNoUsandoFondo.Find("FlechaDer").gameObject.SetActive(true);
                BarajaUsandoFondo.Find("FlechaIzq").gameObject.SetActive(true);
                BarajaNoUsandoFondo.Find("FlechaIzq").gameObject.SetActive(true);
                Volver.gameObject.SetActive(true);
            }

        }

           //Boton menos
        if(BotonMenosUsable.GetComponent<Button>().enabled == false)
        {
            BotonMenosUsable.GetComponent<Button>().enabled = true;
            if(BotonMenosUsable.GetComponent<Image>().enabled == true && posibleCartaQuitarBaraja != -1)
            {
                if(CartasNoEnBaraja.Count < 4)
                {
                    CartaNoUsandoTemp = SlotsCartasNoUsando.GetChild(CartasNoEnBaraja.Count);
                    CartaNoUsandoTemp.GetComponent<Image>().enabled = false;
                    CartaNoUsandoTemp.GetChild(0).gameObject.SetActive(true);
                    CartaNoUsandoTemp.GetChild(1).gameObject.SetActive(true);
                    CartaNoUsandoTemp.GetChild(2).gameObject.SetActive(true);
                    CartaNoUsandoTemp.GetChild(3).gameObject.SetActive(false);
                    CartaNoUsandoTemp.GetChild(3).GetComponent<Image>().enabled = true;
                }
                CartasNoEnBaraja.Add(CartasEnBaraja[posibleCartaQuitarBaraja]);
                CartasEnBaraja.RemoveAt(posibleCartaQuitarBaraja);
                if(minCardVisibleBaraja > 0) minCardVisibleBaraja--;

                for(int i = 0; i < SlotsCartasUsando.childCount; i++)
                {
                    SlotsCartasUsando.GetChild(i).GetChild(3).gameObject.SetActive(false);
                    SlotsCartasNoUsando.GetChild(i).GetChild(3).gameObject.SetActive(false);
                    CartaUsandoTemp = SlotsCartasUsando.GetChild(i);

                    if(i > CartasEnBaraja.Count - 1)
                    {
                    CartaUsandoTemp.GetComponent<Image>().enabled = true;
                    CartaUsandoTemp.GetChild(0).gameObject.SetActive(false);
                    CartaUsandoTemp.GetChild(1).gameObject.SetActive(false);
                    CartaUsandoTemp.GetChild(2).gameObject.SetActive(false);
                    CartaUsandoTemp.GetChild(3).gameObject.SetActive(true);
                    CartaUsandoTemp.GetChild(3).GetComponent<Image>().enabled = false;
                    }

                    else
                    {
                    CartaUsandoTemp.GetChild(2).GetComponent<Image>().sprite = CartasEnBaraja[minCardVisibleBaraja + i].sprite;

                    if(CartasEnBaraja[minCardVisibleBaraja + i].type == "const") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(1f, 0.827451f, 0.6784314f, 1f); //Color Para const
                    else if(CartasEnBaraja[minCardVisibleBaraja + i].type == "var") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.6254902f, 0.8980392f, 0.8666667f, 1f); //Color Para var
                    else if(CartasEnBaraja[minCardVisibleBaraja + i].type == "change") CartaUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.7764706f, 0.6392157f, 0.8588236f, 1f); //Color Para Change
                    }



                    if(SlotsCartasUsando.GetChild(i).GetChild(0).GetComponent<Image>().color == Color.green)
                    {
                        CartaUsandoTemp.GetChild(0).GetComponent<Image>().color = Color.black;
                    }
                }

                if(CartasNoEnBaraja.Count <= 4)
                {
                    CartaNoUsandoTemp = SlotsCartasNoUsando.GetChild(CartasNoEnBaraja.Count - 1);
                    CartaNoUsandoTemp.GetChild(2).GetComponent<Image>().sprite = CartasNoEnBaraja[CartasNoEnBaraja.Count - 1].sprite;

                    if(CartasNoEnBaraja[CartasNoEnBaraja.Count - 1].type == "const") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(1f, 0.827451f, 0.6784314f, 1f); //Color Para const
                    else if(CartasNoEnBaraja[CartasNoEnBaraja.Count - 1].type == "var") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.6254902f, 0.8980392f, 0.8666667f, 1f); //Color Para var
                    else if(CartasNoEnBaraja[CartasNoEnBaraja.Count - 1].type == "change") CartaNoUsandoTemp.GetChild(1).GetComponent<Image>().color = new Color(0.7764706f, 0.6392157f, 0.8588236f, 1f); //Color Para Change
                }
                
                posibleCartaQuitarBaraja = -1;

                CantidadCartas.GetComponent<TMP_Text>().text = CartasEnBaraja.Count.ToString() + " / 20";
                CantidadCartas.GetChild(0).GetComponent<TMP_Text>().text = CartasEnBaraja.Count.ToString() + " / 20";
                CantidadCartasDisponibles.GetComponent<TMP_Text>().text = CartasNoEnBaraja.Count.ToString();
                CantidadCartasDisponibles.GetChild(0).GetComponent<TMP_Text>().text = CartasNoEnBaraja.Count.ToString();

                BotonMenosUsable.GetComponent<Image>().enabled = false;
                BotonMasUsable.GetComponent<Image>().enabled = false;

                BarajaUsandoFondo.Find("FlechaDer").gameObject.SetActive(true);
                BarajaNoUsandoFondo.Find("FlechaDer").gameObject.SetActive(true);
                BarajaUsandoFondo.Find("FlechaIzq").gameObject.SetActive(true);
                BarajaNoUsandoFondo.Find("FlechaIzq").gameObject.SetActive(true);
                Volver.gameObject.SetActive(true);
            }

        }
        if(Volver.GetComponent<Button>().enabled == false)
        {
            Volver.GetComponent<Button>().enabled = true;
            if(CartasEnBaraja.Count < 10)
            {
                Volver.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                Volver.GetChild(0).gameObject.SetActive(false);
                TodoOscuro.SetActive(false);
                BarajaUsandoFondo.parent.gameObject.SetActive(false);
            }
        }


    }
}
