namespace Game;

public struct Cell(Coordinates co, int con)
{
    public Coordinates coordinates = co;
    public int content = con;
    public void MoveTo(Coordinates newCo) => coordinates = newCo;
}