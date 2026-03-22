using UnityEngine;


public class FoodDrag : MonoBehaviour
{
    public FoodType foodType;

    public Slot CurrentSlot { get; set; }

    private Slot targetSlot;

    private Grill previousGrill;

    private Vector3 offset;

    private Vector3 originalScale;
    [SerializeField] private float dragScale = 1.2f;

    void OnMouseDown()
    {
        GameEvents.IsDraggingFood = true;

        offset = transform.position - GetMouseWorldPos();

    
        originalScale = transform.localScale;

        transform.localScale = originalScale * dragScale;

        if (CurrentSlot != null)
        {
            previousGrill = CurrentSlot.GetComponentInParent<Grill>();

            CurrentSlot.ClearFood();
            CurrentSlot = null;
        }

        transform.SetParent(null);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        GameEvents.IsDraggingFood = false;

       
        transform.localScale = originalScale;

        if (targetSlot != null && targetSlot.IsEmpty())
        {
            targetSlot.SetFood(this);
            CurrentSlot = targetSlot;

            Grill grill = targetSlot.GetComponentInParent<Grill>();

            if (grill != null)
                grill.CheckMatch();
        }

        if (previousGrill != null)
        {
            previousGrill.CheckEmpty();
            previousGrill = null;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Slot"))
        {
            targetSlot = col.GetComponent<Slot>();
        }
    }

   

    Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10f;
        return Camera.main.ScreenToWorldPoint(pos);
    }
}