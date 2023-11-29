namespace SearchAlgorithms;

public class Search 
{
    public IEnumerable<Search> GetAllPossibleStates()
    {
        return Enumerable.Empty<Search>();
    }
    public bool IsOver()
    {
        return false;
    }
}