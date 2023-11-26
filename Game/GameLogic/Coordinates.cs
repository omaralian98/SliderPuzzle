namespace Game;

public struct Coordinates(int a, int b)
{
    public int x = a;
    public int y = b;
    public int Distance(Coordinates b) => (int)Math.Sqrt(Math.Pow(b.x - x, 2) + Math.Pow(b.y - y, 2));
    public static bool operator ==(Coordinates a, Coordinates b) => a.x == b.x && a.y == b.y;
    public static bool operator !=(Coordinates a, Coordinates b) => !(a == b);
    public override string ToString() => $"({x}, {y})";
}