namespace Game;

public struct Cell(Coordinates co, int con)
{
    public Coordinates coordinates = co;
    public int content = con;
    public void MoveTo(Coordinates newCo) => coordinates = newCo;
    public override string ToString() => content.ToString();
    public static bool operator ==(Cell a, Cell b) => a.content == b.content;
    public static bool operator !=(Cell a, Cell b) => !(a == b);

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        else return (Cell)obj == this;
    }
}