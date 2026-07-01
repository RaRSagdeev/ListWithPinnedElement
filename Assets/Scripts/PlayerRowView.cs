using TMPro;
using UnityEngine;  
using UnityEngine.UI;


public class PlayerRowView : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text ratingText;

    [Header("Highlight")]
    [SerializeField] private Image background;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color localPlayerColor = Color.yellow;


    private RectTransform rectTransform;


    public PlayerModel Player { get; private set; }


    private void Awake()
    {
        rectTransform = (RectTransform)transform;
    }


    public void Bind(PlayerModel player)
    {
        Player = player;

        rankText.text = player.Rank.ToString();
        nameText.text = player.Name;
        ratingText.text = player.Rating.ToString("N0");


        if (background != null)
        {
            background.color = player.IsLocalPlayer ? localPlayerColor : normalColor;
        }
    }


    public void SetPosition(float y)
    {
        rectTransform.anchoredPosition = new Vector2(0, -y);
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
