using UnityEngine;

public class Slot : MonoBehaviour
{
    public Transform skewerPosition;
    public Collider2D slotCollider;

    public FoodDrag currentFood;

    void Awake()
    {
        slotCollider = GetComponent<Collider2D>();
    }

    public bool IsEmpty()
    {
        return currentFood == null;
    }

    public void SetFood(FoodDrag food)
    {
        currentFood = food;

        food.CurrentSlot = this;

        food.transform.SetParent(transform);
        food.transform.position = skewerPosition.position;

        slotCollider.enabled = false;
    }

    public void ClearFood()
    {
        currentFood = null;
        slotCollider.enabled = true;
    }
}