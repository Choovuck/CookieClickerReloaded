﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
  //Script is attached to System GO

  //  Components
  private SoundManager soundManager;

  //  Game 
  public float CookiesPerSecond;
  public float CookiesPerTap;
  public float TotalCookies;

  //  Systems
  public Vector2 newPos;
  private Vector2 oldPos;

  //  CookieComponents
  private GameObject CookieGO;
  private Button CookieBtn;
  private RectTransform CookieTransform;

  void Start()
  {
    Init();
  }

  void Update()
  {
    TotalCookies += CookiesPerSecond * Time.deltaTime;
  }

  void Init()
  {
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
    float randomX = Input.mousePosition.x + Random.Range(-20, 20); //  - random range of x-axis of mouse
                                                                   // float randomY = Input.mousePosition.y + Random.Range(-10, 15);     - random range of y-axis of mouse
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
}
