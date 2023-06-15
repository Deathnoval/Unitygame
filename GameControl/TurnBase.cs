using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class TurnBase : MonoBehaviour
{
    List<Player> players;
    [SerializeField] GridManager gridManager;
    [SerializeField] Dice dice;
    [SerializeField] LimitCardInHand HandPlayer1;
    [SerializeField] LimitCardInHand HandPlayer2;
    [SerializeField] Monster monster;
    public enum PHASE
   {
        BEGIN,
        INIT,
        DRAW,
        BATTLE,
   };
   public PHASE currentPhase;

    void Start()
    {
        currentPhase = PHASE.BEGIN;
        players = gridManager.GetAllPlayers();
        //Debug.Log(monster.GetComponent<MonsterAttack>());
        StartCoroutine(players[0].deck.StartGame());
        StartCoroutine(players[1].deck.StartGame2());
    }
     void Update()
    {

      
        if (currentPhase == PHASE.INIT)
        {
            
           dice.hasRolledDice = false;
           currentPhase = PHASE.DRAW;


        }
        else if (currentPhase == PHASE.DRAW && players[0].playerTurn)
        {
           
            if(HandPlayer1.CountCard < 7)
            {
                players[0].deck.Draw();
                currentPhase = PHASE.BATTLE;
            }
            else
            {
                currentPhase = PHASE.BATTLE;
            }
          
        }
        else if (currentPhase == PHASE.DRAW && players[1].playerTurn)
        {
            if (HandPlayer2.CountCard < 10)
            {
                players[1].deck.Draw2();
                currentPhase = PHASE.BATTLE;
            }
            else
            {
                currentPhase = PHASE.BATTLE;
            }
        }
       

    }
}
