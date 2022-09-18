using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
   
    private PathFinder pathfinder;
    private List<Tile> path;
    private Prefab character;
    public Tile tile;
    public Tile targetTile;
    public Vector2Int spawnTile = new Vector2Int (14,12);

    void Awake()
    {
      character = gameObject.GetComponent<Prefab>();
    }


    private void Start()
    {
        pathfinder = new PathFinder();
        path = new List<Tile>();
       // character = gameObject.GetComponent<Prefab>();
      character.standingOnTile = GridManager.Instance.GetTileAtPosition(spawnTile);
        
    }

    void Update()
    {
        targetTile = GameObject.FindGameObjectWithTag("Target").GetComponent<Tile>();
        path = pathfinder.FindPath(character.standingOnTile, targetTile, GridManager.Instance.GetMapTiles(GridManager.Instance.map));
        MoveAlongPath();
        Debug.Log(targetTile);
        if(Vector2.Distance(character.transform.position, targetTile.transform.position) <0.1 )
        {
            Destroy(gameObject);
            GameManager.Instance.gold -= 25;
            
        }
       
    }
    

    private void MoveAlongPath()
    {
        var step = speed * Time.deltaTime;

        float zIndex = path[0].transform.position.z;
        character.transform.position = Vector2.MoveTowards(character.transform.position, path[0].transform.position, step);
        character.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, zIndex);

        if(Vector2.Distance(character.transform.position, path[0].transform.position) < 0.00001f)
        {
            PositionCharacterOnTile(path[0]);
            path.RemoveAt(0);
        }
    }

    private void PositionCharacterOnTile(Tile tile)
    {
        character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y+0.0001f, tile.transform.position.z);
        character.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder+1;
        character.standingOnTile = tile;
    }   


}
