using UnityEngine;


public enum FoodType
{
    dumplings = 0,
    gingerbread = 1,
    tamarindCake = 2,
    bread = 3,
    eggTart = 4,
    eggTartVip = 5,
    abalone = 6,
    cabbage = 7
}

public class FoodDrag : MonoBehaviour
{
    public FoodType foodType;

    public Slot CurrentSlot { get; set; }
    private Slot targetSlot;

    private Vector3 offset;

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();

        if (CurrentSlot != null)
        {
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
        if (targetSlot != null && targetSlot.IsEmpty())
        {
            targetSlot.SetFood(this);
            CurrentSlot = targetSlot;

            Debug.Log(targetSlot);
            Debug.Log(targetSlot.GetComponentInParent<Grill>());

            Grill grill = targetSlot.GetComponentInParent<Grill>();
            grill.CheckMatch();
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