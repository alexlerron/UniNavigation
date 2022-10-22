using UnityEngine;

public enum TypeName { Cabinet, Food, WC, Library, OfficeCab, Wardrobe }

public class CabData : MonoBehaviour
{
    public TypeName typeName;
    public bool isCabActive;
    public int cabNumber;
}
