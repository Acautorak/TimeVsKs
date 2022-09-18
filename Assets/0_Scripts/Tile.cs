using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
public int G;
public int H;
public int F { get { return G + H; } }
public bool isBlocked = false;
public Tile Previous;
public Vector3Int gridLocation;
public Vector2Int grid2DLocation {get {return new Vector2Int(gridLocation.x,gridLocation.y);}}
public GameObject customCursor;
public bool isTarget;
public bool isSpawner;
public Vector2Int baseTile = new Vector2Int (1,1);
//[SerializeField] SpriteRenderer spriteRenderer;



void Update()
{
    // IsBase();
  
}

void OnMouseEnter()
{
    gameObject.GetComponent<SpriteRenderer>().enabled = true;
    
}

void OnMouseExit()
{
    gameObject.GetComponent<SpriteRenderer>().enabled = false;
}
void IsBase ()
{
    Debug.Log(GridManager.Instance.map.ContainsKey(baseTile) + " " + baseTile);
    if (GridManager.Instance.map.ContainsKey(baseTile)){
        gameObject.tag = "Target";
        Debug.Log(GridManager.Instance.map[baseTile]);
        
    }
}


/*void OnTriggerExit2D(Collider2D other)
{
   other.GetComponent<Prefab>().standingOnTile = null;
}
void OnTriggerEnter2D(Collider2D other)
{
   if(other.transform.name == "Enemy1(Clone)")
   {
       other.gameObject.GetComponent<Prefab>().standingOnTile = this;
   
   D//ebug.Log("desilo se");
   }
   */
}


