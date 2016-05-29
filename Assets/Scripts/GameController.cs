using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class GameController : MonoBehaviour
{

  //Script is attached to System GO

  //  Components
  private SoundManager soundManager;

  //  Game 
  public int CookiePerSecond;                         //  Give cookie per second
  public int ClickPerTap;                             //  Give cookie per tap
  public int TotalCookies;                            //  Total cookie (current)

  //  Systems
  public Vector2 newPos;                              //  New position to Scale Cookie
  private Vector2 oldPos;

  //  CookieComponents
  private GameObject CookieGO;
  private Button CookieBtn;
  private RectTransform CookieTransform;

  void Start()
  {
    Init();                                         //  Initialize
    InvokeRepeating("GiveCookiePerSecond", 0, 1);   //  Start give cookie each second
  }

  void Update()
  {
    // Empty 
  }

  void Init()
  {
    CookieGO = GameObject.FindGameObjectWithTag("Cookie");
    CookieTransform = CookieGO.GetComponent<RectTransform>();

    oldPos = CookieTransform.sizeDelta;

    soundManager = GetComponent<SoundManager>();
    CookieBtn = CookieGO.GetComponent<Button>();

    CookieBtn.onClick.AddListener(() =>
    {
      Click();  // handle click here
    });
  }
  
  //  Click function handler
  public void Click()
  {
    string str = ClickPerTap.ToString() + "x";

    TotalCookies += ClickPerTap;

    StartCoroutine("resize");
    soundManager.PlayClickSound();
    InstantiateText(str);
  }


  //  Instantiate text
  void InstantiateText(string str)
  {
    float randomX = Input.mousePosition.x + Random.Range(-20, 20); //  - random range of x-axis of mouse
                                                                   // float randomY = Input.mousePosition.y + Random.Range(-10, 15);     - random range of y-axis of mouse
    Vector3 pos = new Vector3(randomX, Input.mousePosition.y, Input.mousePosition.z);
    GameObject t = Instantiate(Resources.Load("GeneratedText", typeof(GameObject)), pos, Quaternion.identity) as GameObject;
    t.GetComponent<Text>().text = str;
    t.transform.parent = CookieGO.transform;
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

  void GiveCookiePerSecond()
  {
    TotalCookies += CookiePerSecond; 
  }
}
