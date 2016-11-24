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
    private MagicCardFactory MagicCardFactory;

    void Start()
    {
        PlayerCardData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/CardDatabase/PlayerTestSpyRessurect.json"));
        EnemyCardData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/CardDatabase/EnemyTestSpyRessurect.json"));
        PlayerDatabase = new List<Card>();
        EnemyDatabase = new List<Card>();
        ConstructPlayerCardDatabase(PlayerCardData, PlayerDatabase);
        ConstructPlayerCardDatabase(EnemyCardData, EnemyDatabase);
    }

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
                Ability magicCardAbility = (Ability)System.Enum.Parse(typeof(Ability), CardData[i]["ability"].ToString(), true);
                AffectRow rowToAffect = (AffectRow)System.Enum.Parse(typeof(AffectRow), CardData[i]["affects"].ToString(), true);
                string description = CardData[i]["desc"].ToString();
                MagicCardFactory = new MagicCardFactory();
                //Add magic card to database
                Database.Add(MagicCardFactory.CreateMagicCard(id, title, cardType, faction, slug, magicCardAbility, rowToAffect, description));
            }
            else 
            {
                int attackValue = (int)CardData[i]["attackValue"];
                Ability cardAbility = (Ability)System.Enum.Parse(typeof(Ability), CardData[i]["ability"].ToString(), true);
                UnitCardFactory = new UnitCardFactory();
                //Add monster card to database
                Database.Add(UnitCardFactory.CreateUnitCard(id, title, cardType, faction, slug, attackValue, cardAbility));
            }          
        }
    }
}




