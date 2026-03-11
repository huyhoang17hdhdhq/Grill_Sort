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
        Debug.Log("SpawnLevel called frame: " + Time.frameCount);
        if (spawnPoints.Count == 0 || grillsPerLevel.Count == 0)
            return;

        int levelIndex = Mathf.Clamp(currentLevel - 1, 0, grillsPerLevel.Count - 1);
        int grillCount = Mathf.Min(grillsPerLevel[levelIndex], spawnPoints.Count);

        for (int i = 0; i < grillCount; i++)
        {
            Transform point = spawnPoints[i];

            GameObject grillObj = Instantiate(grillPrefab, grillContainer);

            grillObj.transform.position = point.position;
            grillObj.transform.rotation = point.rotation;

            Grill grill = grillObj.GetComponent<Grill>();
            if (grill != null)
                grill.Init();

            activeGrills.Add(grillObj);
        }
    }

    public void ClearLevel()
    {
        foreach (var grill in activeGrills)
        {
            Destroy(grill);
        }

        activeGrills.Clear();
    }

    public void NextLevel()
    {
        currentLevel++;

        GameData.SetInt(GameData.Key.Level, currentLevel);

        ClearLevel();
        SpawnLevel();
    }
}