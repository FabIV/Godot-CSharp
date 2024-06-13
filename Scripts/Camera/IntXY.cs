namespace Pixelator;

public class IntXY
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public IntXY(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void SetX(int x)
    {
        X = x;
    }
    public void SetY(int y)
    {
        Y = y;
    }
    
    public void SetXY(int x,int y)
    {
        X = x;
        Y = y;
    }


}