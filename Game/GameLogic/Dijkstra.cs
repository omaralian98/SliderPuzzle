namespace Game;

public class Dijkstra(int V)
{
    public int minDistance(int[] dist, bool[] sptSet)
    {
        // Initialize min value
        int min = int.MaxValue;
        int min_index = -1;

        for (int v = 0; v < V; v++)
            if (sptSet[v] == false && dist[v] <= min)
            {
                min = dist[v];
                min_index = v;
            }

        return min_index;
    }
    public int[] dijkstra(int[,] graph, int src)
    {
        int[] dist = Enumerable.Repeat(int.MaxValue, V).ToArray();
        bool[] sptSet = Enumerable.Repeat(false, V).ToArray();
        dist[src] = 0;
        for (int count = 0; count < V - 1; count++)
        {
            int u = minDistance(dist, sptSet);
            sptSet[u] = true;
            for (int v = 0; v < V; v++)
                if (!sptSet[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                    dist[v] = dist[u] + graph[u, v];
        }
        return dist;
    }
}
