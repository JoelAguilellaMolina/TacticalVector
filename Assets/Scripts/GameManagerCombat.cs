using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using TMPro;


public class GameManagerCombat : MonoBehaviour
{
    public Transform avatarPosition;
    public Transform ataquePosition;
    public GameObject VidaValueObjectJugador;
    public TMP_Text VidaValueJugador;
    public int VidaJugador;
    

    public Transform posibilityLeftPos;
    public Transform posibilityRightPos;
    public Transform RadioGeneral;
    public Transform temporalPosition;

    public Transform enemigoPosition;
    public Transform ataqueEnemigoPosition;
    public GameObject VidaValueObjectE1;
    public TMP_Text VidaValueE1;
    public TMP_Text actualFormula;
    public int VidaE1;

    public TMP_Text ProblemTimer;
    public GameObject aState;
    public GameObject bState;
    public GameObject cState;
    public GameObject dState;

    public Material correct;
    public Material incorrect;
    public GameObject Problemas;


    public Transform posibilityLeftPosEnemigo1;
    public Transform posibilityRightPosEnemigo1;
    public Transform RadioGeneralEnemigo1;
    public Transform temporalPositionEnemigo1;

    public bool isMoving;
    public bool isMovingA;
    public bool isMovingB;
    public bool isMoveState;
    public bool isEnemyTurn;

    public bool onProblem;
    public bool onProblemStart;
    public bool timeBonus;

    public bool movingAnim;
    public bool attackAnim;


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

    public float VELOCIDAD;

    public Cards[] CartasMano = new Cards[7];
    public List<Cards> CartasNoDescartadas = new List<Cards>();
    public List<Cards> CartasDescartadas = new List<Cards>();

    public Cards[] CartasSeleccionadas = new Cards[7];

    public Cards[] Baraja = new Cards[20];

    public Cards[] BarajaEnemigo1 = new Cards[7];

    public GameObject Canvas;

    public GameObject CardsPosition;
    public GameObject[] SpriteCards = new GameObject[7];

    public GameObject SendFormula;
    public GameObject ClearTurn;
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

    public GameObject virtualCamera;
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

        VELOCIDAD = 2* Time.deltaTime;
        virtualCamera = GameObject.Find("CinemachineCamera");
        
        VidaJugador = 20;
        VidaE1 = 30;
        VidaValueJugador.text = VidaJugador.ToString();
        VidaValueE1.text = VidaE1.ToString();


        isMoveState = true;
        isEnemyTurn = false;

        eligiendoMovimiento = true;
        temporalPosition.position = new Vector3(0,0,0);

        cartasElegidasMovimiento = false;
        activarCartas = false;
        hayChange = false;
        elegirDireccion = false;
        convertTo = "";
        actualFormula.text = "y = 0x + 0";

        onProblem = false;
        onProblemStart = true;
        timeBonus = true;


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
        ActualizarRadioVisual();


        if (!onProblem)      
        {

        
        if(eligiendoMovimiento)
        {
            if(!cartasElegidasMovimiento && !isEnemyTurn)
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
                SpriteCards[i].transform.GetChild(1).GetComponent<RawImage>().texture = CartasMano[i].sprite.texture; //el hijo que no es el fondo de la carta (2do)
                if(CartasMano[i].type == "const") SpriteCards[i].transform.GetChild(0).GetComponent<RawImage>().color = new Color(1f, 0.827451f, 0.6784314f, 1f);
                else if(CartasMano[i].type == "var") SpriteCards[i].transform.GetChild(0).GetComponent<RawImage>().color = new Color(0.6254902f, 0.8980392f, 0.8666667f, 1f);
                else if(CartasMano[i].type == "change") SpriteCards[i].transform.GetChild(0).GetComponent<RawImage>().color = new Color(0.7764706f, 0.6392157f, 0.8588236f, 1f);
                
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
                if (ClearTurn.GetComponent<Button>().enabled == false)
                {
                    if(!isEnemyTurn)
                    {

                    for(int i = 0; i < SpriteCards.Length; i++)
                    {
                        if(SpriteCards[i].GetComponent<RawImage>().color == Color.green)
                        {
                            SpriteCards[i].GetComponent<RawImage>().color = Color.black;
                        }
                    }
                    m = 0;
                    n = 0;
                    aplicarMovimiento = true;
                    isMoving = false;
                    eligiendoMovimiento = false;
                    
                    }
                    
                    ClearTurn.GetComponent<Button>().enabled = true;
                    ClearTurn.SetActive(true);
                }

            }

            if (isEnemyTurn && !cartasElegidasMovimiento)
                {
                    SendFormula.GetComponent<Button>().enabled = false;
                    CardsPosition.SetActive(false);
                    SendFormula.SetActive(false);

                    for(int i = 0; i < BarajaEnemigo1.Length; i++)
                    {
                        float random = Random.Range(0,2);
                        if(random > 0.5f)
                        {
                            if(BarajaEnemigo1[i].type == "change" && !hayChange)
                            {
                                hayChange = true;
                                convertTo = BarajaEnemigo1[i].convertTo;
                            }
                            else if(BarajaEnemigo1[i].type == "var")
                            {
                                m += BarajaEnemigo1[i].value;
                            }
                            else if(BarajaEnemigo1[i].type == "const")
                            {
                                n += BarajaEnemigo1[i].value;
                            }
                        }
                    }

                    //Se aplica la formula solo una vez de cambio de texto pues ya se ha elegido

                    ActualizarFormulaTexto();

                    activarCartas = false;
                    elegirDireccion = true;
                    CardsPosition.SetActive(false);
                    SendFormula.GetComponent<Button>().enabled = true;
                    SendFormula.SetActive(false);
                    cartasElegidasMovimiento = true;

                    isMovingA = true;
                    isMovingB = true;

                    if(n > r) n = r;
                    else if (n < -r) n = -r;
                }

            if(elegirDireccion)
            {
                if(convertTo == "log" && m!= 0) isMovingA = false;
                if(isMovingA)
                {
                    if(m < 1.5f && m > -1.5f && convertTo != "^2")
                    x1 = x1 - (VELOCIDAD * 2);
                    else
                    x1 = x1 - VELOCIDAD;

                    if(convertTo == "") y1 = m * x1 + n;
                    if(convertTo == "log") y1 = m * Mathf.Log10(x1) + n;
                    else if(convertTo == "sen") y1 = m * Mathf.Sin(x1) + n;
                    else if(convertTo == "^2") y1 = m * (x1 * x1) + n;
                    
                    if(!isEnemyTurn)
                    posibilityLeftPos.position = new Vector3(temporalPosition.position.x + x1,1, temporalPosition.position.z + y1);
                    else
                    posibilityLeftPosEnemigo1.position = new Vector3(temporalPositionEnemigo1.position.x + x1,1, temporalPositionEnemigo1.position.z + y1);
                }
                else if(isMovingB)
                {
                    if(m < 1.5f && m > -1.5f)
                    x2 = x2 + (VELOCIDAD * 2);
                    else
                    x2 = x2 + VELOCIDAD;

                    if(convertTo == "") y2 = m * x2 + n;
                    else if(convertTo == "log") y2 = m * Mathf.Log10(x2) + n;
                    else if(convertTo == "sen") y2 = m * Mathf.Sin(x2) + n;
                    else if(convertTo == "^2") y2 = m * (x2 * x2) + n;
                    
                    if(!isEnemyTurn)
                    posibilityRightPos.position = new Vector3(temporalPosition.position.x + x2,1,temporalPosition.position.z + y2);
                    else
                    posibilityRightPosEnemigo1.position = new Vector3(temporalPositionEnemigo1.position.x + x2,1,temporalPositionEnemigo1.position.z + y2);
                }

                else if(!isMovingA && !isMovingB)
                {
                    
                    FlechaDer.SetActive(true);
                    if(convertTo != "log" || m!= 0) FlechaIzq.SetActive(true);

                    if(isEnemyTurn) // Ia de opciones del enemigo
                    {
                        if(Mathf.Abs(temporalPosition.position.x - (temporalPositionEnemigo1.position.x)) > 15f || Mathf.Abs(temporalPosition.position.y - (temporalPositionEnemigo1.position.y)) < 15f) // si esta muy lejos simplemente coge la que mas se acerca en x
                        {
                            if(avatarPosition.position.x < enemigoPosition.position.x && (convertTo != "log" && m!= 0)) FlechaIzq.GetComponent<Button>().enabled = false;
                            else FlechaDer.GetComponent<Button>().enabled = false;
                        }
                        else // si esta cerca busca que posicion le pone más cerca en y de las dos
                        {
                            if(Mathf.Abs(posibilityLeftPosEnemigo1.position.y - -(avatarPosition.position.y)) < Mathf.Abs(posibilityRightPosEnemigo1.position.y - -(avatarPosition.position.y)) && (convertTo != "log" && m!= 0)) 
                            FlechaDer.GetComponent<Button>().enabled = false;
                            else FlechaIzq.GetComponent<Button>().enabled = false;
                        }
                    }

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
        
        if(!isEnemyTurn)
        {
            ActualizarFormulaTexto();
        }
        
        }

        else if (aplicarMovimiento)

        {
            if(isMoving)
            {
                if(m < 1.5f && m > -1.5f)
                x = x + (direccion) * (VELOCIDAD * 2);
                else
                x = x + (direccion) * VELOCIDAD;


                if(convertTo == "") y = m * x + n;
                else if(convertTo == "log") y = m * Mathf.Log10(x) + n;
                else if(convertTo == "sen") y = m * Mathf.Sin(x) + n;
                else if(convertTo == "^2") y = m * (x * x) + n;

                if(!isEnemyTurn)
                {

                if(isMoveState)
                {
                    avatarPosition.GetChild(1).GetComponent<Animator>().SetBool("isMoving", true);
                    avatarPosition.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isMoving", true);

                    avatarPosition.position = new Vector3(temporalPosition.position.x + x,VELOCIDAD,temporalPosition.position.z + y);
                }    
                
                else
                {
                    avatarPosition.GetChild(1).GetComponent<Animator>().SetBool("isAttacking", true);
                    avatarPosition.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isAttacking", true);

                    ataquePosition.gameObject.SetActive(true);
                    ataquePosition.position = new Vector3(temporalPosition.position.x + x,1,temporalPosition.position.z + y);

                }
                
                }

                else
                {

                if(isMoveState)
                {
                    enemigoPosition.GetChild(1).GetComponent<Animator>().SetBool("isMoving", true);
                    enemigoPosition.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isMoving", true);

                    enemigoPosition.position = new Vector3(temporalPositionEnemigo1.position.x + x,VELOCIDAD,temporalPositionEnemigo1.position.z + y);
                }    
                
                else
                {
                    enemigoPosition.GetChild(1).GetComponent<Animator>().SetBool("isAttacking", true);
                    enemigoPosition.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isAttacking", true);

                    ataqueEnemigoPosition.gameObject.SetActive(true);
                    ataqueEnemigoPosition.position = new Vector3(temporalPositionEnemigo1.position.x + x,1,temporalPositionEnemigo1.position.z + y);
                }
                

                }
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

                if(!isEnemyTurn)
                {

                posibilityLeftPos.position = avatarPosition.position;
                posibilityRightPos.position = avatarPosition.position;
                RadioGeneral.position = new Vector3(avatarPosition.position.x, VELOCIDAD ,avatarPosition.position.z);
                temporalPosition.position = avatarPosition.position;
                ataquePosition.position =  new Vector3(avatarPosition.position.x, -1 ,avatarPosition.position.z);
                ataquePosition.gameObject.SetActive(false);

                avatarPosition.GetChild(1).GetComponent<Animator>().SetBool("isMoving", false);
                avatarPosition.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isMoving", false);
                avatarPosition.GetChild(1).GetComponent<Animator>().SetBool("isAttacking", false);
                avatarPosition.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isAttacking", false);

                

                // Si es el turno finalizando de ataque va al siguiente enemigo

                if(!isMoveState)
                {
                    virtualCamera.GetComponent<CinemachineCamera>().Follow = enemigoPosition;
                    virtualCamera.GetComponent<CinemachineCamera>().LookAt = enemigoPosition;
                    isEnemyTurn = true;
                    
                    RadioGeneral.GetComponent<Animator>().SetBool("isRadioMov", true);
                }
                else
                {
                    RadioGeneral.GetComponent<Animator>().SetBool("isRadioMov", false);
                }
                

                }
                else
                {

                posibilityLeftPosEnemigo1.position = enemigoPosition.position;
                posibilityRightPosEnemigo1.position = enemigoPosition.position;
                RadioGeneralEnemigo1.position = new Vector3(enemigoPosition.position.x, VELOCIDAD ,enemigoPosition.position.z);
                temporalPositionEnemigo1.position = enemigoPosition.position;
                ataqueEnemigoPosition.position =  new Vector3(enemigoPosition.position.x, -1 ,enemigoPosition.position.z);
                ataqueEnemigoPosition.gameObject.SetActive(false);

                enemigoPosition.GetChild(1).GetComponent<Animator>().SetBool("isMoving", false);
                enemigoPosition.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isMoving", false);
                enemigoPosition.GetChild(1).GetComponent<Animator>().SetBool("isAttacking", false);
                enemigoPosition.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isAttacking", false);

                
                

                // Si es el turno finalizando de ataque va al siguiente enemigo

                if(!isMoveState)
                {
                virtualCamera.GetComponent<CinemachineCamera>().Follow = avatarPosition;
                virtualCamera.GetComponent<CinemachineCamera>().LookAt = avatarPosition;
                isEnemyTurn = false;

                RadioGeneralEnemigo1.GetComponent<Animator>().SetBool("isRadioMov", true);
                }

                else
                {
                
                RadioGeneralEnemigo1.GetComponent<Animator>().SetBool("isRadioMov", false);

                }
                

                }
                

                if(isMoveState) isMoveState = false;
                else isMoveState = true;

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

        

        }

        else if (onProblem)
        {
            if(onProblemStart)
            {
                timer = 0;
                onProblemStart = false;
            }

            if(timer >= 10)
            {
                ProblemTimer.text = "10";
                ProblemTimer.color = Color.red;
                timeBonus = false;
            }
            else
            {
                timer += Time.deltaTime;
                ProblemTimer.text = ((int)timer).ToString();
            }

            if(aState.GetComponent<Button>().enabled == false)
            {
                aState.GetComponent<Image>().color = Color.red;
            }
            
            if(cState.GetComponent<Button>().enabled == false)
            {
                cState.GetComponent<Image>().color = Color.red;
            }

            if(dState.GetComponent<Button>().enabled == false)
            {
                dState.GetComponent<Image>().color = Color.red;
            }

            if(bState.GetComponent<Button>().enabled == false)
            {
                int damage = 10;
                if(isEnemyTurn)
                {
                    if(timeBonus && aState.GetComponent<Button>().enabled && cState.GetComponent<Button>().enabled && dState.GetComponent<Button>().enabled)
                    damage = 0;
                    else
                    {
                        timer = 10;
                        int contIncorrecto = 0;
                        
                        if(!aState.GetComponent<Button>().enabled) contIncorrecto +=1;
                        if(!cState.GetComponent<Button>().enabled) contIncorrecto +=1;
                        if(!dState.GetComponent<Button>().enabled) contIncorrecto +=1;

                        if(contIncorrecto == 0) damage = (int)(damage/4); //Resistencia del 75%
                        else if(contIncorrecto == 1) damage = (int)(2 * damage / 3); //Resistencia del 33.333%
                        else if(contIncorrecto == 2) damage = damage; //Resistencia del 0%
                        else if(contIncorrecto == 3) damage = (int)(damage * 1.5f); // Resistencia del -50%
                        else if(contIncorrecto == 4) damage = damage * 2; // Resistencia del -100%
                    }
                    VidaJugador -= damage;
                }
                else
                {
                    if(timeBonus && aState.GetComponent<Button>().enabled && bState.GetComponent<Button>().enabled && dState.GetComponent<Button>().enabled)
                    damage = (int)(damage * 1.5f);
                    else
                    {
                        timer = 10;
                        int contIncorrecto = 0;
                        
                        if(!aState.GetComponent<Button>().enabled) contIncorrecto +=1;
                        if(!cState.GetComponent<Button>().enabled) contIncorrecto +=1;
                        if(!dState.GetComponent<Button>().enabled) contIncorrecto +=1;

                        if(contIncorrecto == 0) damage = damage; // 100% Potencia
                        else if(contIncorrecto == 1) damage = (int)(damage / 2); // 50% Potencia
                        else if(contIncorrecto == 2) damage = (int)(damage / 4); // 25 % Potencia
                        else if(contIncorrecto == 3) damage = (int)(damage / 10); // Resistencia del 10%
                        else if(contIncorrecto == 4) damage = 0; // 0% Potencia
                    }
                    VidaE1 -= damage;

                }

                onProblemStart = true;
                timeBonus = true;
                onProblem = false;

                aState.GetComponent<Image>().color = Color.black;
                bState.GetComponent<Image>().color = Color.black;
                cState.GetComponent<Image>().color = Color.black;
                dState.GetComponent<Image>().color = Color.black;
                ProblemTimer.color = Color.black;

                aState.GetComponent<Button>().enabled = true;
                bState.GetComponent<Button>().enabled = true;
                cState.GetComponent<Button>().enabled = true;
                dState.GetComponent<Button>().enabled = true;

                

                VidaValueJugador.text = VidaJugador.ToString();
                VidaValueE1.text = VidaE1.ToString();
                VidaValueObjectJugador.SetActive(true);
                VidaValueObjectE1.SetActive(true);
                Problemas.SetActive(false);

                int animNum = Random.Range(0,3);

                if(isEnemyTurn)
                {
                    VidaValueJugador.transform.GetChild(0).GetComponent<TMP_Text>().text = "-"+ damage.ToString();
                    VidaValueJugador.transform.GetChild(0).GetComponent<Animator>().SetBool("visibility", true);
                    VidaValueJugador.transform.GetChild(0).GetComponent<Animator>().SetInteger("animRand", animNum);
                }
                else
                {
                    VidaValueE1.transform.GetChild(0).GetComponent<TMP_Text>().text = "-"+ damage.ToString();
                    VidaValueE1.transform.GetChild(0).GetComponent<Animator>().SetBool("visibility", true);
                    VidaValueE1.transform.GetChild(0).GetComponent<Animator>().SetInteger("animRand", animNum);
                }

                

                

            }
            
            
        }
        
        
    }

    void ActualizarFormulaTexto()
    {
        string tempM = m.ToString();
        string tempN = n.ToString();

        if(m % 1 == Mathf.Abs(0.5f)) tempM = (m * 2).ToString() + "/2";
        if(m % 1 == Mathf.Abs(0.25f) || m % 1 == Mathf.Abs(0.75f)) tempM = (m * 4).ToString() + "/4";
        if(n % 1 == Mathf.Abs(0.5f)) tempN = (n * 2).ToString() + "/2";
        if(n % 1 == Mathf.Abs(0.25f) || n % 1 == Mathf.Abs(0.75f)) tempN = (n * 4).ToString() + "/4";

        if(n < 0) tempN = " - " + tempN.Substring(1,tempN.Length - 1); // quitando el negativo
        else tempN = " + " + tempN;

        if(m == 1f) tempM = "";
        if(m == -1f) tempM = "-";

        if(n == 0f && m != 0f) tempN = "";
        if(m == 0f) 
        {
            if(n >= 0f) actualFormula.text = "y = " + tempN.Substring(3,tempN.Length - 3); // quitando el positivo
            else actualFormula.text = "y = " + tempN;
        }
        else if(convertTo == "") actualFormula.text = "y = " + tempM + "x" + tempN;
        else if(convertTo == "log") actualFormula.text = "y = " + tempM + "log(x)" + tempN;
        else if(convertTo == "sen") actualFormula.text = "y = " + tempM + "sen(x)" + tempN;
        else if(convertTo == "^2") actualFormula.text = "y = " + tempM + "x^2" + tempN;
    }

    void ActualizarRadioVisual()
    {
        if(!isMoveState && !isEnemyTurn)
        RadioGeneral.GetComponent<Animator>().SetBool("isRadioMov", false);
        else RadioGeneral.GetComponent<Animator>().SetBool("isRadioMov", true);

        if(!isMoveState && isEnemyTurn)
        RadioGeneralEnemigo1.GetComponent<Animator>().SetBool("isRadioMov", false);
        else RadioGeneralEnemigo1.GetComponent<Animator>().SetBool("isRadioMov", true);
    }
}
