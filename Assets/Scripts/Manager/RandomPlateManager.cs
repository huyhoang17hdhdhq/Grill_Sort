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

        bool hasFree = false;

        foreach (var grill in grills)
        {
            int plateCount = Random.Range(3  , 5 );

            if (plateCount < 3)
                hasFree = true;

            grillPlateCounts.Add(grill, plateCount);
            TotalPlate += plateCount;
        }

       
        if (!hasFree && grills.Count > 1 )
        {
            grillPlateCounts[grills[0]] = 2;
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