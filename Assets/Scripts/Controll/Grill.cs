using UnityEngine;
using System.Collections.Generic;

public class Grill : MonoBehaviour
{
    public Slot[] slots;

    public Plate plate;

    [SerializeField] private List<GameObject> plates;

    public void Init()
    {
        CheckEmpty();

    }

    public void CheckMatch()
    {
        if (slots.Length < 3) return;

        var first = slots[0].currentFood;
        if (first == null) return;

        for (int i = 1; i < 3; i++)
            if (slots[i].currentFood?.foodType != first.foodType)
                return;

        FoodDrag[] foods =
        {
        slots[0].currentFood,
        slots[1].currentFood,
        slots[2].currentFood
    };

        foreach (var f in foods)
            f.CurrentSlot.ClearFood();

        int finished = 0;

        void OnFinish()
        {
            if (++finished < foods.Length) return;

            foreach (var f in foods)
                ObjectPool.Instance.Despawn(f.gameObject);

            GameEvents.OnGrillMatch?.Invoke(this);
            CheckEmpty();
        }

        foreach (var f in foods)
            f.GetComponent<FoodTween>()
             .FlyTo(CollectPoint.Instance.position, OnFinish);
    }

    public void CheckEmpty()
    {
        if (GameEvents.IsDraggingFood)
            return;
        foreach (Slot slot in slots)
        {
            if (slot.currentFood != null)
                return;
        }

        
        SpawnFromPlate();
    }

    void SpawnFromPlate()
    {
        if (plate == null) return;

        
        FoodType[] foods = plate.GetFoods();
        if (foods == null || foods.Length == 0) return;

        int count = Mathf.Min(foods.Length, slots.Length);

        for (int i = 0; i < count; i++)
        {
            GameObject food = ObjectPool.Instance.Spawn(
                foods[i],
                slots[i].transform.position,
                Quaternion.identity
            );

            food.transform.SetParent(slots[i].transform);

            FoodDrag drag = food.GetComponent<FoodDrag>();

            slots[i].SetFood(drag);
            drag.CurrentSlot = slots[i];
        }

        
        PlateFood.Instance.SpawnFoodForPlate(plate);
    }

    public void SetPlateCount(int count)
    {
        Debug.Log("SetPlateCount: " + count);
        for (int i = 0; i < plates.Count; i++)
        {
            plates[i].SetActive(i < count);
        }
    }

    public int GetActivePlateCount()
    {
        return RandomPlateManager.Instance.GetPlateCount(this);
    }
}