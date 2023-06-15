using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dice : MonoBehaviour
{
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public int rollResult = 0;
    public int rollCount = 0;
    public List<Player> players;
    public bool hasRolledDice;
    public int temp = 0;
    int currentPlayerTurn = 0;
    [SerializeField] GridManager gridManager;
    [SerializeField] Tilemap targetTilemap;
    public TurnBase turnBase;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        turnBase = targetTilemap.GetComponent<TurnBase>();
        players = gridManager.GetAllPlayers();


    }
    private void Update()
    {

        if (CanRollDice() && players[1].playerTurn == true)
        {
            StartCoroutine(RollTheDiceForPlayer2());
            hasRolledDice = true;
        }

    }
    private void OnMouseDown()
    {
        OnButtonMouse();
    }
    private void OnButtonMouse()
    {
        if (CanRollDice())
        {
            StartCoroutine(RollTheDice());
            hasRolledDice = true;


            if (currentPlayerTurn == 0 && turnBase.currentPhase == TurnBase.PHASE.BEGIN)
            {
                StartCoroutine(AutoRollDiceForBegin());
            }
        }
    }
    public bool CanRollDice()
    {
        if (turnBase.currentPhase == TurnBase.PHASE.BEGIN)
        {
            if (rollCount < players.Count)
            {
                return true;
            }
            return false;
        }
        else if (turnBase.currentPhase == TurnBase.PHASE.BATTLE)
        {
            if (hasRolledDice == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    private IEnumerator AutoRollDiceForBegin()
    {
        yield return new WaitForSeconds(2f);
        if (currentPlayerTurn == 0)
        {
            currentPlayerTurn = 1;
            int randomDiceSide = 0;
            for (int i = 0; i <= 20; i++)
            {
                randomDiceSide = Random.Range(0, 6);
                rend.sprite = diceSides[randomDiceSide];
                yield return new WaitForSeconds(0.05f);
            }
            rollResult = randomDiceSide + 1;
            if (turnBase.currentPhase == TurnBase.PHASE.BEGIN)
            {
                if (temp < rollResult)
                {
                    players[currentPlayerTurn].playerTurn = true;
                    turnBase.currentPhase = TurnBase.PHASE.INIT;
                    temp = 0;
                }
                else if (temp > rollResult)
                {
                    currentPlayerTurn = 0;
                    players[currentPlayerTurn].playerTurn = true;
                    turnBase.currentPhase = TurnBase.PHASE.INIT;
                    temp = 0;
                }
            }
            rollCount++;
        }
    }
    public IEnumerator RollTheDice()
    {
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }
        rollResult = randomDiceSide + 1;
        rollCount++;
        temp = rollResult;
    }
    public IEnumerator RollTheDiceForPlayer2()
    {
        yield return new WaitForSeconds(1.75f);
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }
        rollResult = randomDiceSide + 1;
        rollCount++;
        temp = rollResult;
    }
}
