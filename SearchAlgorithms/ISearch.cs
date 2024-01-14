namespace SearchAlgorithms;

public interface ISearch
{
    public ISearch? Parent { get; set; }
    public bool IsOver();
    public IEnumerable<ISearch> GetAllPossibleStates();
    public string ToString();
}