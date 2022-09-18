using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseController : MonoBehaviour
{

    private static MouseController _instance;
    public static MouseController Instance {get {return _instance;}}
    private Tile tile;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }else
        {
            _instance= this;
        }
    }

    void Update()
    {
       
       if(Input.GetMouseButtonDown(0))
       {
        tile = GetMouseTile();
        tile.gameObject.GetComponent<SpriteRenderer>().color = new Color (1,0,0,1);
        tile.isBlocked = true;
        tile.tag = "Target";
    
       }
       if(Input.GetMouseButtonDown(1))
       {
        tile = GetMouseTile();
        tile.gameObject.GetComponent<SpriteRenderer>().color = new Color (1,0,0,0);
        tile.isBlocked = false;
       // tile.gameObject.tag = "Target";
    
       }
    }

    public Tile GetMouseTile ()
    {
        RaycastHit2D? hit = GetFocusedOnTile();
        if (hit.HasValue)
        {
            Tile tile = hit.Value.collider.gameObject.GetComponent<Tile>();
            
            return tile;
            
        } 
        else{
            return null;
        }
    }
    public static RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

        if (hits.Length > 0)
        {
            
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
            
        }

        return null;
    }


}
