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
  }

  public void Save(Serializer serializer)
  {
    serializer
      .Save(CPSAdditive)
      .Save(CPSMultiplicative)
      .Save(PerTapAdditive)
      .Save(PerTapMultiplicative)
      .Save(OverallMultiplicative)
      .Save(Count);
  }

  public void Load(Deserializer deserializer)
  {
    if (!deserializer.IsValid)
      return;

    deserializer
      .Load(ref CPSAdditive)
      .Load(ref CPSMultiplicative)
      .Load(ref PerTapAdditive)
      .Load(ref PerTapMultiplicative)
      .Load(ref OverallMultiplicative)
      .Load(ref Count);
  }

  void Init()
  {
    if (gameController == null)
    {
      gameController = GameObject.Find("System").GetComponent<GameController>();
    }

    soundManager = GameObject.Find("System").GetComponent<SoundManager>();

    myButton = GetComponent<Button>();
    myButton.onClick.AddListener(() =>
    {
      Buy();
    });

    titleTxt = transform.FindChild("Title").GetComponent<Text>();
    countTxt = transform.FindChild("Count").GetComponent<Text>();
    costTxt = transform.FindChild("Cost").GetComponent<Text>();
    descTxt = transform.FindChild("Desc").GetComponent<Text>();

    UpdateText();
  }

  bool Pay()
  {
    float Cost = GetCost();

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
    costTxt.text = "Cost:\n" + Utils.ShortNumberString(GetCost());
    countTxt.text = "Owned:\n" + Count.ToString();
    descTxt.text = "Each produces " + Utils.ShortNumberString(GetCPS()) + " cookies/s\n";
    
    if (Count != 0)
    {
      descTxt.text += "Overall producing " + Utils.ShortNumberString(GetOverallCPS()) + " cookies/s";
    }
  }

  float GetCost()
  {
    return BaseCost * Mathf.Pow(CostGrowth, Count);
  }

  float GetCPS()
  {
    return (BaseCPSGain + CPSAdditive) * CPSMultiplicative;
  }

  float GetOverallCPS()
  {
    return GetCPS() * Count * OverallMultiplicative;
  }

  float GetPerTapGain()
  {
    return (BasePerTapGain + PerTapAdditive) * PerTapMultiplicative;
  }

  float GetOverallPerTapGain()
  {
    return GetPerTapGain() * Count * OverallMultiplicative;
  }
  
  public void Enable(bool _IsEnabled)
  {
    if (isEnabled == _IsEnabled)
      return;

    isEnabled = _IsEnabled;

    float sign = isEnabled ? 1.0f : -1.0f;

    gameController.ChangeCPS(GetOverallCPS()        * sign);
    gameController.ChangeCPT(GetOverallPerTapGain() * sign);

    if (isEnabled)
      UpdateText();
  }
}
