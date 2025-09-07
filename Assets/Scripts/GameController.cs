using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    private void Awake()
    {
        Instance = this;
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
