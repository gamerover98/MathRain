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