using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] Monster monster;
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private GameObject player;
    [SerializeField] private TurnBase turn;
    [SerializeField] private GameObject map;
    public List<Player> players;
    public bool hasAttacked = false;

    private void Start()
    {
        map = GameObject.Find("Map");
        targetTilemap = map.GetComponent<Tilemap>();
        turn = map.GetComponent<TurnBase>();
        player = GameObject.Find("Main Camera");
        players = player.GetComponent<GameControl>().players;
    }
    private void Update()
    {
        if (turn.currentPhase == TurnBase.PHASE.DRAW)
        {
            monster.canAttack = false;
        }
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        Vector3 worldPointPlayer;
        Vector3 monsterPoint;
        Vector3Int monsterPosition;
        Vector3Int playerPosition = Vector3Int.zero;
        monsterPoint = monster.transform.position;
        monsterPosition = targetTilemap.WorldToCell(monsterPoint);
        if (players[1].playerTurn == true)
        {
            worldPointPlayer = players[0].transform.position;
            playerPosition = targetTilemap.WorldToCell(worldPointPlayer);
            if ((monsterPosition.x - 1 == playerPosition.x && monsterPosition.y  == playerPosition.y) && monster.canAttack == false)
            {
                monster.canAttack = true;
                yield return new WaitForSeconds(0.5f);
                players[0].heal -= monster.dame;
            }
            else if ((monsterPosition.x + 1 == playerPosition.x && monsterPosition.y == playerPosition.y) && monster.canAttack == false)
            {
                monster.canAttack = true;
                yield return new WaitForSeconds(0.5f);
                players[0].heal -= monster.dame;
            }
            else if ((monsterPosition.x - 1 == playerPosition.x && monsterPosition.y - 1 == playerPosition.y) && monster.canAttack == false)
            {
                monster.canAttack = true;
                yield return new WaitForSeconds(0.5f);
                players[0].heal -= monster.dame;
            }
            else if ((monsterPosition.x  == playerPosition.x && monsterPosition.y + 1 == playerPosition.y) && monster.canAttack == false)
            {
                monster.canAttack = true;
                yield return new WaitForSeconds(0.5f);
                players[0].heal -= monster.dame;
            }
            else if ((monsterPosition.x == playerPosition.x && monsterPosition.y + 1 == playerPosition.y) && monster.canAttack == false)
            {
                monster.canAttack = true;
                yield return new WaitForSeconds(0.5f);
                players[0].heal -= monster.dame;
            }
            else if ((monsterPosition.x + 1 == playerPosition.x && monsterPosition.y - 1 == playerPosition.y) && monster.canAttack == false)
            {
                monster.canAttack = true;
                yield return new WaitForSeconds(0.5f);
                players[0].heal -= monster.dame;
            }
        }
        else if (players[0].playerTurn == true)
        {
            worldPointPlayer = players[1].transform.position;
            playerPosition = targetTilemap.WorldToCell(worldPointPlayer);
            if ((monsterPosition.x - 1 == playerPosition.x && monsterPosition.y == playerPosition.y) && monster.canAttack == false)
            {
                monster.canAttack = true;
                yield return new WaitForSeconds(1f);
                players[1].heal -= monster.dame;
            }
            else if ((monsterPosition.x + 1 == playerPosition.x && monsterPosition.y == playerPosition.y) && monster.canAttack == false)
            {
                monster.canAttack = true;
                yield return new WaitForSeconds(1f);
                players[1].heal -= monster.dame;
            }
            else if ((monsterPosition.x - 1 == playerPosition.x && monsterPosition.y - 1 == playerPosition.y) && monster.canAttack == false)
            {
                monster.canAttack = true;
                yield return new WaitForSeconds(1f);
                players[1].heal -= monster.dame;
            }
            else if ((monsterPosition.x - 1 == playerPosition.x && monsterPosition.y + 1 == playerPosition.y) && monster.canAttack == false)
            {
                monster.canAttack = true;
                yield return new WaitForSeconds(1f);
                players[1].heal -= monster.dame;
            }
            else if ((monsterPosition.x  == playerPosition.x && monsterPosition.y + 1 == playerPosition.y) && monster.canAttack == false)
            {
                monster.canAttack = true;
                yield return new WaitForSeconds(1f);
                players[1].heal -= monster.dame;
            }
            else if ((monsterPosition.x  == playerPosition.x && monsterPosition.y - 1 == playerPosition.y) && monster.canAttack == false)
            {
                monster.canAttack = true;
                yield return new WaitForSeconds(1f);
                players[1].heal -= monster.dame;
            }
        }
    }    
}
