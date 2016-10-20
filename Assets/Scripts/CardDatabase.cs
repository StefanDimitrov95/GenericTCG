using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts;
using Assets.Scripts.Classes;
using Assets.Scripts.Classes.EnumClasses;

public class CardDatabase : MonoBehaviour {

    public List<Card> database { get; set; }
    private JsonData cardData;
    private UnitCardFactory unitCardFactory;

    void Start()
    {
        cardData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/CardDatabase/Cards.json"));
        ConstructCardDatabase();        
    }

    public Card FetchCardById(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].ID == id)
            {
                return database[i];
            }
        }
        return null;
    }
    void ConstructCardDatabase()
    {
        database = new List<Card>();
        for (int i = 0; i < cardData.Count; i++)
        {
            int id = (int)cardData[i]["id"];
            string title = cardData[i]["title"].ToString();
            Faction faction = (Faction)System.Enum.Parse(typeof(Faction), cardData[i]["faction"].ToString(), true);
            CardType cardType = (CardType)System.Enum.Parse(typeof(CardType), cardData[i]["type"].ToString(), true);
            string slug = cardData[i]["slug"].ToString();

            if (cardType == CardType.Special)
            {
                //Add magic card to database
                //database.Add(new MagicCard((int)cardData[i]["id"], cardData[i]["title"].ToString(), cardType, faction, cardData[i]["slug"].ToString(), cardData[i]["effect"].ToString()));
            }
            else 
            {
                int attackValue = (int)cardData[i]["attackValue"];
                MonsterAbility cardAbility = (MonsterAbility)System.Enum.Parse(typeof(MonsterAbility), cardData[i]["ability"].ToString(), true);
                unitCardFactory = new UnitCardFactory();
                //Add monster card to database
                database.Add(unitCardFactory.CreateUnitCard(id, title, cardType, faction, slug, attackValue, cardAbility));
            }          
        }
    }
}




