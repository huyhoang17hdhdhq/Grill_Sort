using System.Collections.Generic;
using UnityEngine;

public class RandomPlateManager : MonoBehaviour
{
    public static RandomPlateManager Instance;

    private Dictionary<Grill, int> grillPlateCounts = new Dictionary<Grill, int>();

    public int TotalPlate { get; private set; }

    void Awake()
    {
        Instance = this;
    }
   
    

    public void Generate(List<Grill> grills)
    {
        grillPlateCounts.Clear();
        TotalPlate = 0;

        foreach (var grill in grills)
        {
            int plateCount = Random.Range(2, 5); 

            grillPlateCounts.Add(grill, plateCount);

            TotalPlate += plateCount;

            grill.SetPlateCount(plateCount);
        }

        Debug.Log("Total Plate: " + TotalPlate);
    }

    public int GetPlateCount(Grill grill)
    {
        if (grillPlateCounts.TryGetValue(grill, out int count))
            return count;

        return 0;
    }
}