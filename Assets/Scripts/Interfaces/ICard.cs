using Assets.Scripts;

public interface ICard
{
        int ID { get; set; }

        string Title { get; set; }

        CardType Type { get; set; }

        string Slug { get; set; }
}