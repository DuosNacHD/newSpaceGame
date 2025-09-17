using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Playgama;
using Playgama.Modules.Advertisement;
public class MenuController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Score;
    void Start()
    {
        Score.text = "Highest Score: " + PlayerPrefs.GetFloat("Point").ToString();
        Bridge.advertisement.ShowBanner();
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
