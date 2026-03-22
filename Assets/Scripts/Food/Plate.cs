using UnityEngine;

public class Plate : MonoBehaviour
{
    [Header("Parent chứa các Slot")]
    [SerializeField] private Transform slotParent;

    private SpriteRenderer[] slots;
    private FoodType[] currentFoods;

    public int SlotCount => slots.Length;

    void Awake()
    {
       
        slots = slotParent.GetComponentsInChildren<SpriteRenderer>();
    }

    void OnEnable()
    {
        if (PlateFood.Instance != null)
        {
            PlateFood.Instance.RegisterPlate(this);
        }

        GameEvents.OnGrillEmpty += HandleGrillEmpty;
    }

    void OnDisable()
    {
        if (PlateFood.Instance != null)
        {
            PlateFood.Instance.UnregisterPlate(this);
        }

        GameEvents.OnGrillEmpty -= HandleGrillEmpty;
    }

    public void SetFoods(FoodType[] foods)
    {
        currentFoods = foods;

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < foods.Length)
                slots[i].sprite = FoodDatabase.Instance.GetImage(foods[i]);
            else
                slots[i].sprite = null;
        }
    }

    public FoodType[] GetFoods()
    {
        return currentFoods;
    }

    void HandleGrillEmpty(Grill grill)
    {
        if (currentFoods == null) return;

        int count = Mathf.Min(currentFoods.Length, grill.slots.Length);

        for (int i = 0; i < count; i++)
        {
            GameObject food = ObjectPool.Instance.Spawn(currentFoods[i], transform.position, Quaternion.identity);

            food.transform.localScale = Vector3.zero;

            FoodDrag drag = food.GetComponent<FoodDrag>();

            grill.slots[i].SetFood(drag);
            drag.CurrentSlot = grill.slots[i];

            food.transform.SetParent(grill.slots[i].transform);

            food.GetComponent<FoodTween>()
                .MoveToSlot(grill.slots[i].transform);
        }
    }
}