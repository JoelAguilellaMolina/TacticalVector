using UnityEngine;
using System.Collections;

public class GameManagerCombat : MonoBehaviour
{
    public Transform avatarPosition;

    public Transform posibilityLeftPos;
    public Transform posibilityRightPos;

    public bool isMoving;
    public bool isMovingA;
    public bool isMovingB;


    public Transform[] PuntosEnFuncion = new Transform[21];
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
    public (float,float)[] arrayPositions;

    public const float VELOCIDAD = 0.01f;

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
        isMoving = true;
        isMovingA = true;
        isMovingB = true;

        cantidadPuntos = 21;
        arrayPositions = new (float,float)[cantidadPuntos];
        timer = 0;
        r = 10f;


        m = 0.02f;
        n = -1f;

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
        

        if(isMoving)
        {
            
            x = x + VELOCIDAD;
            y = 0.5f * x * x;

            avatarPosition.position = new Vector3(x,0, y);
            
        }
        if(isMovingA)
        {
            x1 = x1 + VELOCIDAD;
            y1 = 0.5f * x1 * x1;

            posibilityLeftPos.position = new Vector3(x1,VELOCIDAD, y1);
        }
        if(isMovingB)
        {
            x2 = x2 - VELOCIDAD;
            y2 = 0.5f * x2 * x2;

            posibilityRightPos.position = new Vector3(x2,VELOCIDAD, y2);
        }
        timer += Time.deltaTime;
        
        
    }
}
