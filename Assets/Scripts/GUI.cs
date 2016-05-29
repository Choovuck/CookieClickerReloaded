using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    if (gameController == null)
    {
      gameController = GetComponent<GameController>();
    }

    if (shop == null)
    {
      shop = GameObject.Find("Shop");
    }

    if (settings == null)
    {
      settings = GameObject.Find("Settings");
    }

    if (exit == null)
    {
      exit = GameObject.Find("Exit");
    }

    if (soundOnImg == null || soundOffImg == null || musicOnImg == null || musicOffImg == null)
    {
      Debug.Log("Please attach soundon/off or musicon/off img");
    }

    audioSource = FindObjectsOfType<AudioSource>() as AudioSource[];

    //  Deactivate Windows at start
    shop.SetActive(false);
    settings.SetActive(false);
    exit.SetActive(false);
  }

  public void OpenCloseShop()
  {
    CloseOtherWindows(shop);

    bool temp = (shop.activeSelf == true) ? false : true;
    shop.SetActive(temp);
  }

  public void OpenCloseSettings()
  {
    CloseOtherWindows(settings);

    bool temp = (settings.activeSelf == true) ? false : true;
    settings.SetActive(temp);
  }

  public void OpenCloseExit()
  {
    CloseOtherWindows(exit);

    bool temp = (exit.activeSelf == true) ? false : true;
    exit.SetActive(temp);
  }

  public void ExitGameBtn()
  {
    Application.Quit();
    Debug.Log("Exit");
  }

  public void CloseOtherWindows(GameObject _windows)
  {
    var t = GameObject.FindGameObjectsWithTag("Panel");
    for (int i = 0; i < t.Length; i++)
    {
      if (t[i] == _windows)
      {
        continue;
      }
      t[i].SetActive(false);
    }
  }

  public void OnOffSound(Button soundBtn)
  {
    audioSource[0].enabled = !audioSource[0].enabled;

    if (audioSource[0].enabled == false)
    {
      soundBtn.gameObject.GetComponent<Image>().sprite = soundOffImg;
    }
    else
    {
      soundBtn.gameObject.GetComponent<Image>().sprite = soundOnImg;
    }
  }

  public void OnOffMusic(Button musicBtn)
  {
    audioSource[1].enabled = !audioSource[1].enabled;

    if (audioSource[1].enabled == false)
    {
      musicBtn.gameObject.GetComponent<Image>().sprite = musicOffImg;
    }
    else
    {
      musicBtn.gameObject.GetComponent<Image>().sprite = musicOnImg;
    }
  }

  void Update()
  {
    UpdateText();

    if (Input.GetKeyDown(KeyCode.Escape))
    {
      OpenCloseExit();
    }
  }

  void UpdateText()
  {
    totalCookiesText.text = gameController.TotalCookies.ToString();
    perSecondText.text = "CPS: " + gameController.CookiesPerSecond.ToString();
  }
}
