using Playgama;
using Playgama.Modules.Platform;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Bridge.platform.SendMessage(PlatformMessage.GameReady);
    }

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField]Image barImage;
    public void SetHealtBarVisual(float Ratio)
    {
        barImage.fillAmount = Ratio;
    }
    public void SetScoreVisual(float Score)
    {
        scoreText.text = Score.ToString();
    }
}
