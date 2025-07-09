using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using System.Runtime;
using System.Linq;
using TMPro;


public class GameManagerCombat : MonoBehaviour
{
    [Header("Jugador")]

    public Transform posibilityLeftPos;
    public Transform posibilityRightPos;
    public Transform RadioGeneral;
    public Transform temporalPosition;

    public Transform avatarPosition;
    public Transform ataquePosition;
    public GameObject VidaValueObjectJugador;
    public TMP_Text VidaValueJugador;

    public int VidaJugador;


    [Header("Enemigo 1")]

    public Transform posibilityLeftPosEnemigo1;
    public Transform posibilityRightPosEnemigo1;
    public Transform RadioGeneralEnemigo1;
    public Transform temporalPositionEnemigo1;

    public Transform enemigoPosition1;
    public Transform ataqueEnemigoPosition1;
    public GameObject VidaValueObjectE1;
    public TMP_Text VidaValueE1;
    
    public int VidaE1;
    public float r1;

    [Header("Enemigo 2")]

    public Transform posibilityLeftPosEnemigo2;
    public Transform posibilityRightPosEnemigo2;
    public Transform RadioGeneralEnemigo2;
    public Transform temporalPositionEnemigo2;

    public Transform enemigoPosition2;
    public Transform ataqueEnemigoPosition2;
    public GameObject VidaValueObjectE2;
    public TMP_Text VidaValueE2;
    
    public int VidaE2;
    public float r2;

    [Header("Enemigo 3")]

    public Transform posibilityLeftPosEnemigo3;
    public Transform posibilityRightPosEnemigo3;
    public Transform RadioGeneralEnemigo3;
    public Transform temporalPositionEnemigo3;

    public Transform enemigoPosition3;
    public Transform ataqueEnemigoPosition3;
    public GameObject VidaValueObjectE3;
    public TMP_Text VidaValueE3;
    
    public int VidaE3;
    public float r3;

    [Header("Problemas")]

    public TMP_Text ProblemTimer;
    public GameObject aState;
    public GameObject bState;
    public GameObject cState;
    public GameObject dState;
    public GameObject eState;

    public Material correct;
    public Material incorrect;
    public GameObject Problemas;

    public TMP_Text actualFormula;


    public List<Problems> ProblemasEnemigo1 = new List<Problems>();
    public List<Problems> ProblemasEnemigo2 = new List<Problems>();
    public List<Problems> ProblemasEnemigo3 = new List<Problems>();

    public int indiceProblema1;
    public int indiceProblema2;
    public int indiceProblema3;

    [Header("Estados en el turno")]

    
    
    public bool isMoving;
    public bool isMovingA;
    public bool isMovingB;
    public bool isMoveState;
    public bool isEnemyTurn;
    public bool isWaited;

    public bool onProblem;
    public bool onProblemStart;
    public bool resuelto;
    public bool timeBonus;
    public bool isAnimationFinished;
    public bool pistaUsada;

    public float maxTimeBonus;
    public Problems actualProblem;
    public Transform Problema;
    public string solucion;

    public bool movingAnim;
    public bool attackAnim;

    public bool eligiendoMovimiento;

    public bool cartasElegidasMovimiento;
    public bool activarCartas;
    public bool hayChange;
    public bool elegirDireccion;
    


    public bool eligiendoAtaque;
    public bool aplicarMovimiento;
    public bool aplicarAtaque;


    //public Transform[] PuntosEnFuncion = new Transform[21];

    [Header("Valores relevantes")]

    public int turnRotation;
    public int cantidadPuntos;
    public int enemyHit;
    public int actualBaseDamage;
    public int numEnemies;

    

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

    public int secondsToUnselect;

    public float direccion;
    public string convertTo;

    public (float,float)[] arrayPositions;

    public List<Cards> DropsPosibles = new List<Cards>();
    public Transform CartaConseguida;
    public bool isDropVida;
    public bool isDropRango;

    public float VELOCIDAD;

    [Header("Cartas")]

    public Cards[] CartasMano = new Cards[7];
    public List<Cards> CartasNoDescartadas = new List<Cards>();
    public List<Cards> CartasDescartadas = new List<Cards>();

    public Cards[] CartasSeleccionadas = new Cards[7];

    public Cards[] Baraja = new Cards[20];

    public Cards[] BarajaEnemigo1 = new Cards[7];
    public Cards[] BarajaEnemigo2 = new Cards[7];
    public Cards[] BarajaEnemigo3 = new Cards[7];

    public GameObject Canvas;

    public GameObject CardsPosition;
    public GameObject[] SpriteCards = new GameObject[7];

    [Header("UI")]

    public GameObject SendFormula;
    public GameObject ClearTurn;
    public GameObject FlechaDer;
    public GameObject FlechaIzq;

    public Transform Finalizado;

    [Header("Camara")]

    public GameObject virtualCamera;

    [Header("Tutorial")]
    public GameObject Tutorial;

    public bool isTutorial;
    public bool tutorialStep1;
    public bool tutorialStep2;
    public bool tutorialStep3;
    public bool tutorialStep4;
    public bool tutorialStep5;

    [Header("DontDestroyComponents")]

    public DataSave dataSave;
    public Transform OptionButton;
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
        dataSave = GameObject.Find("DataSave").transform.GetComponent<DataSave>();
        Shuffle(ProblemasEnemigo1);
        Shuffle(ProblemasEnemigo2);
        Shuffle(ProblemasEnemigo3);

        virtualCamera.GetComponent<CinemachineCamera>().Follow = avatarPosition;
        virtualCamera.GetComponent<CinemachineCamera>().LookAt = avatarPosition;

        virtualCamera.GetComponent<CinemachineCamera>().Follow = null;
        virtualCamera.GetComponent<CinemachineCamera>().LookAt = null;

        indiceProblema1 = 0;
        indiceProblema2 = 0;
        indiceProblema3 = 0;

        float maxTimeBonus = 9999f;
        string solucion = "";

        turnRotation = 0;
        enemyHit = 0;
        secondsToUnselect = 1;

        VELOCIDAD = 7 * Time.fixedDeltaTime;

        //virtualCamera = GameObject.Find("CinemachineCamera");

        /*
        
        VidaJugador = 20;

        VidaE1 = 30;
        VidaE2 = 30;
        VidaE3 = 30;

        */

        //JUGADOR

        Baraja = dataSave.CartasEnBaraja.ToArray();
        ShuffleBaraja(Baraja);
        if(dataSave.boostRadio)
        {
            RadioGeneral.localScale = new Vector3(1.5f,1.5f,1f);
            RadioGeneral.GetComponent<SphereCollider>().radius = 14.55f;
            r = 15f;
        }
        else r = 10f;
        if(dataSave.boostVida)
        {
            VidaJugador = 100;
        }
        else VidaJugador = 50;


        VidaValueJugador.text = VidaJugador.ToString();
        VidaValueE1.text = VidaE1.ToString();
        VidaValueE2.text = VidaE2.ToString();
        VidaValueE3.text = VidaE3.ToString();


        isMoveState = true;
        isEnemyTurn = false;
        isWaited = true;

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
        isAnimationFinished = false;
        pistaUsada = false;


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
        


        m = 0f;
        n = 0f;

        x = 0;
        x1 = 0;
        x2 = 0;

        y = 0;
        y1 = 0;
        y2 = 0;

        if(isTutorial) virtualCamera.GetComponent<PanZoom>().enabled = false;

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

    // Utilizado Fixed Update para que se muevan independientemente de los framerates
    void FixedUpdate()
    {
        
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
                for(int i = 0; i < dataSave.nivelesCompletados.Length; i++)
                {
                    if(SceneManager.GetActiveScene().name == "Nivel" + (i+1).ToString()) dataSave.nivelesCompletados[i] = true;
                }
                SceneManager.LoadScene("Mapa");
            }
            
        }
        else

        {

        
        
        SkipDefeatedEnemies();
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
                if(CartasMano[i].type == "const") SpriteCards[i].transform.GetChild(0).GetComponent<RawImage>().color = new Color(1f, 0.827451f, 0.6784314f, 1f); //Color Para const
                else if(CartasMano[i].type == "var") SpriteCards[i].transform.GetChild(0).GetComponent<RawImage>().color = new Color(0.6254902f, 0.8980392f, 0.8666667f, 1f); //Color Para var
                else if(CartasMano[i].type == "change") SpriteCards[i].transform.GetChild(0).GetComponent<RawImage>().color = new Color(0.7764706f, 0.6392157f, 0.8588236f, 1f); //Color Para change
                
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
                        if(isTutorial && tutorialStep1 && CartasMano[i].type != "change")
                        {
                            tutorialStep1 = false;
                            tutorialStep2 = true;
                            Tutorial.transform.Find("Tutorial8").gameObject.SetActive(false);
                            Tutorial.transform.Find("Tutorial9").gameObject.SetActive(true);
                            Tutorial.transform.Find("FocusJugador7").gameObject.SetActive(false);
                            Tutorial.transform.Find("FocusJugador8").gameObject.SetActive(true);
                        }
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

                    if(isTutorial && tutorialStep4)
                    {
                        tutorialStep4 = false;
                        tutorialStep5 = true;

                        Tutorial.transform.Find("Tutorial15").gameObject.SetActive(false);
                        Tutorial.transform.Find("Tutorial16").gameObject.SetActive(true);
                        Tutorial.transform.Find("FocusJugador14").gameObject.SetActive(false);
                        Tutorial.transform.Find("FocusJugador15").gameObject.SetActive(true);

                    }

                    
                    
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

                    if(turnRotation == 1)
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
                    else if(turnRotation == 2)
                    for(int i = 0; i < BarajaEnemigo2.Length; i++)
                    {
                        float random = Random.Range(0,2);
                        if(random > 0.5f)
                        {
                            if(BarajaEnemigo2[i].type == "change" && !hayChange)
                            {
                                hayChange = true;
                                convertTo = BarajaEnemigo2[i].convertTo;
                            }
                            else if(BarajaEnemigo2[i].type == "var")
                            {
                                m += BarajaEnemigo2[i].value;
                            }
                            else if(BarajaEnemigo2[i].type == "const")
                            {
                                n += BarajaEnemigo2[i].value;
                            }
                        }
                    }
                    else if(turnRotation == 3)
                    for(int i = 0; i < BarajaEnemigo3.Length; i++)
                    {
                        float random = Random.Range(0,2);
                        if(random > 0.5f)
                        {
                            if(BarajaEnemigo3[i].type == "change" && !hayChange)
                            {
                                hayChange = true;
                                convertTo = BarajaEnemigo3[i].convertTo;
                            }
                            else if(BarajaEnemigo3[i].type == "var")
                            {
                                m += BarajaEnemigo3[i].value;
                            }
                            else if(BarajaEnemigo3[i].type == "const")
                            {
                                n += BarajaEnemigo3[i].value;
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

                    if(turnRotation == 1)
                    {
                        if(n > r1) n = r1;
                        else if (n < -r1) n = -r1;
                    }
                    else if(turnRotation == 2)
                    {
                        if(n > r1) n = r1;
                        else if (n < -r1) n = -r1;
                    }
                    else if(turnRotation == 3)
                    {
                        if(n > r1) n = r1;
                        else if (n < -r1) n = -r1;
                    }
                    
                }

            if(elegirDireccion)
            {
                if(convertTo == "log") isMovingA = false;
                if(isMovingA)
                {
                    if(m < 1.5f && m > -1.5f && convertTo != "^2")
                    x1 = x1 - (VELOCIDAD * 2);
                    else
                    x1 = x1 - VELOCIDAD;

                    if(convertTo == "") y1 = m * x1 + n;
                    if(convertTo == "log") y1 = m * Mathf.Log10(x1) + n;
                    else if(convertTo == "sen") y1 = m * Mathf.Sin(x1) + n;
                    else if(convertTo == "cos") y1 = m * Mathf.Cos(x1) + n;
                    else if(convertTo == "^2") y1 = m * (x1 * x1) + n;
                    
                    if(!isEnemyTurn)
                    posibilityLeftPos.position = new Vector3(temporalPosition.position.x + x1,1, temporalPosition.position.z + y1);
                    else
                    {
                        if(turnRotation == 1) posibilityLeftPosEnemigo1.position = new Vector3(temporalPositionEnemigo1.position.x + x1,1, temporalPositionEnemigo1.position.z + y1);
                        if(turnRotation == 2) posibilityLeftPosEnemigo2.position = new Vector3(temporalPositionEnemigo2.position.x + x1,1, temporalPositionEnemigo2.position.z + y1);
                        if(turnRotation == 3) posibilityLeftPosEnemigo3.position = new Vector3(temporalPositionEnemigo3.position.x + x1,1, temporalPositionEnemigo3.position.z + y1);
                    }
                    
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
                    else if(convertTo == "cos") y2 = m * Mathf.Sin(x2) + n;
                    else if(convertTo == "^2") y2 = m * (x2 * x2) + n;
                    
                    if(!isEnemyTurn)
                    posibilityRightPos.position = new Vector3(temporalPosition.position.x + x2,1,temporalPosition.position.z + y2);
                    else
                    {
                        if(turnRotation == 1) posibilityRightPosEnemigo1.position = new Vector3(temporalPositionEnemigo1.position.x + x2,1,temporalPositionEnemigo1.position.z + y2);
                        if(turnRotation == 2) posibilityRightPosEnemigo2.position = new Vector3(temporalPositionEnemigo2.position.x + x2,1,temporalPositionEnemigo2.position.z + y2);
                        if(turnRotation == 3) posibilityRightPosEnemigo3.position = new Vector3(temporalPositionEnemigo3.position.x + x2,1,temporalPositionEnemigo3.position.z + y2);
                    }
                    
                }

                else if(!isMovingA && !isMovingB)
                {
                    float secsToWait = 0.5f;
                    if(!isWaited)
                    {
                        timer+= Time.fixedDeltaTime;
                        if(timer > secsToWait)
                        {
                            isWaited = true;
                        }

                    }
                    else
                    {

                    
                    FlechaDer.SetActive(true);
                    if(convertTo != "log") FlechaIzq.SetActive(true);

                    if(isTutorial && tutorialStep2)
                    {
                        tutorialStep2 = false;
                        tutorialStep3 = true;

                        Tutorial.transform.Find("Tutorial11").gameObject.SetActive(false);
                        Tutorial.transform.Find("Tutorial12").gameObject.SetActive(true);
                        Tutorial.transform.Find("FocusJugador10").gameObject.SetActive(false);
                        Tutorial.transform.Find("FocusJugador11").gameObject.SetActive(true);

                    }

                    if(isEnemyTurn && isWaited) // Ia de opciones del enemigo (Revisa que hipotenusa es menor y la elige entre las dos opciones (x^2 + z^2 de ambos))
                    {
                        
                        if(isWaited && timer < secsToWait)
                        {
                            FlechaDer.SetActive(false);
                            FlechaIzq.SetActive(false);
                            isWaited = false;
                            return;
                        }
                        
                        
                        

                        if(turnRotation == 1)
                        {

                        if ( ( (posibilityLeftPosEnemigo1.position.x - avatarPosition.position.x) * (posibilityLeftPosEnemigo1.position.x - avatarPosition.position.x) +
                        (posibilityLeftPosEnemigo1.position.z - avatarPosition.position.z) * (posibilityLeftPosEnemigo1.position.z - avatarPosition.position.z) <
                        (posibilityRightPosEnemigo1.position.x - avatarPosition.position.x) * (posibilityRightPosEnemigo1.position.x - avatarPosition.position.x) +
                        (posibilityRightPosEnemigo1.position.z - avatarPosition.position.z) * (posibilityRightPosEnemigo1.position.z - avatarPosition.position.z) )&&      
                        (convertTo != "log") ) FlechaIzq.GetComponent<Button>().enabled = false;
                        else FlechaDer.GetComponent<Button>().enabled = false;

                        }

                        else if(turnRotation == 2)
                        {
                            
                        if ( ( (posibilityLeftPosEnemigo2.position.x - avatarPosition.position.x) * (posibilityLeftPosEnemigo2.position.x - avatarPosition.position.x) +
                        (posibilityLeftPosEnemigo2.position.z - avatarPosition.position.z) * (posibilityLeftPosEnemigo2.position.z - avatarPosition.position.z) <
                        (posibilityRightPosEnemigo2.position.x - avatarPosition.position.x) * (posibilityRightPosEnemigo2.position.x - avatarPosition.position.x) +
                        (posibilityRightPosEnemigo2.position.z - avatarPosition.position.z) * (posibilityRightPosEnemigo2.position.z - avatarPosition.position.z) )&&      
                        (convertTo != "log") ) FlechaIzq.GetComponent<Button>().enabled = false;
                        else FlechaDer.GetComponent<Button>().enabled = false;

                        }

                        else if(turnRotation == 3)
                        {
                            
                        if ( ( (posibilityLeftPosEnemigo3.position.x - avatarPosition.position.x) * (posibilityLeftPosEnemigo3.position.x - avatarPosition.position.x) +
                        (posibilityLeftPosEnemigo3.position.z - avatarPosition.position.z) * (posibilityLeftPosEnemigo3.position.z - avatarPosition.position.z) <
                        (posibilityRightPosEnemigo3.position.x - avatarPosition.position.x) * (posibilityRightPosEnemigo3.position.x - avatarPosition.position.x) +
                        (posibilityRightPosEnemigo3.position.z - avatarPosition.position.z) * (posibilityRightPosEnemigo3.position.z - avatarPosition.position.z) )&&      
                        (convertTo != "log") ) FlechaIzq.GetComponent<Button>().enabled = false;
                        else FlechaDer.GetComponent<Button>().enabled = false;

                        }
                        
                        
                    }
                    else if(!isEnemyTurn) 
                    {
                        virtualCamera.GetComponent<CinemachineCamera>().Follow = avatarPosition;
                        virtualCamera.GetComponent<CinemachineCamera>().LookAt = avatarPosition;
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

                        if(isTutorial && tutorialStep3)
                        {
                            tutorialStep3 = false;
                            tutorialStep4 = true;

                            
                            Tutorial.transform.Find("Tutorial13").gameObject.SetActive(false);
                            Tutorial.transform.Find("Tutorial14").gameObject.SetActive(true);
                            Tutorial.transform.Find("FocusJugador12").gameObject.SetActive(false);
                            Tutorial.transform.Find("FocusJugador13").gameObject.SetActive(true);
                        }

                        

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

                        if(isTutorial && tutorialStep3)
                        {
                            tutorialStep3 = false;
                            tutorialStep4 = true;

                            
                            Tutorial.transform.Find("Tutorial13").gameObject.SetActive(false);
                            Tutorial.transform.Find("Tutorial14").gameObject.SetActive(true);
                            Tutorial.transform.Find("FocusJugador12").gameObject.SetActive(false);
                            Tutorial.transform.Find("FocusJugador13").gameObject.SetActive(true);
                        }

                        

                    }

                    if(turnRotation == 0)
                    {
                    Vector3 direccionAMirar;
                    if(direccion == 1f) direccionAMirar = posibilityRightPos.position - avatarPosition.position;
                    else direccionAMirar = posibilityLeftPos.position - avatarPosition.position;
                    Quaternion rotation = Quaternion.LookRotation(direccionAMirar);
                    avatarPosition.rotation = rotation;
                    }

                    if(turnRotation == 1)
                    {
                    Vector3 direccionAMirar;
                    if(direccion == 1f) direccionAMirar = posibilityRightPosEnemigo1.position - enemigoPosition1.position;
                    else direccionAMirar = posibilityLeftPosEnemigo1.position - enemigoPosition1.position;
                    Quaternion rotation = Quaternion.LookRotation(direccionAMirar);
                    enemigoPosition1.rotation = rotation;
                    }

                    if(turnRotation == 2)
                    {
                    Vector3 direccionAMirar;
                    if(direccion == 1f) direccionAMirar = posibilityRightPosEnemigo2.position - enemigoPosition2.position;
                    else direccionAMirar = posibilityLeftPosEnemigo2.position - enemigoPosition2.position;
                    Quaternion rotation = Quaternion.LookRotation(direccionAMirar);
                    enemigoPosition2.rotation = rotation;
                    }

                    if(turnRotation == 3)
                    {
                    Vector3 direccionAMirar;
                    if(direccion == 1f) direccionAMirar = posibilityRightPosEnemigo3.position - enemigoPosition3.position;
                    else direccionAMirar = posibilityLeftPosEnemigo3.position - enemigoPosition3.position;
                    Quaternion rotation = Quaternion.LookRotation(direccionAMirar);
                    enemigoPosition3.rotation = rotation;
                    }

                    timer = 0f;

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
                else if(convertTo == "cos") y = m * Mathf.Cos(x) + n;
                else if(convertTo == "^2") y = m * (x * x) + n;

                

                if(!isEnemyTurn)
                {

                if(isMoveState)
                {
                    avatarPosition.GetChild(1).GetComponent<Animator>().SetBool("isMoving", true);
                    avatarPosition.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isMoving", true);

                    avatarPosition.position = new Vector3(temporalPosition.position.x + x,avatarPosition.position.y,temporalPosition.position.z + y);

                    
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
                    if(turnRotation == 1)
                    {

                    enemigoPosition1.GetChild(1).GetComponent<Animator>().SetBool("isMoving", true);
                    enemigoPosition1.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isMoving", true);

                    enemigoPosition1.position = new Vector3(temporalPositionEnemigo1.position.x + x,enemigoPosition1.position.y,temporalPositionEnemigo1.position.z + y);

                    }
                    else if(turnRotation == 2)
                    {
                        
                    enemigoPosition2.GetChild(1).GetComponent<Animator>().SetBool("isMoving", true);
                    enemigoPosition2.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isMoving", true);

                    enemigoPosition2.position = new Vector3(temporalPositionEnemigo2.position.x + x,enemigoPosition2.position.y,temporalPositionEnemigo2.position.z + y);
                    }
                    else if(turnRotation == 3)
                    {
                        
                    enemigoPosition3.GetChild(1).GetComponent<Animator>().SetBool("isMoving", true);
                    enemigoPosition3.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isMoving", true);

                    enemigoPosition3.position = new Vector3(temporalPositionEnemigo3.position.x + x,enemigoPosition3.position.y,temporalPositionEnemigo3.position.z + y);
                    }

                    
                    
                }    
                
                else
                {
                    if(turnRotation == 1)
                    {

                    enemigoPosition1.GetChild(1).GetComponent<Animator>().SetBool("isAttacking", true);
                    enemigoPosition1.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isAttacking", true);

                    ataqueEnemigoPosition1.gameObject.SetActive(true);
                    ataqueEnemigoPosition1.position = new Vector3(temporalPositionEnemigo1.position.x + x,1,temporalPositionEnemigo1.position.z + y);

                    }

                    else if(turnRotation == 2)
                    {

                    enemigoPosition2.GetChild(1).GetComponent<Animator>().SetBool("isAttacking", true);
                    enemigoPosition2.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isAttacking", true);

                    ataqueEnemigoPosition2.gameObject.SetActive(true);
                    ataqueEnemigoPosition2.position = new Vector3(temporalPositionEnemigo2.position.x + x,1,temporalPositionEnemigo2.position.z + y);

                    
                    }

                    else if(turnRotation == 3)
                    {

                    enemigoPosition3.GetChild(1).GetComponent<Animator>().SetBool("isAttacking", true);
                    enemigoPosition3.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isAttacking", true);

                    ataqueEnemigoPosition3.gameObject.SetActive(true);
                    ataqueEnemigoPosition3.position = new Vector3(temporalPositionEnemigo3.position.x + x,1,temporalPositionEnemigo3.position.z + y);
                    
                    }
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
                StartCoroutine(WaitForEmitting(secondsToUnselect));
                /*
                posibilityLeftPos.GetComponent<TrailRenderer>().emitting = true;
                posibilityRightPos.GetComponent<TrailRenderer>().emitting = true;
                */



                RadioGeneral.position = new Vector3(avatarPosition.position.x, RadioGeneral.position.y ,avatarPosition.position.z);
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
                    if(enemigoPosition1.position.y < 9000f)
                    {
                    virtualCamera.GetComponent<CinemachineCamera>().Follow = enemigoPosition1;
                    virtualCamera.GetComponent<CinemachineCamera>().LookAt = enemigoPosition1;
                    }
                    
                    isEnemyTurn = true;
                    
                    RadioGeneral.GetComponent<Animator>().SetBool("isRadioMov", true);

                    turnRotation += 1;
                }
                else
                {
                    RadioGeneral.GetComponent<Animator>().SetBool("isRadioMov", false);

                    virtualCamera.GetComponent<CinemachineCamera>().Follow = avatarPosition;
                    virtualCamera.GetComponent<CinemachineCamera>().LookAt = avatarPosition;
                    StartCoroutine(WaitForUnselect(secondsToUnselect));
                }
                

                }
                else
                {
                
                if(turnRotation == 1)
                {
                posibilityLeftPosEnemigo1.position = enemigoPosition1.position;
                posibilityRightPosEnemigo1.position = enemigoPosition1.position;
                AllEmit();

                RadioGeneralEnemigo1.position = new Vector3(enemigoPosition1.position.x, RadioGeneralEnemigo1.position.y ,enemigoPosition1.position.z);
                temporalPositionEnemigo1.position = enemigoPosition1.position;
                ataqueEnemigoPosition1.position =  new Vector3(enemigoPosition1.position.x, -1 ,enemigoPosition1.position.z);
                ataqueEnemigoPosition1.gameObject.SetActive(false);

                enemigoPosition1.GetChild(1).GetComponent<Animator>().SetBool("isMoving", false);
                enemigoPosition1.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isMoving", false);
                enemigoPosition1.GetChild(1).GetComponent<Animator>().SetBool("isAttacking", false);
                enemigoPosition1.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isAttacking", false);
                }
                else if(turnRotation == 2)
                {
                posibilityLeftPosEnemigo2.position = enemigoPosition2.position;
                posibilityRightPosEnemigo2.position = enemigoPosition2.position;
                AllEmit();
                
                RadioGeneralEnemigo2.position = new Vector3(enemigoPosition2.position.x, RadioGeneralEnemigo2.position.y ,enemigoPosition2.position.z);
                temporalPositionEnemigo2.position = enemigoPosition2.position;
                ataqueEnemigoPosition2.position =  new Vector3(enemigoPosition2.position.x, -1 ,enemigoPosition2.position.z);
                ataqueEnemigoPosition2.gameObject.SetActive(false);

                enemigoPosition2.GetChild(1).GetComponent<Animator>().SetBool("isMoving", false);
                enemigoPosition2.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isMoving", false);
                enemigoPosition2.GetChild(1).GetComponent<Animator>().SetBool("isAttacking", false);
                enemigoPosition2.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isAttacking", false);
                }
                else if(turnRotation == 3)
                {
                posibilityLeftPosEnemigo3.position = enemigoPosition3.position;
                posibilityRightPosEnemigo3.position = enemigoPosition3.position;
                AllEmit();
                
                RadioGeneralEnemigo3.position = new Vector3(enemigoPosition3.position.x, RadioGeneralEnemigo3.position.y ,enemigoPosition3.position.z);
                temporalPositionEnemigo3.position = enemigoPosition3.position;
                ataqueEnemigoPosition3.position =  new Vector3(enemigoPosition3.position.x, -1 ,enemigoPosition3.position.z);
                ataqueEnemigoPosition3.gameObject.SetActive(false);

                enemigoPosition3.GetChild(1).GetComponent<Animator>().SetBool("isMoving", false);
                enemigoPosition3.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isMoving", false);
                enemigoPosition3.GetChild(1).GetComponent<Animator>().SetBool("isAttacking", false);
                enemigoPosition3.GetChild(1).GetChild(7).GetComponent<Animator>().SetBool("isAttacking", false);
                }

                

                
                

                // Si es el turno finalizando de ataque va al siguiente enemigo
                

                if(!isMoveState)
                {

                turnRotation += 1;

                if(turnRotation == 2)
                {
                    RadioGeneralEnemigo1.GetComponent<Animator>().SetBool("isRadioMov", true);
                    if(enemigoPosition2.position.y < 9000f)
                    {
                    virtualCamera.GetComponent<CinemachineCamera>().Follow = enemigoPosition2;
                    virtualCamera.GetComponent<CinemachineCamera>().LookAt = enemigoPosition2;
                    }
                    
                }

                else if(turnRotation == 3)
                {
                    RadioGeneralEnemigo2.GetComponent<Animator>().SetBool("isRadioMov", true);
                    if(enemigoPosition3.position.y < 9000f)
                    {
                    virtualCamera.GetComponent<CinemachineCamera>().Follow = enemigoPosition3;
                    virtualCamera.GetComponent<CinemachineCamera>().LookAt = enemigoPosition3;
                    }
                    
                }

                else if(turnRotation == 4)
                {
                    RadioGeneralEnemigo3.GetComponent<Animator>().SetBool("isRadioMov", true);
                    
                    virtualCamera.GetComponent<CinemachineCamera>().Follow = avatarPosition;
                    virtualCamera.GetComponent<CinemachineCamera>().LookAt = avatarPosition;
                    StartCoroutine(WaitForUnselect(secondsToUnselect));

                    turnRotation = 0;
                    isEnemyTurn = false;
                }
                
                }

                else
                {
                if(turnRotation == 1) RadioGeneralEnemigo1.GetComponent<Animator>().SetBool("isRadioMov", false);
                else if(turnRotation == 2) RadioGeneralEnemigo2.GetComponent<Animator>().SetBool("isRadioMov", false);
                else if(turnRotation == 3) RadioGeneralEnemigo3.GetComponent<Animator>().SetBool("isRadioMov", false);
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
                if(enemyHit == 1) 
                {
                    actualProblem = ProblemasEnemigo1[indiceProblema1];
                    if(indiceProblema1 == ProblemasEnemigo1.Count - 1) indiceProblema1 = 0;
                    else indiceProblema1++;
                }
                else if(enemyHit == 2)
                {
                    actualProblem = ProblemasEnemigo2[indiceProblema2];
                    if(indiceProblema2 == ProblemasEnemigo2.Count - 1) indiceProblema2 = 0;
                    else indiceProblema2++;
                } 
                else if (enemyHit == 3)
                {
                    actualProblem = ProblemasEnemigo3[indiceProblema3];
                    if(indiceProblema3 == ProblemasEnemigo3.Count - 1) indiceProblema3 = 0;
                    else indiceProblema3++;
                } 

                

                Problema = Problemas.transform.GetChild(0).GetChild(0);
                Problemas.transform.GetChild(0).GetComponent<Animator>().SetBool("initProblem",true);

                actualBaseDamage = Random.Range((int)(actualProblem.baseDamage-5),(int)(actualProblem.baseDamage+5));

                if(isTutorial) Tutorial.transform.Find("Tutorial18").gameObject.SetActive(false);

                //Aplicación visual

                //Enunciado
                Problema.GetChild(1).GetComponent<TMP_Text>().text = actualProblem.enunciado;
                //Sprite Apoyo
                Problema.GetChild(2).GetComponent<Image>().sprite = actualProblem.spriteApoyo;
                //BaseDamage
                Problema.GetChild(3).GetComponent<TMP_Text>().text = actualBaseDamage.ToString();
                //Max Time Bonus
                maxTimeBonus = actualProblem.maxTimeBonus;
                //Opciones
                Problema.GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = actualProblem.a;
                Problema.GetChild(5).GetChild(1).GetComponent<TMP_Text>().text = actualProblem.b;
                Problema.GetChild(5).GetChild(2).GetComponent<TMP_Text>().text = actualProblem.c;
                Problema.GetChild(5).GetChild(3).GetComponent<TMP_Text>().text = actualProblem.d;
                Problema.GetChild(5).GetChild(4).GetComponent<TMP_Text>().text = actualProblem.e;
                //Solucion
                solucion = actualProblem.solucion;
                print(actualProblem.solucion);
                //Pista
                Problema.GetChild(6).GetComponent<TMP_Text>().text = actualProblem.pista;



                timer = 0;
                timeBonus = true;
                resuelto = false;
                onProblemStart = false;
                
            }

            if(Problema.GetChild(6).Find("BotonPista").gameObject.activeSelf == false && !pistaUsada) // Pista usada
            {
                pistaUsada = true;
            }

            if(timer >= maxTimeBonus)
            {
                ProblemTimer.text = maxTimeBonus.ToString() + " / " + maxTimeBonus.ToString();
                ProblemTimer.color = Color.red;
                timeBonus = false;
            }
            else
            {
                timer += Time.deltaTime;
                if(timer >= 7.6f || isAnimationFinished) //lo que tarda la animación no se cuenta
                {
                    if(!isAnimationFinished) 
                    {
                        timer = 0;
                        isAnimationFinished = true;
                    }
                    ProblemTimer.text = ((int)timer).ToString() + " / " + maxTimeBonus.ToString();

                    if(isTutorial && tutorialStep5)
                    {
                        tutorialStep5 = false;

                        Tutorial.transform.Find("Tutorial19").gameObject.SetActive(true);
                        Tutorial.transform.Find("FocusJugador17").gameObject.SetActive(true);

                    }
                }
            }

            if(aState.GetComponent<Button>().enabled == false)
            {
                if(solucion != "a")
                {
                    aState.transform.GetComponent<TMP_Text>().color = Color.red;
                    timer = maxTimeBonus;
                }
                else resuelto = true;
            }
            
            if(bState.GetComponent<Button>().enabled == false)
            {
                if(solucion != "b")
                {
                    bState.transform.GetComponent<TMP_Text>().color = Color.red;
                    timer = maxTimeBonus;
                }
                else resuelto = true;
                print(resuelto);
            }

            if(cState.GetComponent<Button>().enabled == false)
            {
                if(solucion != "c")
                {
                    cState.transform.GetComponent<TMP_Text>().color = Color.red;
                    timer = maxTimeBonus;
                }
                else resuelto = true;
            }

            if(dState.GetComponent<Button>().enabled == false)
            {
                if(solucion != "d")
                {
                    dState.transform.GetComponent<TMP_Text>().color = Color.red;
                    timer = maxTimeBonus;
                }
                else resuelto = true;
            }

            if(eState.GetComponent<Button>().enabled == false)
            {
                if(solucion != "e")
                {
                    eState.transform.GetComponent<TMP_Text>().color = Color.red;
                    timer = maxTimeBonus;
                }
                else resuelto = true;
            }

            if(resuelto)
            {
                int damage = actualBaseDamage;
                if(isEnemyTurn)
                {
                    if(timeBonus && !pistaUsada && (aState.GetComponent<Button>().enabled || solucion == "a")
                    && (bState.GetComponent<Button>().enabled || solucion == "b")
                    && (cState.GetComponent<Button>().enabled || solucion == "c")
                    && (dState.GetComponent<Button>().enabled || solucion == "d")
                    && (eState.GetComponent<Button>().enabled || solucion == "e") )
                    damage = 0;
                    else
                    {

                        int contIncorrecto = 0;
                        
                        if(!aState.GetComponent<Button>().enabled && solucion != "a") contIncorrecto +=1;
                        if(!bState.GetComponent<Button>().enabled && solucion != "b") contIncorrecto +=1;
                        if(!cState.GetComponent<Button>().enabled && solucion != "c") contIncorrecto +=1;
                        if(!dState.GetComponent<Button>().enabled && solucion != "d") contIncorrecto +=1;
                        if(!eState.GetComponent<Button>().enabled && solucion != "e") contIncorrecto +=1;
                        

                        if(contIncorrecto == 0) damage = (int)(damage/4); //Resistencia del 75%
                        else if(contIncorrecto == 1) damage = (int)(2 * damage / 3); //Resistencia del 33.333%
                        else if(contIncorrecto == 2) damage = damage; //Resistencia del 0%
                        else if(contIncorrecto == 3) damage = (int)(damage * 1.5f); // Resistencia del -50%
                        else damage = damage * 2; // Resistencia del -100%

                        if(pistaUsada) damage = (int)(damage * 1.2f);
                    }
                    VidaJugador -= damage;
                }
                else
                {
                    
                    if(timeBonus && !pistaUsada && (aState.GetComponent<Button>().enabled || solucion == "a")
                    && (bState.GetComponent<Button>().enabled || solucion == "b")
                    && (cState.GetComponent<Button>().enabled || solucion == "c")
                    && (dState.GetComponent<Button>().enabled || solucion == "d")
                    && (eState.GetComponent<Button>().enabled || solucion == "e") )
                    {
                        damage = (int)(damage * 1.5f);
                    }
                    
                    else
                    {
                        timer = maxTimeBonus;
                        ProblemTimer.text = maxTimeBonus.ToString() + " / " + maxTimeBonus.ToString();
                        ProblemTimer.color = Color.red;
                        timeBonus = false;

                        int contIncorrecto = 0;
                        
                        if(!aState.GetComponent<Button>().enabled && solucion != "a") contIncorrecto +=1;
                        if(!bState.GetComponent<Button>().enabled && solucion != "b") contIncorrecto +=1;
                        if(!cState.GetComponent<Button>().enabled && solucion != "c") contIncorrecto +=1;
                        if(!dState.GetComponent<Button>().enabled && solucion != "d") contIncorrecto +=1;
                        if(!eState.GetComponent<Button>().enabled && solucion != "e") contIncorrecto +=1;

                        if(contIncorrecto == 0) damage = damage; // 100% Potencia
                        else if(contIncorrecto == 1) damage = (int)(damage / 2); // 50% Potencia
                        else if(contIncorrecto == 2) damage = (int)(damage / 4); // 25 % Potencia
                        else if(contIncorrecto == 3) damage = (int)(damage / 10); // Potencia del 10%
                        else damage = 0; // 0% Potencia

                        if(pistaUsada) damage = (int)(damage * 0.8f);
                    }
                    if(enemyHit == 1 && VidaE1 > 0) VidaE1 -= damage;
                    else if(enemyHit == 2 && VidaE2 > 0) VidaE2 -= damage;
                    else if(enemyHit == 3 && VidaE3 > 0) VidaE3 -= damage;

                }

                onProblemStart = true;
                onProblem = false;
                isAnimationFinished = false;

                pistaUsada = false;
                Problema.GetChild(6).Find("BotonPista").gameObject.SetActive(true);
                Problema.GetChild(6).GetComponent<TMP_Text>().enabled = false;

                ProblemTimer.text = "0"+ " / " + "0";


                aState.GetComponent<TMP_Text>().color = Color.black;
                bState.GetComponent<TMP_Text>().color = Color.black;
                cState.GetComponent<TMP_Text>().color = Color.black;
                dState.GetComponent<TMP_Text>().color = Color.black;
                eState.GetComponent<TMP_Text>().color = Color.black;
                ProblemTimer.color = Color.black;

                aState.GetComponent<Button>().enabled = true;
                bState.GetComponent<Button>().enabled = true;
                cState.GetComponent<Button>().enabled = true;
                dState.GetComponent<Button>().enabled = true;
                eState.GetComponent<Button>().enabled = true;


                if(VidaE1 <= 0) VidaE1 = 0;
                if(VidaE2 <= 0) VidaE2 = 0;
                if(VidaE3 <= 0) VidaE3 = 0;

                VidaValueJugador.text = VidaJugador.ToString();
                VidaValueE1.text = VidaE1.ToString();
                VidaValueE2.text = VidaE2.ToString();
                VidaValueE3.text = VidaE3.ToString();
                VidaValueObjectJugador.SetActive(true);
                VidaValueObjectE1.SetActive(true);
                if(numEnemies >= 2) VidaValueObjectE2.SetActive(true);
                if(numEnemies >= 3)VidaValueObjectE3.SetActive(true);

                Problema.GetChild(6).GetComponent<TMP_Text>().enabled = false;
                Problema.GetChild(6).GetChild(0).gameObject.SetActive(true);

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
                    if(enemyHit == 1)
                    {
                    VidaValueE1.transform.GetChild(0).GetComponent<TMP_Text>().text = "-"+ damage.ToString();
                    VidaValueE1.transform.GetChild(0).GetComponent<Animator>().SetBool("visibility", true);
                    VidaValueE1.transform.GetChild(0).GetComponent<Animator>().SetInteger("animRand", animNum);

                    if(VidaE1 <= 0) 
                    {
                        VidaValueObjectE1.GetComponent<TMP_Text>().color = new Color(0.5f,0.5f,0.5f,1f);
                        VidaValueObjectE1.transform.GetChild(0).GetComponent<Image>().color = new Color(0.76f,0.76f,0.76f,1f);
                        VidaValueE1.text = "0";
                        enemigoPosition1.parent.gameObject.SetActive(false);
                        virtualCamera.GetComponent<CinemachineCamera>().Follow = enemigoPosition2;
                        virtualCamera.GetComponent<CinemachineCamera>().LookAt = enemigoPosition2;
                    }
                    }
                    else if(enemyHit == 2)
                    {
                    VidaValueE2.transform.GetChild(0).GetComponent<TMP_Text>().text = "-"+ damage.ToString();
                    VidaValueE2.transform.GetChild(0).GetComponent<Animator>().SetBool("visibility", true);
                    VidaValueE2.transform.GetChild(0).GetComponent<Animator>().SetInteger("animRand", animNum);

                    if(VidaE2 <= 0) 
                    {
                        VidaValueObjectE2.GetComponent<TMP_Text>().color = new Color(0.5f,0.5f,0.5f,1f);
                        VidaValueObjectE2.transform.GetChild(0).GetComponent<Image>().color = new Color(0.76f,0.76f,0.76f,1f);
                        VidaValueE2.text = "0";
                        enemigoPosition2.parent.gameObject.SetActive(false);
                        virtualCamera.GetComponent<CinemachineCamera>().Follow = enemigoPosition3;
                        virtualCamera.GetComponent<CinemachineCamera>().LookAt = enemigoPosition3;
                    }
                    }
                    else if(enemyHit == 3)
                    {
                    VidaValueE3.transform.GetChild(0).GetComponent<TMP_Text>().text = "-"+ damage.ToString();
                    VidaValueE3.transform.GetChild(0).GetComponent<Animator>().SetBool("visibility", true);
                    VidaValueE3.transform.GetChild(0).GetComponent<Animator>().SetInteger("animRand", animNum);

                    if(VidaE3 <= 0) 
                    {
                        VidaValueObjectE3.GetComponent<TMP_Text>().color = new Color(0.5f,0.5f,0.5f,1f);
                        VidaValueObjectE3.transform.GetChild(0).GetComponent<Image>().color = new Color(0.76f,0.76f,0.76f,1f);
                        VidaValueE3.text = "0";
                        enemigoPosition3.parent.gameObject.SetActive(false);
                        virtualCamera.GetComponent<CinemachineCamera>().Follow = avatarPosition;
                        virtualCamera.GetComponent<CinemachineCamera>().LookAt = avatarPosition;
                        StartCoroutine(WaitForUnselect(secondsToUnselect));
                    }
                    }
                    
                }

                

                

            }
            
            
        }

        }
        
        
    }

    void ActualizarFormulaTexto()
    {
        string tempM = m.ToString();
        string tempN = n.ToString();

        if(Mathf.Abs(m - Mathf.Floor(m)) == Mathf.Abs(0.5f)) tempM = (m * 2).ToString() + "/2";
        if(Mathf.Abs(m - Mathf.Floor(m)) == Mathf.Abs(0.25f) || Mathf.Abs(m - Mathf.Floor(m)) == Mathf.Abs(0.75f)) tempM = (m * 4).ToString() + "/4";
        if(Mathf.Abs(n - Mathf.Floor(n)) == Mathf.Abs(0.5f)) tempN = (n * 2).ToString() + "/2";
        if(Mathf.Abs(n - Mathf.Floor(n)) == Mathf.Abs(0.25f) || Mathf.Abs(n - Mathf.Floor(n)) == Mathf.Abs(0.75f)) tempN = (n * 4).ToString() + "/4";

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
        else if(convertTo == "cos") actualFormula.text = "y = " + tempM + "cos(x)" + tempN;
        else if(convertTo == "^2") actualFormula.text = "y = " + tempM + "x^2" + tempN;
    }

    void ActualizarRadioVisual()
    {
        if(!isMoveState && !isEnemyTurn)
        RadioGeneral.GetComponent<Animator>().SetBool("isRadioMov", false);
        else RadioGeneral.GetComponent<Animator>().SetBool("isRadioMov", true);

        if(!isMoveState && isEnemyTurn)
        {
            if(turnRotation == 1) RadioGeneralEnemigo1.GetComponent<Animator>().SetBool("isRadioMov", false);
            else if(turnRotation == 2) RadioGeneralEnemigo2.GetComponent<Animator>().SetBool("isRadioMov", false);
            else if(turnRotation == 3) RadioGeneralEnemigo3.GetComponent<Animator>().SetBool("isRadioMov", false);
        }
        
        else 
        {
            if(turnRotation == 1) RadioGeneralEnemigo1.GetComponent<Animator>().SetBool("isRadioMov", true);
            else if(turnRotation == 2) RadioGeneralEnemigo2.GetComponent<Animator>().SetBool("isRadioMov", true);
            else if(turnRotation == 3) RadioGeneralEnemigo3.GetComponent<Animator>().SetBool("isRadioMov", true);

        }
    }
    void SkipDefeatedEnemies()
    {
        if(VidaE1 <= 0 && turnRotation == 1) 
        {
            turnRotation = 2;
            virtualCamera.GetComponent<CinemachineCamera>().Follow = enemigoPosition2;
            virtualCamera.GetComponent<CinemachineCamera>().LookAt = enemigoPosition2;
        }
        if(VidaE2 <= 0 && turnRotation == 2) 
        {
            turnRotation = 3;
            virtualCamera.GetComponent<CinemachineCamera>().Follow = enemigoPosition3;
            virtualCamera.GetComponent<CinemachineCamera>().LookAt = enemigoPosition3;
        }
        if(VidaE3 <= 0 && turnRotation == 3) 
        {
            isEnemyTurn = false;
            turnRotation = 0;
            virtualCamera.GetComponent<CinemachineCamera>().Follow = avatarPosition;
            virtualCamera.GetComponent<CinemachineCamera>().LookAt = avatarPosition;
            StartCoroutine(WaitForUnselect(secondsToUnselect));
        }
    }
    void Shuffle(List<Problems> array)
    {
        int n = array.Count;
        while (n > 1) 
        {
            int k = Random.Range(0,n--);
            Problems temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
    void ShuffleBaraja(Cards[] array)
    {
        int n = array.Length;
        while (n > 1) 
        {
            int k = Random.Range(0,n--);
            Cards temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
    IEnumerator WaitForUnselect(int n)
    {
        
        yield return new WaitForSeconds(n);
        
        virtualCamera.GetComponent<CinemachineCamera>().Follow = null;
        virtualCamera.GetComponent<CinemachineCamera>().LookAt = null;
        
        
    }
    IEnumerator WaitForEmitting(int n)
    {
        
        yield return new WaitForSeconds(n);
        
        AllEmit();
        
        
    }
    void AllEmit()
    {
        
            posibilityLeftPos.GetComponent<TrailRenderer>().emitting = true;
            posibilityRightPos.GetComponent<TrailRenderer>().emitting = true;
        
            posibilityLeftPosEnemigo1.GetComponent<TrailRenderer>().emitting = true;
            posibilityRightPosEnemigo1.GetComponent<TrailRenderer>().emitting = true;
        
            posibilityLeftPosEnemigo2.GetComponent<TrailRenderer>().emitting = true;
            posibilityRightPosEnemigo2.GetComponent<TrailRenderer>().emitting = true;
        
            posibilityLeftPosEnemigo3.GetComponent<TrailRenderer>().emitting = true;
            posibilityRightPosEnemigo3.GetComponent<TrailRenderer>().emitting = true;
        
        
        
    }
}

