using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

[Serializable]
public class Scoreboard
{
    public List<Game> games = new();

    public void AddGame(Game game) => games.Add(game);

    public int GetMaxScore() => games.Max(game => game.score);
    
    public List<Game> GetGamesSortedByScoreDescending()
    {
        var sortedGames = new List<Game>(games);
        sortedGames.Sort((g1, g2) => g2.score.CompareTo(g1.score));
        return sortedGames;
    }
}

[Serializable]
public class Game
{
    public string dateTime;
    public int score;

    public Game(int score)
    {
        dateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        this.score = score;
    }
}