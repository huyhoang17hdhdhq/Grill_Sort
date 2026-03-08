using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FoodTypeData
{
    public FoodType type;  
    public int score;      
    public Sprite image;   
}
public class FoodDatabase : MonoBehaviour
{
    public static FoodDatabase Instance;

    public List<FoodTypeData> foods;

    private Dictionary<FoodType, FoodTypeData> foodDict;

    void Awake()
    {
        Instance = this;

        foodDict = new Dictionary<FoodType, FoodTypeData>();

        foreach (var f in foods)
        {
            foodDict[f.type] = f;
        }
    }

    public int GetScore(FoodType type)
    {
        if (foodDict.TryGetValue(type, out var data))
            return data.score;

        return 0;
    }

    public Sprite GetImage(FoodType type)
    {
        if (foodDict.TryGetValue(type, out var data))
            return data.image;

        return null;
    }
}