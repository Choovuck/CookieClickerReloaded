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

  //  Texts
  protected Text titleTxt;
  protected Text costTxt;
  protected Text descTxt;

  void Start()
  {
    Init();

    myButton.onClick.AddListener(() =>
    {
      GainItem();
    });
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
    if (!Pay())
      return;

    item.Enable(false);
    Activate();
    item.Enable(true);

    myButton.interactable = false;
    paidImg.enabled = true;
  }

  public void SetTexts()
  {
    titleTxt.text = ItemTitle;
    costTxt.text = ItemCost.ToString();
    UpdateDescriptionText();
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
