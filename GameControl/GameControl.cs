using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    private List<ItemCard> usedCards = new List<ItemCard>();
    public List<Player> players;
    [SerializeField] GridMap grid;
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] Tilemap highlightTilemap;
    [SerializeField] Tilemap highlightTilemap2;
    [SerializeField] TileBase highlightTile;
    [SerializeField] TileBase highlightTile2;
    [SerializeField] GridManager gridManager;
    [SerializeField] UsePlayerCard test;
    Pathfinding pathfinding;
    public Text namePlayer;
    public Text namePlayer2;
    public Text healPlayer;
    public Text healPlayer2;
    int currentPlayerTurn = 0;
    private Player selectedPlayer = null;
    [SerializeField] TurnBase turn;
    [SerializeField] FireCanon playerShoot;
    [SerializeField] Dice dice;
    [SerializeField] GameObject dropZones;
    public GameObject enemy;
    public GameObject healEffect;
    public GameObject rangeEffect;
    public GameObject dameUpEffect;
    public GameObject cardEvent;
    public GameObject cardEvent2;
    private List<PathNode> highlight;
    private int index=-1;


    private void Start()
    {
        pathfinding = targetTilemap.GetComponent<Pathfinding>();
        players = gridManager.GetAllPlayers();
        PositionPlayer();
     
    }
    private void Update()
    {
        InfoPlayer();
        if(index==-1)
            MovePlayer();
        UsedCard();
    }
    private void MovePlayer()
    {
        Vector3 worldPointPlayer;
        Vector3 worldPoint;
        Vector3Int playerPosition = Vector3Int.zero;
        if (players[currentPlayerTurn].playerTurn == true)
        {
            worldPointPlayer = players[currentPlayerTurn].transform.position;
            playerPosition = targetTilemap.WorldToCell(worldPointPlayer);
        }
        else if (players[currentPlayerTurn + 1].playerTurn == true)
        {
            worldPointPlayer = players[currentPlayerTurn + 1].transform.position;
            playerPosition = targetTilemap.WorldToCell(worldPointPlayer);
        }
        if ((players[currentPlayerTurn].playerTurn == true || players[currentPlayerTurn + 1].playerTurn == true) && turn.currentPhase == TurnBase.PHASE.BATTLE)
        {
            if (currentPlayerTurn < players.Count - 1 && players[currentPlayerTurn + 1].playerTurn == true)
            {
                currentPlayerTurn++;
            }
            if (players[currentPlayerTurn].x == playerPosition.x && players[currentPlayerTurn].y == playerPosition.y)
            {
                highlightTilemap.ClearAllTiles();
                highlightTilemap2.ClearAllTiles();
                if (gridManager.CheckPosition(playerPosition.x, playerPosition.y) == false)
                {
                    Debug.Log(playerPosition.x + " " + playerPosition.y);
                }
                selectedPlayer = players[currentPlayerTurn];
                int temp = dice.temp;
                if (temp == 0)
                {
                    //selectedPlayer = null;
                    return;
                }
                selectedPlayer.moveDistance = dice.temp;

                if (selectedPlayer != null)
                {
                    List<PathNode> toHighlight = new List<PathNode>();
                    List<PathNode> toHighlight2 = new List<PathNode>();
                    pathfinding.Clear();
                    pathfinding.CalculateWalkableTerrain(playerPosition.x, playerPosition.y, players[currentPlayerTurn].moveDistance, ref toHighlight);
                    pathfinding.CalculateWalkableTerrain(playerPosition.x, playerPosition.y, players[currentPlayerTurn].range, ref toHighlight2);
                    for (int i = 1; i < toHighlight.Count; i++)
                    {
                        if(grid.CheckPositionMonster(toHighlight[i].xPos, toHighlight[i].yPos))
                        {
                            continue;
                        }    
                        highlightTilemap.SetTile(new Vector3Int(toHighlight[i].xPos, toHighlight[i].yPos, 0), highlightTile);
                    }
                    highlight = toHighlight;
                 
                    for (int i = 0; i < players.Count; i++)
                    {
                        if (players[i].x != playerPosition.x || players[i].y != playerPosition.y)
                        {
                            bool isPlayerHighlighted = false;
                            for (int j = 1; j < toHighlight2.Count; j++)
                            {
                                if (toHighlight2[j].xPos == players[i].x && toHighlight2[j].yPos == players[i].y)
                                {
                                    isPlayerHighlighted = true;
                                    break;
                                }
                            }
                            if (isPlayerHighlighted)
                            {
                                highlightTilemap2.SetTile(new Vector3Int(players[i].x, players[i].y, 0), highlightTile2);
                            }
                        }
                    }
                }
            }
    }
        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int clickPosition = targetTilemap.WorldToCell(worldPoint);
//15/6 sửa để bot tự di chuyển
        if (players[currentPlayerTurn].playerTurn == true && selectedPlayer != null)
        {
            if ((Input.GetMouseButtonDown(0) && players[0].playerTurn == true && selectedPlayer != null) || (players[1].playerTurn == true && selectedPlayer != null))
            {
                if (selectedPlayer == null)
                {
                    return;
                }
                ///#################################################
              
                List<PathNode> path=new List<PathNode>();
                if (players[0].playerTurn == true)
                {
                    path = pathfinding.TrackBackPath(selectedPlayer, clickPosition.x, clickPosition.y);
                    Debug.Log("thuyen 1 "+clickPosition.x + "   " + clickPosition.y);

                }
                else if (players[1].playerTurn == true)
                {

                    // Lấy một chỉ số ngẫu nhiên từ 0 đến kích thước của danh sách
                     index = Random.Range(0,highlight.Count);

                    
                    // Lấy phần tử tương ứng từ danh sách
                    PathNode randomNode = highlight[index];
                    path = pathfinding.TrackBackPath(selectedPlayer, randomNode.xPos, randomNode.yPos);
                    Debug.Log("thuyen 2 "+randomNode.xPos + "    " + randomNode.yPos+"         " + index);

                }

                if (selectedPlayer.shouldAttack && selectedPlayer.canBeAttack)
                {
                    playerShoot.Shoot(worldPoint);
                    selectedPlayer.canBeAttack = false;
                    StartCoroutine(TakeDame());
                }
                else if (usedCards.Count > 0)
                {
                    return;
                }
                else if (path != null && path.Count > 0 && usedCards.Count == 0)
                {
                    StartCoroutine(MovePlayerCoroutine(path));
                }
                else
                {
                    return;
                }
            }
        }
        
        
    }
    private IEnumerator TakeDame()
    {
        yield return new WaitForSeconds(0.75f);
        if (currentPlayerTurn == 0)
        {
            players[1].heal -= selectedPlayer.dame;
            Deselect();

        }
        else if (currentPlayerTurn == 1)
        {
            players[0].heal -= selectedPlayer.dame;
          
            Deselect();
        }
    }
    private IEnumerator MovePlayerCoroutine(List<PathNode> path)
    {
        highlightTilemap.ClearAllTiles();
        highlightTilemap2.ClearAllTiles();
        for (int i = path.Count - 1; i >= 0; i--)
            {
                selectedPlayer.GetComponent<MapElement>().MovePlayer(path[i].xPos, path[i].yPos);
                yield return new WaitForSeconds(0.1f);
             }
       
        selectedPlayer.x = path[0].xPos;
        selectedPlayer.y = path[0].yPos;
        CreateEvent();
        Deselect();
    }
    private void Deselect()
    {
        players[currentPlayerTurn].playerTurn = false;
        pathfinding.Clear();
        if (currentPlayerTurn + 1 >= players.Count)
        {
            currentPlayerTurn = 0;
        }
        else
        {
            currentPlayerTurn += 1;
        }
        players[currentPlayerTurn].playerTurn = true;
        selectedPlayer.range = 1;
        selectedPlayer.dame = 1;
        selectedPlayer.canBeAttack = false;
        selectedPlayer.shouldAttack = false;
        selectedPlayer = null;
        dice.temp = 0;
        turn.currentPhase = TurnBase.PHASE.INIT;
        index = -1;

    }
    private void InfoPlayer()
    {
        namePlayer.text = players[0].Name;
        namePlayer2.text = players[1].Name;
        healPlayer.text = players[0].heal.ToString();
        healPlayer2.text = players[1].heal.ToString();
    }    
    private void PositionPlayer()
    { 
        players[currentPlayerTurn].x = 15;
        players[currentPlayerTurn].y = 0;
        players[currentPlayerTurn + 1].x = 0;
        players[currentPlayerTurn + 1].y = 15;
  
    }
    private void UsedCard()
    {
            ItemCard[] cardsToAdd = dropZones.GetComponentsInChildren<ItemCard>();
            foreach (ItemCard card in cardsToAdd)
            {
                if (!usedCards.Contains(card))
                {
                    usedCards.Add(card);
                }
            }
            List<ItemCard> cardsToRemove = new List<ItemCard>();
            foreach (ItemCard usedCard in usedCards)
            {
                if (usedCard!=null && !usedCard.transform.IsChildOf(dropZones.transform))
                {
                    cardsToRemove.Add(usedCard);
                }
            }
            foreach (ItemCard cardToRemove in cardsToRemove)
            {
                usedCards.Remove(cardToRemove);
            }
    }
    public void ConfirmCard()
    {
        foreach (ItemCard card in usedCards)
        {
            switch (card.cardIdGame)
            {
                case 7:
                    players[0].canBeAttack = true;
                    break;
                case 8:
                    GameObject dameUp = Instantiate(dameUpEffect, selectedPlayer.transform.position, Quaternion.identity);
                    selectedPlayer.dame = ++selectedPlayer.dame;
                    Destroy(dameUp, 3f);
                    break;
                case 9:
                   if(dice.temp!=0)
                   {
                        if(selectedPlayer.range<dice.temp)
                        {
                            GameObject rangeUp = Instantiate(rangeEffect, selectedPlayer.transform.position, Quaternion.identity);
                            selectedPlayer.range += 1;
                            Destroy(rangeUp, 3f);
                        }    
                   }    
                    break;
                case 10:
                    if (selectedPlayer.heal < selectedPlayer.maxHeal)
                    {
                        GameObject healing = Instantiate(healEffect, selectedPlayer.transform.position, Quaternion.identity);
                       if(test.checkHeal==true)
                        {
                            selectedPlayer.heal += 2;
                            test.checkHeal = false;
                        }    
                       else
                        {
                            selectedPlayer.heal += 1;
                        }    
                        Destroy(healing, 3f);
                    }
                    break;
            }
        }
        DeleteCard();
        
    }
    public void DeleteCard()
    {
        if (usedCards.Count > 0)
        {
            foreach (Transform card in dropZones.transform)
            {
                Destroy(card.gameObject);
            }
            StartCoroutine(DelayDeleteCard());
        }
    }
    IEnumerator DelayDeleteCard()
    {
        yield return new WaitForSeconds(0.5f);
        usedCards.Clear();
    }
  void CreateEvent()
    {
        int random = Random.Range(1, 3);
     
         if (grid.CheckEvent(selectedPlayer.x, selectedPlayer.y) && random==1)
        {
           if(grid.CheckWalkable(selectedPlayer.x + 1, selectedPlayer.y))
            {
                grid.Set(selectedPlayer.x, selectedPlayer.y, 0);
                gridManager.UpdateTile(selectedPlayer.x, selectedPlayer.y);
                if (selectedPlayer.y % 2 == 0)
                {
                    GameObject spawnedEnemy = Instantiate(enemy, new Vector3(selectedPlayer.x * 1f + 1f, selectedPlayer.y * 0.75f, -0.5f), Quaternion.identity);
                }
                else
                {
                    GameObject spawnedEnemy = Instantiate(enemy, new Vector3(selectedPlayer.x * 1f + 0.5f + 1f, selectedPlayer.y * 0.75f, -0.5f), Quaternion.identity);
                }
            }
           else if(grid.CheckWalkable(selectedPlayer.x + 1, selectedPlayer.y) == false)
            {
                if (grid.CheckWalkable(selectedPlayer.x + 1, selectedPlayer.y + 1))
                {
                    grid.Set(selectedPlayer.x, selectedPlayer.y , 0);
                    gridManager.UpdateTile(selectedPlayer.x, selectedPlayer.y );
                    if (selectedPlayer.y % 2 == 0)
                    {
                        GameObject spawnedEnemy = Instantiate(enemy, new Vector3(selectedPlayer.x * 1f + 0.5f, selectedPlayer.y * 0.75f + 0.75f, -0.5f), Quaternion.identity);
                    }
                    else
                    {
                        GameObject spawnedEnemy = Instantiate(enemy, new Vector3(selectedPlayer.x * 1f + 1f, selectedPlayer.y * 0.75f + 0.75f, -0.5f), Quaternion.identity);
                    }
                }    
                else if(grid.CheckWalkable(selectedPlayer.x + 1, selectedPlayer.y +1)==false)
                {
                    if(grid.CheckWalkable(selectedPlayer.x + 1, selectedPlayer.y-1))
                    {
                        grid.Set(selectedPlayer.x, selectedPlayer.y, 0);
                        gridManager.UpdateTile(selectedPlayer.x, selectedPlayer.y);
                        if (selectedPlayer.y % 2 == 0)
                        {
                            GameObject spawnedEnemy = Instantiate(enemy, new Vector3(selectedPlayer.x * 1f + 0.5f , (selectedPlayer.y * 0.75f) - 0.75f, -0.5f), Quaternion.identity);
                        }
                        else
                        {
                            GameObject spawnedEnemy = Instantiate(enemy, new Vector3(selectedPlayer.x * 1f + 1f , (selectedPlayer.y * 0.75f) - 0.75f, -0.5f), Quaternion.identity);
                        }
                    }
                    else if (grid.CheckWalkable(selectedPlayer.x +1 , selectedPlayer.y- 1)== false)
                    {
                        if(grid.CheckWalkable(selectedPlayer.x - 1, selectedPlayer.y ))
                        {
                            grid.Set(selectedPlayer.x, selectedPlayer.y, 0);
                            gridManager.UpdateTile(selectedPlayer.x, selectedPlayer.y);
                            if (selectedPlayer.y % 2 == 0)
                            {
                                GameObject spawnedEnemy = Instantiate(enemy, new Vector3(selectedPlayer.x * 1f - 1f, selectedPlayer.y * 0.75f, -0.5f), Quaternion.identity);
                            }
                            else
                            {
                                GameObject spawnedEnemy = Instantiate(enemy, new Vector3(selectedPlayer.x * 1f + 0.5f - 1f, selectedPlayer.y * 0.75f , -0.5f), Quaternion.identity);
                            }
                        }    
                        else if(grid.CheckWalkable(selectedPlayer.x - 1, selectedPlayer.y)==false)
                        {
                            if(grid.CheckWalkable(selectedPlayer.x , selectedPlayer.y + 1))
                            {
                                grid.Set(selectedPlayer.x, selectedPlayer.y, 0);
                                gridManager.UpdateTile(selectedPlayer.x, selectedPlayer.y);
                                if (selectedPlayer.y % 2 == 0)
                                {
                                    GameObject spawnedEnemy = Instantiate(enemy, new Vector3(selectedPlayer.x * 1f +0.5f , (selectedPlayer.y * 0.75f) + 0.75f, -0.5f), Quaternion.identity);
                                }
                                else
                                {
                                    GameObject spawnedEnemy = Instantiate(enemy, new Vector3(selectedPlayer.x * 1f + 1f , (selectedPlayer.y * 0.75f) + 0.75f, -0.5f), Quaternion.identity);
                                }
                            }    
                            else if(grid.CheckWalkable(selectedPlayer.x , selectedPlayer.y+1)==false)
                            {

                                if(grid.CheckWalkable(selectedPlayer.x , selectedPlayer.y-1))
                                {
                                    grid.Set(selectedPlayer.x, selectedPlayer.y, 0);
                                    gridManager.UpdateTile(selectedPlayer.x, selectedPlayer.y);
                                    if (selectedPlayer.y % 2 == 0)
                                    {
                                        GameObject spawnedEnemy = Instantiate(enemy, new Vector3(selectedPlayer.x * 1f + 0.5f  , (selectedPlayer.y * 0.75f) - 0.75f, -0.5f), Quaternion.identity);
                                    }
                                    else
                                    {
                                        GameObject spawnedEnemy = Instantiate(enemy, new Vector3(selectedPlayer.x * 1f + 1f, (selectedPlayer.y * 0.75f) - 0.75f, -0.5f), Quaternion.identity);
                                    }
                                }
                            }    
                        }    
                    }    
                }
            }    
        }
        else if(grid.CheckEvent(selectedPlayer.x, selectedPlayer.y) && random == 2)
        {
            if (players[0].playerTurn)
            {
                Instantiate(cardEvent, transform.position, transform.rotation);
            }    
            else
            {
                Instantiate(cardEvent2, transform.position, transform.rotation);
            }
            grid.Set(selectedPlayer.x, selectedPlayer.y, 4);
            gridManager.UpdateTile(selectedPlayer.x, selectedPlayer.y);
           
        }
      
    }
   
}