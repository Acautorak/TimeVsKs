using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;


public class GridManager : MonoBehaviour
{
    private static GridManager _instance;
    public static GridManager Instance { get { return _instance; } }

  
    public GameObject TilePrefab;
    public GameObject tileContainer;

    public Dictionary<Vector2Int, Tile> map;
    public bool ignoreBottomTiles;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
    }

    void Start()
    {
        var tileMaps = gameObject.transform.GetComponentsInChildren<Tilemap>().OrderByDescending(x => x.GetComponentInChildren<TilemapRenderer>().sortingOrder);
        map = new Dictionary<Vector2Int, Tile>();

        foreach (var tm in tileMaps)
        {
            BoundsInt bounds = tm.cellBounds;

            for (int z = bounds.max.z; z >= bounds.min.z; z--)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    for (int x = bounds.min.x; x < bounds.max.x; x++)
                    {
                        if (z == 0 && ignoreBottomTiles)
                            return;

                        if (tm.HasTile(new Vector3Int(x, y, z)))
                        {
                            if (!map.ContainsKey(new Vector2Int(x, y)))
                            {
                                var tile = Instantiate(TilePrefab, tileContainer.transform);
                                var cellWorldPosition = tm.GetCellCenterWorld(new Vector3Int(x, y, z));
                                tile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                                tile.gameObject.GetComponent<Tile>().gridLocation = new Vector3Int(x, y, z);

                                map.Add(new Vector2Int(x, y), tile.gameObject.GetComponent<Tile>());
                            }
                        }
                    }
                }
            }
        }

    //    GetTileAtPosition(target).gameObject.tag = "Target";
        
    }

    public List<Tile> GetNeightbourTiles(Tile currentTile, List<Tile> searchableTiles)
    {
        Dictionary<Vector2Int, Tile> tilesToSearch =  new Dictionary<Vector2Int, Tile>();
        var map = GridManager.Instance.map;

        if(searchableTiles.Count>0)
        {
            foreach(var item in searchableTiles)
            {
                tilesToSearch.Add(item.grid2DLocation, item);
            }
        }else
            {
                tilesToSearch = map;
            }
       
        
        List<Tile> neighbours = new List<Tile>();

            //right
        Vector2Int locationToCheck = new Vector2Int(
            currentTile.gridLocation.x + 1,
            currentTile.gridLocation.y
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            neighbours.Add(tilesToSearch[locationToCheck]);
        }

            //left
        locationToCheck = new Vector2Int(
            currentTile.gridLocation.x - 1,
            currentTile.gridLocation.y
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            neighbours.Add(tilesToSearch[locationToCheck]);
        }

        //top
        locationToCheck = new Vector2Int(
            currentTile.gridLocation.x,
            currentTile.gridLocation.y + 1
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            neighbours.Add(tilesToSearch[locationToCheck]);
        }

            //bottom
        locationToCheck = new Vector2Int(
            currentTile.gridLocation.x,
            currentTile.gridLocation.y - 1
        );

        if (tilesToSearch.ContainsKey(locationToCheck))
        {
            neighbours.Add(tilesToSearch[locationToCheck]);
        }

        return neighbours;
    }

    public Tile GetTileAtPosition(Vector2Int pos) 
    {
        if(map.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }

    public List<Tile> GetMapTiles(Dictionary<Vector2Int, Tile> map)
    {
        List<Tile> mapList = new List<Tile>();
        mapList = map.OfType<Tile>().ToList();
        return mapList;
    }
    
    
}