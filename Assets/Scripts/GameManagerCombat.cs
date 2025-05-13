using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManagerCombat : MonoBehaviour
{
    public Transform avatarPosition;

    public Transform posibilityLeftPos;
    public Transform posibilityRightPos;
    public Transform RadioGeneral;
    public Transform temporalPosition;

    public bool isMoving;
    public bool isMovingA;
    public bool isMovingB;


    //public Transform[] PuntosEnFuncion = new Transform[21];
    public int cantidadPuntos;

    public float timer;

    public float x1;
    public float x2;

    public float x;
    public float y;

    public float y1;
    public float y2;

    public float m;
    public float n;
    public float r;

    public float direccion;
    public string convertTo;

    public (float,float)[] arrayPositions;

    public const float VELOCIDAD = 0.02f;

    public Cards[] CartasMano = new Cards[7];
    public List<Cards> CartasNoDescartadas = new List<Cards>();
    public List<Cards> CartasDescartadas = new List<Cards>();

    public Cards[] CartasSeleccionadas = new Cards[7];

    public Cards[] Baraja = new Cards[20];

    public GameObject Canvas;

    public GameObject CardsPosition;
    public GameObject[] SpriteCards = new GameObject[7];

    public GameObject SendFormula;
    public GameObject FlechaDer;
    public GameObject FlechaIzq;


    public bool eligiendoMovimiento;

    public bool cartasElegidasMovimiento;
    public bool activarCartas;
    public bool hayChange;
    public bool elegirDireccion;
    


    public bool eligiendoAtaque;
    public bool aplicarMovimiento;
    public bool aplicarAtaque;

    /*

    ECUACIÓN DE LA CIRCUNFERENCIA

    x^2 + y^2 = r^2 --> y = sqrt(r^2 - x^2) PARA SACAR LOS CORTES CON EL RADIO

    PRIMERA, ECUACIÓN DE LA RECTA 

    y = mx + n --> Ecuación de la recta, NOTAR QUE N TIENE QUE SER MENOR QUE EL VALOR RADIO ABSOLUTO.

    mx + n = sqrt(r^2 - x^2) CORTES!!

    Primer corte --> x1 = ( ( -m * n + Mathf.Sqrt( (r * r) + (m * m) * (r * r) - (n * n)) ) / ( (m * m) + 1 ) );
    Segundo corte --> x2 = ( - ( m * n + Mathf.Sqrt( (r * r) + (m * m) * (r * r) - (n * n)) ) / ( (m * m) + 1 ) );

    SEGUNDA, FUNCIÓN CUADRATICA

    y = ax^2 + bx + c

    ax^2 + bx + c = sqrt(r^2 - x^2) CORTES!!

    La voy a simplificar (sino las lineas y calculos son desorbitados) para traer solo dos cortes

    mx^2 + n = sqrt(r^2 - x^2), NOTAR QUE N TIENE QUE SER MENOR QUE EL VALOR RADIO ABSOLUTO.

    Primer corte  --> x1 = Mathf.Sqrt(Mathf.Sqrt(4 * (m * m) * (r * r) + 4 * m * n + 1)/(m * m) - 1/(m * m) - (2 * n)/m)/Mathf.Sqrt(2)
    Segundo corte --> x2 = -Mathf.Sqrt(Mathf.Sqrt(4 * (m * m) * (r * r) + 4 * m * n + 1)/(m * m) - 1/(m * m) - (2 * n)/m)/Mathf.Sqrt(2)

    y = m^x + n

    m^x = sqrt(r^2 - x^2)
    (m^x)^2 = r^2 - x^2



    */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        eligiendoMovimiento = true;
        temporalPosition.position = new Vector3(0,0,0);

        cartasElegidasMovimiento = false;
        activarCartas = false;
        hayChange = false;
        elegirDireccion = false;
        convertTo = "";


        eligiendoAtaque = false;
        aplicarMovimiento = false;
        aplicarAtaque = false;
        
        isMoving = true;
        isMovingA = true;
        isMovingB = true;

        CartasNoDescartadas.AddRange(Baraja);

        cantidadPuntos = 21;
        arrayPositions = new (float,float)[cantidadPuntos];
        timer = 0;
        r = 10f;


        m = 0f;
        n = 0f;

        x = 0;
        x1 = 0;
        x2 = 0;

        y = 0;
        y1 = 0;
        y2 = 0;

        //Creamos las posiciones corte y donde x = 0 (pues es donde el personaje comenzará a moverse)

        //CASO RECTA

        /*

        x1 = (  ( -m * n + Mathf.Sqrt( (r * r) + (m * m) * (r * r) - (n * n)) ) / ( (m * m) + 1 ) );
        x2 = ( - ( m * n + Mathf.Sqrt( (r * r) + (m * m) * (r * r) - (n * n)) ) / ( (m * m) + 1 ) );

        y = m * x1 + n;
        arrayPositions[cantidadPuntos - 1] = (x1,y);
        y = m * x2 + n;
        arrayPositions[0] = (x2,y);
        y = n;
        arrayPositions[((cantidadPuntos - 1) / 2)] = (0f,y);

        // Revisamos nodos para movimiento

        for(int i = 1; i < ((cantidadPuntos - 1) / 2); i++) //Parte x1
        {
            y = m * ( i * x1 / ((cantidadPuntos - 1) / 2)) + n;
            arrayPositions[((cantidadPuntos - 1) / 2) + i] = (( i * x1 / ((cantidadPuntos - 1) / 2)),y);
        }
        for(int i = 1; i < ((cantidadPuntos - 1) / 2); i++) //Parte x2
        {
            y = m * ( i * x2 / ((cantidadPuntos - 1) / 2)) + n;
            arrayPositions[((cantidadPuntos - 1) / 2) - i] = (( i * x2 / ((cantidadPuntos - 1) / 2)),y);
        }

        */

        /*

        // CASO PARABOLA

        x1 = -Mathf.Sqrt(Mathf.Sqrt(4 * (m * m) * (r * r) + 4 * m * n + 1)/(m * m) - 1/(m * m) - (2 * n)/m)/Mathf.Sqrt(2);
        x2 = Mathf.Sqrt(Mathf.Sqrt(4 * (m * m) * (r * r) + 4 * m * n + 1)/(m * m) - 1/(m * m) - (2 * n)/m)/Mathf.Sqrt(2);

        y = m * (x1 * x1) + n;
        arrayPositions[cantidadPuntos - 1] = (x1,y);
        y = m * (x2 * x2) + n;
        arrayPositions[0] = (x2,y);
        y = n;
        arrayPositions[((cantidadPuntos - 1) / 2)] = (0f,y);

        // Revisamos nodos para movimiento

        for(int i = 1; i < ((cantidadPuntos - 1) / 2); i++) //Parte x1
        {
            y = m * ( i * x1 / ((cantidadPuntos - 1) / 2)) * ( i * x1 / ((cantidadPuntos - 1) / 2)) + n;
            arrayPositions[((cantidadPuntos - 1) / 2) + i] = (( i * x1 / ((cantidadPuntos - 1) / 2)),y);
        }
        for(int i = 1; i < ((cantidadPuntos - 1) / 2); i++) //Parte x2
        {
            y = m * ( i * x2 / ((cantidadPuntos - 1) / 2)) * ( i * x2 / ((cantidadPuntos - 1) / 2)) + n;
            arrayPositions[((cantidadPuntos - 1) / 2) - i] = (( i * x2 / ((cantidadPuntos - 1) / 2)),y);
        }

        */

        /*

        
        // CAMBIAR POS PUNTOS

        for(int i = 0; i < cantidadPuntos; i++)
        {
            PuntosEnFuncion[i].position = new Vector3(arrayPositions[i].Item1 + avatarPosition.position.x , 0.1f,  arrayPositions[i].Item2 + avatarPosition.position.z );
        }






        avatarPosition.position = new Vector3(arrayPositions[cantidadPuntos - 1].Item1 + avatarPosition.position.x  ,0, arrayPositions[cantidadPuntos - 1].Item2  + avatarPosition.position.z );

        */
        
    }

    // Update is called once per frame
    void Update()
    {
        if(eligiendoMovimiento)
        {
            if(!cartasElegidasMovimiento)
            {
                //Para volver a utilizarlas
            if(CartasNoDescartadas.Count < 7)
            {
                CartasNoDescartadas.AddRange(Baraja);
                CartasDescartadas = new List<Cards>();
                for(int i=0; i<CartasMano.Length;i++) CartasMano[i] = null;
            }
            
            for(int i=0; i<CartasMano.Length;i++)
            {
                if(CartasMano[i] == null)
                {
                int newCard = Random.Range(0,CartasNoDescartadas.Count);
                CartasMano[i] = CartasNoDescartadas[newCard];
                CartasNoDescartadas.Remove(CartasNoDescartadas[newCard]);
                SpriteCards[i].transform.GetChild(0).GetComponent<RawImage>().texture = CartasMano[i].sprite.texture;
                }
            }
            cartasElegidasMovimiento = true;
            activarCartas = true;

            }

            if(activarCartas)
            {
                for(int i = 0; i < SpriteCards.Length; i++)
                {
                    if(SpriteCards[i].GetComponent<Button>().enabled == false)
                    {
                        if(SpriteCards[i].GetComponent<RawImage>().color == Color.black)
                        {
                            if(CartasMano[i].type == "change" && !hayChange)
                            {
                                hayChange = true;
                                convertTo = CartasMano[i].convertTo;
                                SpriteCards[i].GetComponent<RawImage>().color = Color.green;
                            }
                            else if(CartasMano[i].type == "var")
                            {
                                m += CartasMano[i].value;
                                SpriteCards[i].GetComponent<RawImage>().color = Color.green;
                            }
                            else if(CartasMano[i].type == "const")
                            {
                                n += CartasMano[i].value;
                                SpriteCards[i].GetComponent<RawImage>().color = Color.green;
                            }
                            
                        }

                        else if(SpriteCards[i].GetComponent<RawImage>().color == Color.green)
                        {
                            if(CartasMano[i].type == "change")
                            {
                                hayChange = false;
                                convertTo = "";
                            }
                            else if(CartasMano[i].type == "var")
                            {
                                m -= CartasMano[i].value;
                            }
                            else if(CartasMano[i].type == "const")
                            {
                                n -= CartasMano[i].value;
                            }
                            SpriteCards[i].GetComponent<RawImage>().color = Color.black;
                        }

                        SpriteCards[i].GetComponent<Button>().enabled = true;
                    }
                }

                if (SendFormula.GetComponent<Button>().enabled == false)
                {
                    for(int i = 0; i < SpriteCards.Length; i++)
                    {
                        if(SpriteCards[i].GetComponent<RawImage>().color == Color.green)
                        {
                            SpriteCards[i].GetComponent<RawImage>().color = Color.black;
                            CartasDescartadas.Add(CartasMano[i]);
                            CartasMano[i] = null;
                        }
                    }
                    activarCartas = false;
                    elegirDireccion = true;
                    CardsPosition.SetActive(false);
                    SendFormula.GetComponent<Button>().enabled = true;
                    SendFormula.SetActive(false);

                    isMovingA = true;
                    isMovingB = true;

                    if(n > r) n = r;
                    else if (n < -r) n = -r;
                }


            }

            if(elegirDireccion)
            {
                if(convertTo == "log") isMovingA = false;
                if(isMovingA)
                {
                    x1 = x1 - VELOCIDAD;
                    if(convertTo == "") y1 = m * x1 + n;
                    if(convertTo == "log") y1 = m * Mathf.Log10(x1) + n;
                    else if(convertTo == "sen") y1 = m * Mathf.Sin(x1) + n;
                    else if(convertTo == "^2") y1 = m * (x1 * x1) + n;
                    
                    posibilityLeftPos.position = new Vector3(temporalPosition.position.x + x1,VELOCIDAD, temporalPosition.position.z + y1);
                }
                else if(isMovingB)
                {
                    x2 = x2 + VELOCIDAD;
                    if(convertTo == "") y2 = m * x2 + n;
                    else if(convertTo == "log") y2 = m * Mathf.Log10(x2) + n;
                    else if(convertTo == "sen") y2 = m * Mathf.Sin(x2) + n;
                    else if(convertTo == "^2") y2 = m * (x2 * x2) + n;
                    
                    posibilityRightPos.position = new Vector3(temporalPosition.position.x + x2,VELOCIDAD,temporalPosition.position.z + y2);
                }
                else if(!isMovingA && !isMovingB)
                {
                    FlechaDer.SetActive(true);
                    if(convertTo != "log") FlechaIzq.SetActive(true);

                    if (FlechaDer.GetComponent<Button>().enabled == false)
                    {
                        FlechaDer.GetComponent<Button>().enabled = true;
                        FlechaDer.SetActive(false);
                        FlechaIzq.SetActive(false);

                        elegirDireccion = false;
                        eligiendoMovimiento = false;
                        aplicarMovimiento = true;
                        isMoving = true;

                        direccion = 1f;

                    }
                    else if (FlechaIzq.GetComponent<Button>().enabled == false)
                    {
                        FlechaDer.GetComponent<Button>().enabled = true;
                        FlechaDer.SetActive(false);
                        FlechaIzq.SetActive(false);

                        elegirDireccion = false;
                        eligiendoMovimiento = false;
                        aplicarMovimiento = true;
                        isMoving = true;

                        direccion = -1f;

                    }
                }
            }


        }

        else if (aplicarMovimiento)

        {
            if(isMoving)
            {
                x = x + (direccion) * VELOCIDAD;
                if(convertTo == "") y = m * x + n;
                else if(convertTo == "log") y = m * Mathf.Log10(x) + n;
                else if(convertTo == "sen") y = m * Mathf.Sin(x) + n;
                else if(convertTo == "^2") y = m * (x * x) + n;
                    
                avatarPosition.position = new Vector3(temporalPosition.position.x + x,VELOCIDAD,temporalPosition.position.z + y);
            }
            else
            {
                eligiendoMovimiento = true;
                cartasElegidasMovimiento = false;
                activarCartas = false;
                hayChange = false;
                elegirDireccion = false;
                convertTo = "";
                aplicarMovimiento = false;

                CardsPosition.SetActive(true);
                SendFormula.SetActive(true);

                FlechaDer.GetComponent<Button>().enabled = true;
                FlechaIzq.GetComponent<Button>().enabled = true;
                
                posibilityLeftPos.position = avatarPosition.position;
                posibilityRightPos.position = avatarPosition.position;
                RadioGeneral.position = avatarPosition.position;
                temporalPosition.position = avatarPosition.position;

                m = 0f;
                n = 0f;
                
                x = 0;
                x1 = 0;
                x2 = 0;
                
                y = 0;
                y1 = 0;
                y2 = 0;

            }

        }

        else if (eligiendoAtaque)
        {

        }

        else if (aplicarAtaque)

        {

        }
        timer += Time.deltaTime;
        
        
    }
}
