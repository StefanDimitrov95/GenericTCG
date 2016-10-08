using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts;

public class CardDatabase : MonoBehaviour {

    private List<Card> database = new List<Card>();
    private JsonData cardData;

    void Start()
    {
        cardData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Libs/Cards.json"));

        ConstructCardDatabase();
        //Debug.Log(database[0].ToString());
        //Debug.Log(database[1].ToString());
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
        for (int i = 0; i < cardData.Count; i++)
        {
            if (cardData[i]["type"].ToString() == "MagicCard")
            {
                //Add magic card to database
                database.Add(new MagicCard((int)cardData[i]["id"], cardData[i]["title"].ToString(), "MagicCard", cardData[i]["slug"].ToString(), cardData[i]["effect"].ToString()));
            }
            else if (cardData[i]["type"].ToString() == "MonsterCard")
            {
                Class cardClass = (Class) System.Enum.Parse(typeof(Class), cardData[i]["class"].ToString(), true);
                //Add monster card to database
                database.Add(new MonsterCard((int)cardData[i]["id"], cardData[i]["title"].ToString(), "MonsterCard", cardData[i]["slug"].ToString(),(int)cardData[i]["attackValue"], cardClass));
            }
            //database.Add(new Card((int)cardData[i]["id"], cardData[i]["title"].ToString(), (int)cardData[i]["attack"]));
        }
    }
}




