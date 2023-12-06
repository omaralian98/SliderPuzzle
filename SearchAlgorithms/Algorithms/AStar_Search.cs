using SearchAlgorithms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

public class AStarSearch<TSearch> where TSearch : ISearch
{
    public delegate int MyFunction(TSearch game);
    /// <summary>
    /// This function finds the shortest path for any game state using A*.
    /// </summary>
    /// <param name="initial">The initial state of the game</param>
    /// <param name="heuristic">the heuristic to be used</param>
    /// <returns>
    /// Tuple contains 3 items.
    /// <br></br>
    /// List of TSearch containing the moves of the shortest path.
    /// <br></br>
    /// The number of discovered nodes.
    /// <br></br>
    /// The number of visited nodes.
    /// </returns>
    public (List<TSearch>, long, long) FindPath(TSearch initial, MyFunction heuristic)
    {
        long DiscoveredNodes = 1;
        long VisitedNodes = 1;
        SortedList<int, (TSearch, List<TSearch>)> list = new(new DuplicateKeyComparer<int>());
        HashSet<(string, int)> visited = [];

        list.Add(0, new(initial, []));
        visited.Add((initial.ToString(), 0));

        while (list.Count > 0)
        {
            VisitedNodes++;
            var (current, path) = list.GetValueAtIndex(0);
            Console.WriteLine(list.GetKeyAtIndex(0));
            if (current.IsOver()) return (path, DiscoveredNodes, VisitedNodes); // Return the path when the solution is found
            list.RemoveAt(0);

            foreach (var next in current.GetAllPossibleStates())
            {
                string newstr = next.ToString();
                int newcost = heuristic((TSearch)next) + path.Count + 1;
                if (!visited.Contains((newstr, newcost)))
                {
                    visited.Add((newstr, newcost));
                    List<TSearch> newPath = [.. path, (TSearch)next];
                    list.Add(newcost, ((TSearch, List<TSearch>))(next, newPath));
                    DiscoveredNodes++;
                }
            }
        }
        return (new List<TSearch>(), DiscoveredNodes, VisitedNodes); // Return empty List if no solution is found
    }
}

/// <summary>
/// Comparer for comparing two keys, handling equality as being greater
/// Use this Comparer e.g. with SortedLists or SortedDictionaries, that don't allow duplicate keys
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class DuplicateKeyComparer<TKey> : IComparer<TKey> where TKey : IComparable
{
    #region IComparer<TKey> Members
    public int Compare(TKey x, TKey y)
    {
        int result = x.CompareTo(y);

        if (result == 0)
            return -1; // Handle equality as being greater. Note: this will break Remove(key) or
        else          // IndexOfKey(key) since the comparer never returns 0 to signal key equality
            return result;
    }
    #endregion
}