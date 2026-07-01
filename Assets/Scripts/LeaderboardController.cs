using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private LeaderboardView leaderboardView;

    private LeaderboardService service;

    private void Awake()
    {
        service = new LeaderboardService();
    }

    private void Start()
    {
        Debug.Log("Started leaderboard");

        LeaderboardModel model = service.CreateTestLeaderboard(1000, 22);

        service.Sort(model);
        leaderboardView.Initialize(model.Players);
    }
}
