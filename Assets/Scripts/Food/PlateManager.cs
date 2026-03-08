using System.Collections.Generic;
using UnityEngine;

public class PlateManager : MonoBehaviour
{
    public static PlateManager Instance;

    private List<Plate> plates = new List<Plate>();

    void Awake()
    {
        Instance = this;
    }

    public void RegisterPlate(Plate plate)
    {
        plates.Add(plate);
        SpawnFoodForPlate(plate);
    }

    public void UnregisterPlate(Plate plate)
    {
        plates.Remove(plate);
    }

    void SpawnFoodForPlate(Plate plate)
    {
        int randomSlot = Random.Range(1, plate.SlotCount + 1);

        FoodType[] foods = new FoodType[randomSlot];

        for (int i = 0; i < randomSlot; i++)
        {
            foods[i] = (FoodType)Random.Range(
                0,
                System.Enum.GetValues(typeof(FoodType)).Length
            );
        }

        plate.SetFoods(foods);
    }
}