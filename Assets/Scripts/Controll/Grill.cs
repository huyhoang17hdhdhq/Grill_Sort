using UnityEngine;

public class Grill : MonoBehaviour
{
    public Slot[] slots;

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
            Destroy(slots[0].currentFood.gameObject);
            Destroy(slots[1].currentFood.gameObject);
            Destroy(slots[2].currentFood.gameObject);

            slots[0].ClearFood();
            slots[1].ClearFood();
            slots[2].ClearFood();
        }
    }
}