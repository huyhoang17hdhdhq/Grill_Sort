using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    [Header("Pool Settings")]
    [SerializeField] private PoolItem[] poolItems;

    private Dictionary<FoodType, Queue<GameObject>> poolDictionary = new Dictionary<FoodType, Queue<GameObject>>();

    [System.Serializable]
    public class PoolItem
    {
        public FoodType type;
        public GameObject prefab;
        public int initialSize = 10;
        public bool expandIfNeeded = true;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var item in poolItems)
        {
            if (item.prefab == null) continue;

            Queue<GameObject> pool = new Queue<GameObject>();

            for (int i = 0; i < item.initialSize; i++)
            {
                GameObject obj = Instantiate(item.prefab, transform);
                obj.SetActive(false);
                obj.name = item.prefab.name;

                pool.Enqueue(obj);
            }

            poolDictionary[item.type] = pool;
        }
    }

    public GameObject Spawn(FoodType type, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogWarning($"Pool for {type} not found!");
            return null;
        }

        Queue<GameObject> pool = poolDictionary[type];

        GameObject obj;

        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            PoolItem item = GetPoolItem(type);

            if (item != null && item.expandIfNeeded)
            {
                obj = Instantiate(item.prefab, transform);
                obj.name = item.prefab.name;
            }
            else
            {
                Debug.LogWarning($"Pool for {type} is empty!");
                return null;
            }
        }

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        return obj;
    }

    public void Despawn(GameObject obj)
    {
        if (obj == null) return;

        obj.SetActive(false);
        obj.transform.SetParent(transform);

        FoodDrag food = obj.GetComponent<FoodDrag>();

        if (food != null && poolDictionary.ContainsKey(food.foodType))
        {
            poolDictionary[food.foodType].Enqueue(obj);
        }
        else
        {
            Destroy(obj);
        }
    }

    private PoolItem GetPoolItem(FoodType type)
    {
        foreach (var item in poolItems)
        {
            if (item.type == type)
                return item;
        }

        return null;
    }
}