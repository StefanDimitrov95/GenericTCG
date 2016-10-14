using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts;

public class CardDatabase : MonoBehaviour {

    public List<Card> database { get; set; }
    private JsonData cardData;

    void Start()
    {
        cardData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Libs/Cards.json"));
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
            if (cardData[i]["type"].ToString() == "MagicCard")
            {
                //Add magic card to database
                database.Add(new MagicCard((int)cardData[i]["id"], cardData[i]["title"].ToString(), "MagicCard", cardData[i]["slug"].ToString(), cardData[i]["effect"].ToString()));
            }
            else if (cardData[i]["type"].ToString() == "MonsterCard")
            {
                MonsterPerk cardPerk = (MonsterPerk)System.Enum.Parse(typeof(MonsterPerk), cardData[i]["cardPerk"].ToString(), true);
                Class cardClass = (Class) System.Enum.Parse(typeof(Class), cardData[i]["class"].ToString(), true);
                //Add monster card to database
                database.Add(new MonsterCard((int)cardData[i]["id"], cardData[i]["title"].ToString(), "MonsterCard", cardData[i]["slug"].ToString(),(int)cardData[i]["attackValue"], cardClass, cardPerk));
            }          
        }
    }
}




