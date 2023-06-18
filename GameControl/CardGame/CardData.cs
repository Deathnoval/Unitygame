
using UnityEngine;

[System.Serializable]

public class CardData 
{
    public int cardId;
    public string cardName;
    public string cardDescription;
    public int cardPower;
    public int cardType;
  
    public string img;

    public CardData(int id, string name, string description, int power, int type, string sprite)
    {
        cardId = id;
        cardName = name;
        cardDescription = description;
        cardPower = power;
        cardType = type;
        img = sprite;
       
    }
}
