using Game;

namespace SearchAlgorithms;

public class Breadth_First_Search
{
    /// <summary>
    /// This function find the shortest path of any slider puzzle
    /// </summary>
    /// <param name="initial">The initial state</param>
    /// <returns>
    /// Tuple contains 3 items.
    /// <br></br>
    /// List of SliderPuzzleGame containing the moves of the shortest path.
    /// <br></br>
    /// The number of discovered nodes.
    /// <br></br>
    /// The number of visited nodes.
    /// </returns>
    public static (List<SliderPuzzleGame>, long, long) FindPath(SliderPuzzleGame initial)
    {
        long DiscoveredNodes = 1;
        long VisitedNodes = 1;
        Queue<(SliderPuzzleGame, List<SliderPuzzleGame>)> queue = [];
        HashSet<string> visited = [];

        queue.Enqueue((initial, [initial]));
        visited.Add(initial.ToString());
        while (queue.Count > 0)
        {
            VisitedNodes++;
            var (current, path) = queue.Dequeue();
            if (current.IsOver()) return (path, DiscoveredNodes, VisitedNodes); // Return the path when the solution is found
            foreach (var next in current.GetAllPossibleStates())
            {
                if (!visited.Contains(next.ToString()))
                {
                    visited.Add(next.ToString());
                    //Copies the previous path and adds next to the new path
                    List<SliderPuzzleGame> newPath = [.. path, next];
                    queue.Enqueue((next, newPath));
                    DiscoveredNodes++;
                }
            }
        }
        return ([], DiscoveredNodes, VisitedNodes); //Return empty List if no solution is found
    }
}



