using System;

public static class GameEvents
{
    public static Action<Grill> OnGrillEmpty;

    public static Action<Grill> OnGrillMatch;

    public static bool IsDraggingFood = false;

    public static void GrillEmpty(Grill grill)
    {
        OnGrillEmpty?.Invoke(grill);
    }
}