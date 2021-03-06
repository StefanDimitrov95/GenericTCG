﻿
using Assets.Scripts.Classes;
using Assets.Scripts.Classes.EnumClasses;
using UnityEngine;

namespace Assets.Scripts
{

    
    public abstract class Card : ICard
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public CardType Type { get; set; }

        public Faction CardFaction { get; set; }

        public string Slug { get; set; }

        public Sprite Sprite { get; set; }

        public DropZone ToRow { get; set; }

        public Card(int id, string title, CardType type, Faction faction, string slug)
        {
            this.ID = id;
            this.Title = title;
            this.Type = type;
            this.CardFaction = faction;
            this.Slug = slug;
            this.Sprite = Resources.Load<Sprite>("Sprites/Cards/" + slug);
        }

        public abstract void OnDropEffect();

        public abstract void OnResurrect();

        public abstract string ConstructCardData();

        public string GetToRowName()
        {
            return ToRow.currentRow.CurrentRow.name;
        }

        public virtual Transform PlayEnemyCard()
        {
            string enemyRowName = "Enemy" + this.ToRow.name;
            this.ToRow = GameObject.Find(enemyRowName).GetComponent<DropZone>();

            OnDropEffect();

            return (GameObject.Find(enemyRowName).transform);
        }

        public override string ToString()
        {
            return string.Format("{0} , {1} , {2}, {3}, {4}", ID, Title, Type, Slug, ToRow);
        }
    }
}
