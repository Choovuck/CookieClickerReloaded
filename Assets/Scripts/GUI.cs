using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUI : MonoBehaviour
{
  //  Script is attached to System GO

  //  Components
  public GameController gameController;
  private AudioSource[] audioSource;

  //  Texts
  public Text totalCookiesText;
  public Text perSecondText;

  //  GUI Windows
  public GameObject shop;
  public GameObject settings;
  public GameObject exit;
  public GameObject restart;

  //  Musics sprites
  public Sprite soundOnImg;
  public Sprite soundOffImg;
  public Sprite musicOnImg;
  public Sprite musicOffImg;

  void Start()
  {
    Init();
  }

  void Init()
  {
    gameController = GetComponent<GameController>();

    audioSource = FindObjectsOfType<AudioSource>() as AudioSource[];

    //  Deactivate Windows at start
    shop.SetActive(false);
    settings.SetActive(false);
    exit.SetActive(false);
  }

  public void OpenCloseShop()
  {
    CloseOtherWindows(shop);

    shop.SetActive(!shop.activeSelf);
  }

  public void OpenCloseSettings()
  {
    CloseOtherWindows(settings);
    
    settings.SetActive(!settings.activeSelf);
  }

  public void OpenCloseExit()
  {
    CloseOtherWindows(exit);
    
    exit.SetActive(!exit.activeSelf);
  }

  public void OpenCloseRestart()
  {
    CloseOtherWindows(restart);

    restart.SetActive(!restart.activeSelf);
  }

  public void ExitGameBtn()
  {
    Application.Quit();
    Debug.Log("Exit");
  }

  public void CloseOtherWindows(GameObject _windows)
  {
    foreach (var panel in GameObject.FindGameObjectsWithTag("Panel"))
      if (panel != _windows)
        panel.SetActive(false);
  }

  public void OnOffSound(Button soundBtn)
  {
    audioSource[0].enabled = !audioSource[0].enabled;

    var img = audioSource[0].enabled ? soundOnImg : soundOffImg;
    soundBtn.gameObject.GetComponent<Image>().sprite = img;
  }

  public void OnOffMusic(Button musicBtn)
  {
    audioSource[1].enabled = !audioSource[1].enabled;

    var img = audioSource[1].enabled ? musicOnImg : musicOffImg;
    musicBtn.gameObject.GetComponent<Image>().sprite = img;
  }

  public void RestartGame()
  {
    GameController.DestroySaves();
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  void Update()
  {
    UpdateText();

    if (Input.GetKeyDown(KeyCode.Escape))
      OpenCloseExit();
  }

  void UpdateText()
  {
    totalCookiesText.text = "Total cookies:\n " + Utils.ShortNumberString(gameController.TotalCookies);
    perSecondText.text = "Cookies per second:\n " + Utils.ShortNumberString(gameController.CookiesPerSecond);
  }
}
