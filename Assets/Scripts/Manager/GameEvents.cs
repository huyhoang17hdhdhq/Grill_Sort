using System;

public static class GameEvents
{
    public static Action<Grill> OnGrillEmpty;

    public static void GrillEmpty(Grill grill)
    {
        OnGrillEmpty?.Invoke(grill);
    }
}