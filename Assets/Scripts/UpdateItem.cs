using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateItem : MonoBehaviour
{
  //  Components
  public Item item;
  protected GameController gameController;
  protected SoundManager soundManager;
  protected Button myButton;
  protected Image paidImg;

  //  System
  public string ItemTitle;
  public int ItemCost;

  private bool isBought = false;

  //  Texts
  protected Text titleTxt;
  protected Text costTxt;
  protected Text descTxt;

  void Start()
  {
    Init();
  }

  void Init()
  {
    if (gameController == null)
    {
      gameController = GameObject.Find("System").GetComponent<GameController>();
    }

    soundManager = GameObject.Find("System").GetComponent<SoundManager>();

    titleTxt = transform.FindChild("Title").GetComponent<Text>();
    costTxt = transform.FindChild("Cost").GetComponent<Text>();
    descTxt = transform.FindChild("Desc").GetComponent<Text>();
    paidImg = transform.FindChild("PaidImg").GetComponent<Image>();

    paidImg.enabled = false;
    SetTexts();

    myButton = GetComponent<Button>();

    myButton.onClick.AddListener(() =>
    {
      GainItem();
    });

    if (isBought)
      OnBought();
  }

  bool Pay()
  {
    if (gameController.TotalCookies < ItemCost)
    {
      soundManager.PlayErrorSound();
      return false;
    }

    gameController.SpendCookies(ItemCost);
    return true;
  }

  void GainItem()
  {
    if (isBought || !Pay())
      return;

    if (item != null)
      item.Enable(false);

    Activate();

    if (item != null)
      item.Enable(true);

    isBought = true;

    OnBought();
  }

  void OnBought()
  {
    myButton.interactable = false;
    paidImg.enabled = true;
  }

  public void SetTexts()
  {
    titleTxt.text = ItemTitle;
    costTxt.text = ItemCost.ToString();
    UpdateDescriptionText();
  }

  public void Save(Serializer serializer)
  {
    serializer.Save(isBought);
  }

  public void Load(Deserializer deserializer)
  {
    if (!deserializer.IsValid)
      return;

    deserializer.Load(ref isBought);
  }

  protected virtual void UpdateDescriptionText()
  {
    // Empty
  }

  protected virtual void Activate()
  {
    // Empty
  }
}
