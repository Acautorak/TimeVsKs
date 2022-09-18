using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PathFinder
{
    public List<Tile> FindPath(Tile start, Tile end, List<Tile> searchableTiles)
    {
        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(start);

        while (openList.Count > 0)
        {
            Tile currentTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            if (currentTile == end)
            {
                return GetFinishedList(start, end);
            }

            foreach (var tile in GridManager.Instance.GetNeightbourTiles(currentTile, searchableTiles))
            {
                if (tile.isBlocked || closedList.Contains(tile) || Mathf.Abs(currentTile.transform.position.z - tile.transform.position.z) > 1)
                {
                    continue;
                }

                tile.G = GetManhattenDistance(start, tile);
                tile.H = GetManhattenDistance(end, tile);

                tile.Previous = currentTile;

                if (!openList.Contains(tile))
                {
                    openList.Add(tile);
                }
            }
        }

        return new List<Tile>();
    }

    private List<Tile> GetFinishedList(Tile start, Tile end)
    {
        List<Tile> finishedList = new List<Tile>();
        Tile currentTile = end;

        while (currentTile != start)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.Previous;
        }
        finishedList.Reverse();

        return finishedList;
    }

    private int GetManhattenDistance(Tile start, Tile tile)
    {
        return Mathf.Abs(start.gridLocation.x - tile.gridLocation.x) + Mathf.Abs(start.gridLocation.y - tile.gridLocation.y);
    }

    

      
}