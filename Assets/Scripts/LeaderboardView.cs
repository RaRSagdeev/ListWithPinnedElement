using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardView : MonoBehaviour
{
    [Header("Scroll")]
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _viewport;
    [SerializeField] private RectTransform _content;

    [Header("Rows")]
    [SerializeField] private PlayerRowView _rowPrefab;
    [SerializeField] private float _rowHeight = 80f;

    [Header("Pinned")]
    [SerializeField] private PlayerRowView _pinnedTop;
    [SerializeField] private PlayerRowView _pinnedBottom;


    private readonly List<PlayerRowView> _rows = new();
    private List<PlayerModel> _players;

    private int _firstVisibleIndex;
    private int _visibleRowCount;
    private int _visibleViewportCount;

    public void Initialize(List<PlayerModel> playerList)
    {
        _players = playerList;

        CalculateContentHeight();
        CreatePool();

        _scrollRect.onValueChanged.AddListener(OnScroll);

        Refresh();
    }


    private void CalculateContentHeight()
    {
        float height = _players.Count * _rowHeight;

        _content.sizeDelta = new Vector2(_content.sizeDelta.x, height);
    }


    private void CreatePool()
    {
        _visibleViewportCount = Mathf.CeilToInt(_viewport.rect.height / _rowHeight);
        _visibleRowCount = _visibleViewportCount + 4;

        for (int i = 0; i < _visibleRowCount; i++)
        {
            PlayerRowView row = Instantiate(_rowPrefab, _content, false);
            row.Hide();
            _rows.Add(row);
        }
    }


    private void OnScroll(Vector2 value)
    {
        Refresh();
    }


    private void Refresh()
    {
        if (_players == null) return;

        float scrollY = _content.anchoredPosition.y;
        _firstVisibleIndex = Mathf.FloorToInt(scrollY / _rowHeight);

        _firstVisibleIndex = Mathf.Clamp(
            _firstVisibleIndex,
            0,
            Mathf.Max(
                0,
                _players.Count - _visibleRowCount));

        for (int i = 0; i < _rows.Count; i++)
        {
            int playerIndex = _firstVisibleIndex + i;
            PlayerRowView row = _rows[i];

            if (playerIndex >= _players.Count)
            {
                row.Hide();
                continue;
            }

            PlayerModel player = _players[playerIndex];
            row.Bind(player);
            row.SetPosition( playerIndex * _rowHeight);
            row.Show();
        }

        UpdatePinned();
    }


    private void UpdatePinned()
    {
        PlayerModel localPlayer = _players.Find(p => p.IsLocalPlayer);

        if (localPlayer == null) return;

        int localIndex = _players.IndexOf(localPlayer);
        int lastVisible = _firstVisibleIndex + _visibleViewportCount - 1;

        if (localIndex < _firstVisibleIndex)
        {
            _pinnedTop.Bind(localPlayer);
            _pinnedTop.Show();
            _pinnedBottom.Hide();
        }
        else if (localIndex > lastVisible)
        {
            _pinnedBottom.Bind(localPlayer);
            _pinnedBottom.Show();
            _pinnedTop.Hide();
        }
        else
        {
            _pinnedTop.Hide();
            _pinnedBottom.Hide();
        }
    }


    public void ScrollToLocalPlayer()
    {
        PlayerModel localPlayer = _players.Find(p => p.IsLocalPlayer);
        if (localPlayer == null)
            return;

        int index = _players.IndexOf(localPlayer);
        float viewportHeight = _viewport.rect.height;
        float target = (index * _rowHeight) - (viewportHeight / 2) + (_rowHeight / 2);

        target = Mathf.Clamp(
            target,
            0,
            _content.rect.height - viewportHeight);

        _content.anchoredPosition = new Vector2(_content.anchoredPosition.x, target);

        Refresh();
    }
}