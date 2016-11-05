using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts;
using Assets.Scripts.Classes;
using Assets.Scripts.Classes.EnumClasses;

public class CardDatabase : MonoBehaviour {

    public List<Card> PlayerDatabase { get; set; }
    public List<Card> EnemyDatabase { get; set; }
    private JsonData PlayerCardData;
    private JsonData EnemyCardData;
    private UnitCardFactory UnitCardFactory;

    void Start()
    {
        PlayerCardData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/CardDatabase/CardsMuster.json"));
        EnemyCardData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/CardDatabase/Cards.json"));
        PlayerDatabase = new List<Card>();
        EnemyDatabase = new List<Card>();
        ConstructPlayerCardDatabase(PlayerCardData, PlayerDatabase);
        ConstructPlayerCardDatabase(EnemyCardData, EnemyDatabase);
    }

    //public Card FetchCardById(int id)
    //{
    //    for (int i = 0; i < Database.Count; i++)
    //    {
    //        if (Database[i].ID == id)
    //        {
    //            return Database[i];
    //        }
    //    }
    //    return null;
    //}

    void ConstructPlayerCardDatabase(JsonData CardData, List<Card> Database)
    {       
        for (int i = 0; i < CardData.Count; i++)
        {
            int id = (int)CardData[i]["id"];
            string title = CardData[i]["title"].ToString();
            Faction faction = (Faction)System.Enum.Parse(typeof(Faction), CardData[i]["faction"].ToString(), true);
            CardType cardType = (CardType)System.Enum.Parse(typeof(CardType), CardData[i]["type"].ToString(), true);
            string slug = CardData[i]["slug"].ToString();

            if (cardType == CardType.Special)
            {
                //Add magic card to database
                //database.Add(new MagicCard((int)cardData[i]["id"], cardData[i]["title"].ToString(), cardType, faction, cardData[i]["slug"].ToString(), cardData[i]["effect"].ToString()));
            }
            else 
            {
                int attackValue = (int)CardData[i]["attackValue"];
                MonsterAbility cardAbility = (MonsterAbility)System.Enum.Parse(typeof(MonsterAbility), CardData[i]["ability"].ToString(), true);
                UnitCardFactory = new UnitCardFactory();
                //Add monster card to database
                Database.Add(UnitCardFactory.CreateUnitCard(id, title, cardType, faction, slug, attackValue, cardAbility));
            }          
        }
    }
}




