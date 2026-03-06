using UnityEngine;
public enum FoodType
{
    dumplings,
    gingerbread,
    tamarindCake,
    bread,
    eggTart,
    eggTartVip,
    abalone,
    cabbage
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

    //void OnTriggerExit2D(Collider2D col)
    //{
    //    if (col.CompareTag("Slot"))
    //    {
    //        if (targetSlot == col.GetComponent<Slot>())
    //            targetSlot = null;
    //    }
    //}

    Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10f;
        return Camera.main.ScreenToWorldPoint(pos);
    }
}