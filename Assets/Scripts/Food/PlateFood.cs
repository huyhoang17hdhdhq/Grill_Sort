using System.Collections.Generic;
using UnityEngine;

public class PlateFood : MonoBehaviour
{
    public static PlateFood Instance;

    private List<Plate> plates = new List<Plate>();

    private Queue<FoodType> globalFoods = new Queue<FoodType>();

    private Dictionary<Plate, Queue<FoodType>> plateQueues = new Dictionary<Plate, Queue<FoodType>>();

    void Awake()
    {
        Instance = this;
    }
    void OnEnable()
    {
        GameEvents.OnGrillMatch += HandleGrillMatch;
    }

    void OnDisable()
    {
        GameEvents.OnGrillMatch -= HandleGrillMatch;
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

    void HandleGrillMatch(Grill grill)
    {
        Plate plate = grill.plate;

        if (plate == null) return;

        
    }

    public void SpawnFoodForPlate(Plate plate)
    {
        if (!plateQueues.ContainsKey(plate)) return;

        var queue = plateQueues[plate];
        if (queue.Count == 0) return;

        int maxSlot = Mathf.Min(plate.SlotCount, 3);

        // ❗ tránh full
        int randomSlot = Random.Range(1, maxSlot);

        FoodType[] foods = new FoodType[randomSlot];

        for (int i = 0; i < randomSlot; i++)
        {
            if (queue.Count == 0) break;

            foods[i] = queue.Dequeue();
        }

        plate.SetFoods(foods);
    }
    public void GenerateFoodForAllPlates(List<Plate> plates, int totalPlate)
    {
        plateQueues.Clear();

        List<FoodType> solution = new List<FoodType>();

        int enumLength = System.Enum.GetValues(typeof(FoodType)).Length;

        // 👉 tạo bộ 3 đảm bảo match
        for (int i = 0; i < totalPlate; i++)
        {
            FoodType type = (FoodType)(i % enumLength);

            solution.Add(type);
            solution.Add(type);
            solution.Add(type);
        }

        // 👉 shuffle
        for (int i = 0; i < solution.Count; i++)
        {
            int rand = Random.Range(i, solution.Count);
            (solution[i], solution[rand]) = (solution[rand], solution[i]);
        }

        // 👉 init queue cho từng plate
        foreach (var plate in plates)
        {
            plateQueues[plate] = new Queue<FoodType>();
        }

        // 👉 phân phối food (tránh dồn vào 1 plate)
        while (solution.Count > 0)
        {
            FoodType type = solution[0];
            solution.RemoveAt(0);

            Plate p1 = plates[Random.Range(0, plates.Count)];
            Plate p2 = plates[Random.Range(0, plates.Count)];

            while (p2 == p1)
                p2 = plates[Random.Range(0, plates.Count)];

            plateQueues[p1].Enqueue(type);
            plateQueues[p2].Enqueue(type);

            if (solution.Count > 0)
            {
                plateQueues[p1].Enqueue(type);
                solution.RemoveAt(0);
            }
        }
    }
    public void RefillAllPlates()
    {
        foreach (var plate in plates)
        {
            SpawnFoodForPlate(plate);
        }
    }
}