public class CellPathOptions
{
    
    public int g;
    public int h;
    public int f;
    public CellModel cameFromNode;
    public int heapIndex = Constants.Numerical.NOT_IN_HEAP;

    public void ResetValues()
    {
        g = int.MaxValue;
        CalculateF();
        cameFromNode = null;
    }

    public void CalculateF()
    {
        f = g + h;
    }

}