using System;
using System.Collections.Generic;
using System.Linq;

public class LeaderboardService
{
    private readonly Random random = new();

    public LeaderboardModel CreateTestLeaderboard(int playerCount, int localPlayerIndex)
    {
        LeaderboardModel leaderboard = new();

        for (int i = 0; i < playerCount; i++)
        {
            PlayerModel player = new(
                id: i,
                name: $"Player_{i + 1}",
                rating: random.Next(1000, 50000),
                isLocalPlayer: i == localPlayerIndex
            );

            leaderboard.Players.Add(player);
        }

        return leaderboard;
    }


    public void Sort(LeaderboardModel leaderboard)
    {
        leaderboard.Players.Sort((a, b) => b.Rating.CompareTo(a.Rating));

        for (int i = 0; i < leaderboard.Players.Count; i++)
        {
            leaderboard.Players[i].Rank = i + 1;
        }
    }
}
