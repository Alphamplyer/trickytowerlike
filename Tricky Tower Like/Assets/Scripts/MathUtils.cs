public class MathUtils
{
    public static int FloorTo(float value, int numberUnderZero, int numberAboveZero)
    {
        return value < 0 ? numberUnderZero : value > 0 ? numberAboveZero : 0;
    }

    public static float IncreaseTimer(float value, float max, float factor)
    {
        value += factor;
        return value < max ? value : max;
    }
    
    public static float DecreaseTimer(float value, float min, float factor)
    {
        value -= factor;
        return value > min ? value : min;
    }
}