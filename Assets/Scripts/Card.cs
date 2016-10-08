using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Card : ICard
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Slug { get; set; }

        public Sprite Sprite { get; set; }

        private string Path { get; set; }
        public Card(int id, string title, string type, string slug)
        {
            this.ID = id;
            this.Title = title;
            this.Type = type;
            this.Slug = slug;
            this.Sprite = Resources.Load<Sprite>("Sprites/Cards/" + slug);
            Path = "Images/Cards/" + slug;
        }
        public override string ToString()
        {
            return string.Format("{0} , {1} , {2}, {3}, {4}", ID, Title, Type, Slug, Path);
        }
    }
}
