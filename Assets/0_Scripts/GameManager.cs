using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance {get {return _instance;}}

    public Tower tower1;
    public Tower tower2;
    public int gold;
    public int progress;
    public float timeToDefend;
    private float timeMultiplyer = 1;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private TMP_Text displayGold;
    public Vector2Int baseTile = new Vector2Int (14,12);
    public Tile baset;

    public GameObject completeLevelUI;
    

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

    void Start()
    {
       // Tile targetTile = GridManager.Instance.GetTileAtPosition(targetTilePos);
       // targetTile.tag = "Target";
      baset=  GridManager.Instance.GetTileAtPosition(baseTile);
      baset.gameObject.tag = "Target";  
      gold = 50;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            timeMultiplyer -= 0.2f;
            
        if(Input.GetKeyDown(KeyCode.M))
        {
            BuyTower();
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            BuyHouse();
        }


        timeToDefend -= (timeMultiplyer * Time.deltaTime);
        displayText.text = Mathf.Round(timeToDefend).ToString();
        displayGold.text = Mathf.Round(gold).ToString();

    }

    public void BuyTower()
    {
        if(gold >= tower1.cost){    
        Vector3 position = new Vector3(MouseController.Instance.GetMouseTile().transform.position.x, MouseController.Instance.GetMouseTile().transform.position.y, 0f);
        Instantiate(tower1, position,Quaternion.identity);
        PositionTowerOnTile(MouseController.Instance.GetMouseTile());
        this.gold-=tower1.cost;
        }   
    }

    private void PositionTowerOnTile(Tile tile)
    {
        tower1.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y+0.0001f, tile.transform.position.z);
       // tower.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder+1;
        tower1.standingOnTile = tile;
        tile.isBlocked = true;
    }   

    private void PositionTower2OnTile(Tile tile)
    {
        tower2.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y+0.0001f, tile.transform.position.z);
       // tower.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder+1;
        tower2.standingOnTile = tile;
        tile.isBlocked = true;
    }   

    private void BuyHouse()
    {
        if(gold >= tower2.cost){   
    Vector3 position = new Vector3(MouseController.Instance.GetMouseTile().transform.position.x, MouseController.Instance.GetMouseTile().transform.position.y, 0f);
    Instantiate(tower2, position,Quaternion.identity);
    PositionTowerOnTile(MouseController.Instance.GetMouseTile());
    this.gold-=tower2.cost;
        }
    }

    public void CompleteLevel()
    {
        if(gold>=1000)  completeLevelUI.SetActive(true);
    }
    
}
