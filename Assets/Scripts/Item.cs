using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
  //Script is attached to each item (child elemnts of Shop panal)

  //Components
  private GameController gameController;
  private SoundManager soundManager;
  private Button myButton;

  //System
  public string ItemTitle;
  public int BaseCost;
  public int BaseCPSGain;
  public int BasePerTapGain;

  [HideInInspector]
  public int CPSAdditive = 0;
  [HideInInspector]
  public float CPSMultiplicative = 1.0f;
  [HideInInspector]
  public int PerTapAdditive = 0;
  [HideInInspector]
  public float PerTapMultiplicative = 1.0f;
  [HideInInspector]
  public float OverallMultiplicative = 1.0f;

  [HideInInspector]
  public static float CostGrowth = 1.15f;

  [HideInInspector]
  public int total;

  [HideInInspector]
  public int Count = 0;

  [HideInInspector]
  private bool isEnabled = true;

  //Texts
  private Text titleTxt;
  private Text countTxt;
  private Text costTxt;
  private Text descTxt;

  void Start()
  {
    Init();

    myButton.onClick.AddListener(() =>
    {
      Buy();
    });
  }

  void Init()
  {
    if (gameController == null)
    {
      gameController = GameObject.Find("System").GetComponent<GameController>();
    }

    soundManager = GameObject.Find("System").GetComponent<SoundManager>();
    myButton = GetComponent<Button>();

    titleTxt = transform.FindChild("Title").GetComponent<Text>();
    countTxt = transform.FindChild("Count").GetComponent<Text>();
    costTxt = transform.FindChild("Cost").GetComponent<Text>();
    descTxt = transform.FindChild("Desc").GetComponent<Text>();

    UpdateText();
  }

  bool Pay()
  {
    int Cost = GetCost();

    if (gameController.TotalCookies < Cost)
    {
      soundManager.PlayErrorSound();
      return false;
    }

    gameController.SpendCookies(Cost);
    return true;
  }

  public void Buy()
  {
    if (!Pay())
      return;

    Enable(false);
    Count++;
    Enable(true);
  }

  public void UpdateText()
  {
    titleTxt.text = ItemTitle;
    costTxt.text = GetCost().ToString();
    countTxt.text = Count.ToString();
    descTxt.text = "Each " + ItemTitle + " produce " + GetCPS() + " cookies per second\n";
    
    if (Count != 0)
    {
      descTxt.text += Count + " " + ItemTitle + " producing " + GetOverallCPS() + " ccokies per second";
    }
  }

  int GetCost()
  {
    return Mathf.FloorToInt(BaseCost * Mathf.Pow(CostGrowth, Count));
  }

  int GetCPS()
  {
    return Mathf.FloorToInt(
        (BaseCPSGain + CPSAdditive) * CPSMultiplicative
      );
  }

  int GetOverallCPS()
  {
    return Mathf.FloorToInt(
        GetCPS() * Count * OverallMultiplicative
      );
  }

  int GetPerTapGain()
  {
    return Mathf.FloorToInt(
        (BasePerTapGain + PerTapAdditive) * PerTapMultiplicative
      );
  }

  int GetOverallPerTapGain()
  {
    return Mathf.FloorToInt(
        GetPerTapGain() * Count * OverallMultiplicative
      );
  }
  
  public void Enable(bool _IsEnabled)
  {
    if (isEnabled == _IsEnabled)
      return;

    isEnabled = _IsEnabled;

    int sign = isEnabled ? 1 : -1;

    gameController.ChangeCPS(GetOverallCPS()        * sign);
    gameController.ChangeCPT(GetOverallPerTapGain() * sign);

    if (isEnabled)
      UpdateText();
  }
}
