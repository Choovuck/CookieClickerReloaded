using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateItem : MonoBehaviour {

   
    //  Components
    public Item item;                           
    private GameController gameController;     
    private SoundManager soundManager;          
    private Button myButton;                    // reference to the this Button in Item
    private Image paidImg;                       

    //  System
    public string ItemTitle;                    //  The item upgrade's name
    public int ItemCost;                        //  How much item upgrade costs
    public int gain;                            //  How much CPS upgrade gains
    public int coefficient;                     //  The coefficent that affects item upgrade's cost

    //  Texts
    private Text titleTxt;
    private Text costTxt;
    private Text descTxt;

    void Start()
    {

        Init();

        //  Add GainItem function to the button
        myButton.onClick.AddListener(() =>
        {
            GainItem();
        });


    }

    //  Initialize method
    void Init() {

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


    void GainItem() {
        if (gameController.TotalCookies >= ItemCost)
        {
            int total = item.ItemCount * (item.CPSGain + gain) - item.total;
            gameController.TotalCookies -= ItemCost;
            item.CPSGain += gain;
            item.SetTexts();
            myButton.interactable = false;
            paidImg.enabled = true;
            gameController.CookiePerSecond += total;

            Debug.Log("Successfully");
        }
        else
        {
            soundManager.PlayErrorSound();
            Debug.Log("Not enough money");
        }
        
    }

    //  Refresh texts
    public void SetTexts()
    {
        titleTxt.text = ItemTitle;
        costTxt.text = ItemCost.ToString() + "$";
        descTxt.text = ItemTitle + "s gain +" + gain + " base CPS";
    }


}
