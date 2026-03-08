using UnityEngine;

public class Grill : MonoBehaviour
{
    public Slot[] slots;

    public Plate plate;  

    void Start()
    {
        CheckEmpty();
    }

    public void CheckMatch()
    {
        if (slots.Length < 3) return;

        if (slots[0].currentFood == null ||
            slots[1].currentFood == null ||
            slots[2].currentFood == null)
            return;

        FoodType type = slots[0].currentFood.foodType;

        if (slots[1].currentFood.foodType == type &&
            slots[2].currentFood.foodType == type)
        {
            ObjectPool.Instance.Despawn(slots[0].currentFood.gameObject);
            ObjectPool.Instance.Despawn(slots[1].currentFood.gameObject);
            ObjectPool.Instance.Despawn(slots[2].currentFood.gameObject);

            slots[0].ClearFood();
            slots[1].ClearFood();
            slots[2].ClearFood();

            CheckEmpty();
        }
    }

    public void CheckEmpty()
    {
        
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
    }
}