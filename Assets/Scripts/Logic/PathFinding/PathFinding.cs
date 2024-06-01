using System.Collections.Generic;
using System.Linq;
using System;
using Game.Tiles;
public static class PathFinding
{
    public static List<BaseTile> FindPath(BaseTile startNode, BaseTile targetNode)
    {
        var toSearch = new List<BaseTile>() { startNode };
        var processed = new List<BaseTile>();
        while (toSearch.Any())
        {
            var current = toSearch[0];
            foreach (var t in toSearch)
                if (t.F < current.F || t.F == current.F && t.H < current.H) current = t;

            processed.Add(current);
            toSearch.Remove(current);


            if (current == targetNode)
            {
                var currentPathTile = targetNode;
                var path = new List<BaseTile>();
                var count = 100;
                while (currentPathTile != startNode)
                {
                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.Connection;
                    count--;
                    if (count < 0) throw new Exception();
                }

                path.Reverse();
                return path;
            }

            foreach (var neighbor in current.Neighbors.Where(t => t.IsTraversable() && !processed.Contains(t)))
            {
                var inSearch = toSearch.Contains(neighbor);

                var costToNeighbor = current.G + current.GetDistance(neighbor);

                if (!inSearch || costToNeighbor < neighbor.G)
                {
                    neighbor.SetG(costToNeighbor);
                    neighbor.SetConnection(current);

                    if (!inSearch)
                    {
                        neighbor.SetH(neighbor.GetDistance(targetNode));
                        toSearch.Add(neighbor);
                    }
                }
            }
        }
        return null;
    }
}
