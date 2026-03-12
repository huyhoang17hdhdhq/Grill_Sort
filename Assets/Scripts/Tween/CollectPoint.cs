using UnityEngine;

public class CollectPoint : MonoBehaviour
{
    public static Transform Instance;

    void Awake()
    {
        Instance = transform;
    }
}