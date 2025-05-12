using UnityEngine;

[CreateAssetMenu(fileName = "Cards", menuName = "Scriptable Objects/Cards")]
public class Cards : ScriptableObject
{
    public string name;
    
    public string type;

    public float value;

    public Sprite sprite;

    public string convertTo;
    
}
