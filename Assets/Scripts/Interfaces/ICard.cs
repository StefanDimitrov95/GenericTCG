using Assets.Scripts.Classes.EnumClasses;

public interface ICard
{
        int ID { get; set; }

        string Title { get; set; }

        CardType Type { get; set; }

        string Slug { get; set; }
}