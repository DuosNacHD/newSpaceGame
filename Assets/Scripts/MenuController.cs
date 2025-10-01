using Playgama;
using Playgama.Modules.Advertisement;
using Playgama.Modules.Platform;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Score;
    void Start()
    {
        Score.text = "Highest Score: " + PlayerPrefs.GetFloat("Point").ToString();
        Bridge.advertisement.ShowBanner();
        Bridge.platform.SendMessage(PlatformMessage.GameReady);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        Bridge.advertisement.ShowInterstitial();
        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
