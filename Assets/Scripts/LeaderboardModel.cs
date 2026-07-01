using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderboardModel
{
    public readonly List<PlayerModel> Players = new();

    public PlayerModel LocalPlayer =>
            Players.FirstOrDefault(p => p.IsLocalPlayer);

    public PlayerModel GetPlayerByRank(int rank)
    {
        return Players.FirstOrDefault(p => p.Rank == rank);
    }
}
