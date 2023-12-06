namespace SearchAlgorithms;

public interface ISearch
{
    public bool IsOver();
    public IEnumerable<ISearch> GetAllPossibleStates();
    public string ToString();
}