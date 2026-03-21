using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Spawn Points")]
    [SerializeField] private List<Transform> spawnPoints;

    [Header("Grill Prefab")]
    [SerializeField] private GameObject grillPrefab;

    [Header("Grill Count Per Level")]
    [SerializeField] private List<int> grillsPerLevel;

    [SerializeField] private Transform grillContainer;

    private List<GameObject> activeGrills = new List<GameObject>();
    private List<Grill> grillScripts = new List<Grill>();

    private int currentLevel;

    void Awake()
    {
        Instance = this;
        currentLevel = GameData.GetInt(GameData.Key.Level, 1);
    }

    void Start()
    {
        SpawnLevel();
    }

    void SpawnLevel()
    {
        if (spawnPoints.Count == 0 || grillsPerLevel.Count == 0)
            return;

        grillScripts.Clear();
        activeGrills.Clear();

        int levelIndex = Mathf.Clamp(currentLevel - 1, 0, grillsPerLevel.Count - 1);
        int grillCount = Mathf.Min(grillsPerLevel[levelIndex], spawnPoints.Count);

        // 🔹 Spawn grill
        for (int i = 0; i < grillCount; i++)
        {
            Transform point = spawnPoints[i];

            GameObject grillObj = Instantiate(grillPrefab, grillContainer);
            grillObj.transform.position = point.position;
            grillObj.transform.rotation = point.rotation;

            Grill grill = grillObj.GetComponent<Grill>();

            if (grill != null)
            {
                grillScripts.Add(grill); // chưa init
            }

            activeGrills.Add(grillObj);
        }

        // 🔹 1. Random số plate cho từng grill
        RandomPlateManager.Instance.Generate(grillScripts);

        // 🔥 2.LẤY ĐÚNG TOÀN BỘ PLATE ĐANG ACTIVE
        
        //PlateFood.Instance.GenerateAllFood(RandomPlateManager.Instance.TotalPlate);
        // 🔹 5. Init grill (spawn lên bếp)
        foreach (var grill in grillScripts)
        {
            grill.Init();
        }
    }

    public void ClearLevel()
    {
        foreach (var grill in activeGrills)
        {
            Destroy(grill);
        }

        activeGrills.Clear();
        grillScripts.Clear();
    }

    public void RemoveGrill(Grill grill)
    {
        if (grill == null) return;

        grillScripts.Remove(grill);
        activeGrills.Remove(grill.gameObject);

        if (activeGrills.Count == 0)
        {
            Win();
        }
    }

    public void NextLevel()
    {
        currentLevel++;

        GameData.SetInt(GameData.Key.Level, currentLevel);

        ClearLevel();
        SpawnLevel();
    }

    void Win()
    {
        Debug.Log("YOU WIN");
    }
}