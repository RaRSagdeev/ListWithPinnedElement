using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public int Id;
    public string Name;
    public int Rating;
    public int Rank;
    public bool IsLocalPlayer;

    public PlayerModel(int id, string name, int rating, bool isLocalPlayer = false)
    {
        Id = id;
        Name = name;
        Rating = rating;
        IsLocalPlayer = isLocalPlayer;
    }
}
