
using UnityEngine;
public class Player :MonoBehaviour
{
    public int moveDistance = 0;
    public int range;
    public string Name;
    public int heal;
    public int maxHeal = 20;
    public int dame = 1;
    public bool shouldAttack = false;
    public bool canBeAttack=false;
    public bool playerTurn = false;
    public TurnBase turn;
    public Deck deck;
    public int x;
    public int y;
    //public int HealthPoint { get { return heal; } }
    //public int GetHealthPoint() { return heal; }
    //public void Heal(int value)
    //{
    //    heal += value;
    //    if (heal > maxHeal)
    //        heal = maxHeal;
    //}
}
