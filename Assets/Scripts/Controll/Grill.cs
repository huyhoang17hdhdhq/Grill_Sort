using UnityEngine;
using System.Collections.Generic;

public class Grill : MonoBehaviour
{
    public Slot[] slots;

    public Plate plate;

    [SerializeField] private List<GameObject> plates;

    private bool hasSpawnedAfterReduce = false;



    public void Init()
    {
        int count = RandomPlateManager.Instance.GetPlateCount(this);

        SetPlateCount(count);

       

        CheckEmpty();
    }
    void Start ()
    {
        
    }

    public void CheckMatch()
    {
        if (slots.Length < 3) return;

        var first = slots[0].currentFood;
        if (first == null) return;

        for (int i = 1; i < 3; i++)
            if (slots[i].currentFood?.foodType != first.foodType)
                return;

        FoodDrag[] foods = { slots[0].currentFood, slots[1].currentFood, slots[2].currentFood };

        foreach (var f in foods)
            f.CurrentSlot.ClearFood();

        int finished = 0;

        void OnFinish()
        {
            if (++finished < foods.Length) return;

            foreach (var f in foods)
                ObjectPool.Instance.Despawn(f.gameObject);

            GameEvents.OnGrillMatch?.Invoke(this);

            hasSpawnedAfterReduce = false; // 🔥 reset

            ReducePlate();
            CheckEmpty();
        }

        foreach (var f in foods) f.GetComponent<FoodTween>().FlyTo(CollectPoint.Instance.position, OnFinish);

    }

    public void CheckEmpty()
    {
        if (GameEvents.IsDraggingFood)
            return;

        if (!IsAllSlotEmpty())
            return;

        bool noPlateLeft = ReducePlate();

        // 🔥 nếu vừa hết plate → cho spawn 1 lần cuối
        if (noPlateLeft && !hasSpawnedAfterReduce)
        {
            hasSpawnedAfterReduce = true;

            SpawnFromPlate(); // spawn lần cuối
            return;
        }

        // 🔥 sau lần spawn cuối → mới destroy
        if (noPlateLeft && hasSpawnedAfterReduce)
        {
            DestroyGrill();
            return;
        }

        // 🔥 còn plate → spawn bình thường
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

            food.GetComponentInChildren<Animator>(true)?.SetTrigger("Smoke");

            PlateFood.Instance.SpawnFoodForPlate(plate);

        }


        // PlateFood.Instance.SpawnFoodForPlate(plate);
        CheckMatch();


    }

    public void SetPlateCount(int count)
    {
        for (int i = 0; i < plates.Count; i++)
        {
            plates[i].SetActive(i < count);
        }
    }

    bool ReducePlate()
    {
        for (int i = plates.Count - 1; i >= 0; i--)
        {
            if (plates[i].activeSelf)
            {
                plates[i].SetActive(false);
                break;
            }
        }

        // check còn plate không
        foreach (var p in plates)
        {
            if (p.activeSelf)
                return false;
        }

        // hết plate nhưng CHƯA destroy
        plate.gameObject.SetActive(false);
        return true;
    }
    bool IsAllSlotEmpty()
    {
        foreach (var slot in slots)
        {
            if (slot.currentFood != null)
                return false;
        }
        return true;
    }

    //public void ReducePlateFromEmpty()
    //{
    //    ReducePlate(); // dùng lại logic cũ
    //}
    void DestroyGrill()
    {
        LevelManager.Instance.RemoveGrill(this);

        Destroy(gameObject);
    }

    public int GetActivePlateCount()
    {
        return RandomPlateManager.Instance.GetPlateCount(this);
    }
}