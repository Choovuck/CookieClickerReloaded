using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;

public class GameController : MonoBehaviour
{
  //Script is attached to System GO

  //  Components
  private SoundManager soundManager;

  //  Game 
  public float CookiesPerSecond;
  public float CookiesPerTap;
  public float TotalCookies;
  public float CookiesGenerationRate;

  private long LastSaveTime = 0;

  //  Systems
  public Vector2 newPos;
  private Vector2 oldPos;

  private GameObject CookieGO;
  private Button CookieBtn;
  private RectTransform CookieTransform;

  private Notification notification;

  void Start()
  {
    Init();
    Load();
    Simulate();
  }

  void Update()
  {
    TotalCookies += CookiesPerSecond * Time.deltaTime;
  }

  void Save()
  {
    if (!Directory.Exists("Save"))
      Directory.CreateDirectory("Save");

    var serializer = new Serializer("Save/game.dat");

    serializer
      .Save(TotalCookies)
      .Save(CookiesPerSecond)
      .Save(CookiesPerTap)
      .Save(CookiesGenerationRate)
      .Save(DateTime.Now.Ticks);

    Item[] items = Resources.FindObjectsOfTypeAll(typeof(Item)) as Item[];
    foreach (var item in items)
      item.Save(new Serializer("Save/item_" + item.ItemTitle + ".dat"));

    UpdateItem[] updateItems = Resources.FindObjectsOfTypeAll(typeof(UpdateItem)) as UpdateItem[];
    foreach (var item in updateItems)
      item.Save(new Serializer("Save/updateitem_" + item.ItemTitle + ".dat"));
  }

  void OnApplicationQuit()
  {
    Save();
  }

  void Load()
  {
    if (!Directory.Exists("Save"))
      return;

    var deserializer = new Deserializer("Save/game.dat");

    if (deserializer.IsValid)
      deserializer
        .Load(ref TotalCookies)
        .Load(ref CookiesPerSecond)
        .Load(ref CookiesPerTap)
        .Load(ref CookiesGenerationRate)
        .Load(ref LastSaveTime);

    Item[] items = Resources.FindObjectsOfTypeAll(typeof(Item)) as Item[];
    foreach (var item in items)
      item.Load(new Deserializer("Save/item_" + item.ItemTitle + ".dat"));

    UpdateItem[] updateItems = Resources.FindObjectsOfTypeAll(typeof(UpdateItem)) as UpdateItem[];
    foreach (var item in updateItems)
      item.Load(new Deserializer("Save/updateitem_" + item.ItemTitle + ".dat"));
  }

  public static void DestroySaves()
  {
    if (!Directory.Exists("Save"))
      return;

    Directory.Delete("Save", true);
  }

  void Simulate()
  {
    if (LastSaveTime <= 0)
      return;

    TimeSpan elapsed = DateTime.Now - new DateTime(LastSaveTime);
    float cookiesGain = (float)elapsed.TotalSeconds * CookiesPerSecond * CookiesGenerationRate;

    if (cookiesGain <= 0)
      return;

    ShowNotification(
        "While you were away, " + Utils.ShortNumberString(cookiesGain).ToString() + " cookies were baked!",
        5.0f
      );

    TotalCookies += cookiesGain;
  }

  public void ChangeGenerationRate(float change)
  {
    CookiesGenerationRate = Mathf.Clamp01(CookiesGenerationRate + change);
  }

  void ShowNotification(string text, float seconds)
  {
    notification.Show(text, seconds);
  }

  void Init()
  {
    //$$ testing
    test();

    // $$ this must execute before loading
    CookiesPerTap = 1;

    CookieGO = GameObject.FindGameObjectWithTag("Cookie");
    CookieTransform = CookieGO.GetComponent<RectTransform>();

    oldPos = CookieTransform.sizeDelta;

    soundManager = GetComponent<SoundManager>();
    CookieBtn = CookieGO.GetComponent<Button>();

    CookieBtn.onClick.AddListener(() =>
    {
      Click();
    });

    notification = GameObject.FindGameObjectWithTag("Notification").GetComponent<Notification>();
  }

  public void Click()
  {
    string str = "+" + CookiesPerTap.ToString();

    TotalCookies += CookiesPerTap;

    StartCoroutine("resize");
    soundManager.PlayClickSound();
    InstantiateText(str);

    foreach(var bg in GameObject.FindGameObjectsWithTag("RotatingBackground"))
    {
      var rbg = bg.GetComponent<RotationBG>();
      rbg.Push();
    }
  }

  void InstantiateText(string str)
  {
    float randomX = Input.mousePosition.x + UnityEngine.Random.Range(-20, 20);
    Vector3 pos = new Vector3(randomX, Input.mousePosition.y, Input.mousePosition.z);
    GameObject t = Instantiate(Resources.Load("GeneratedText", typeof(GameObject)), pos, Quaternion.identity) as GameObject;
    t.GetComponent<Text>().text = str;
    t.transform.SetParent(CookieGO.transform);
  }

  IEnumerator resize()
  {
    CookieTransform.sizeDelta = oldPos;
    yield return new WaitForSeconds(0.1f);
    CookieTransform.sizeDelta = newPos;
  }

  public void MouseEnter()
  {
    CookieTransform.sizeDelta = Vector2.Lerp(CookieTransform.sizeDelta, newPos, 3f);
  }

  public void MouseExit()
  {
    CookieTransform.sizeDelta = Vector2.Lerp(CookieTransform.sizeDelta, oldPos, 3f);
  }

  public void SpendCookies(float amount)
  {
    TotalCookies -= amount;
  }

  public void ChangeCPS(float value)
  {
    CookiesPerSecond += value;
  }

  public void ChangeCPT(float value)
  {
    CookiesPerTap += value;
  }

  // $$ remove
  void test()
  {
    
  }
}
