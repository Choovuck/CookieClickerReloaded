using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour {

    //Script is attached to each item (child elemnts of Shop panal)

    //Components
    private GameController gameController;
    private SoundManager soundManager;
    private Button myButton;            // reference to the this Button in Item


    //System
    public string ItemTitle;            //  The item's name
    public int ItemCost;                //  How much item costs
    public int CPSGain;                 //  How much points will be added to your current CPS
    public int PerTapGain;              //  How much points will be added for each tap
    public int coefficient;             //  The coefficent that affeccts item's cost
    [HideInInspector]
    public int total;
	[HideInInspector]
	public int ItemCount = 0;
    
    private int baseCost;
    

    //Texts
    private Text titleTxt;
    private Text countTxt;
    private Text costTxt;
    private Text descTxt;

    void Start() {

        Init();                         //  Initialize

        //  Add function to button
        myButton.onClick.AddListener(() =>
        {
            BuyItem();                  //  handle click here
        });

        baseCost = ItemCost;
    }

    void Init() {
        if (gameController == null) {
            gameController = GameObject.Find("System").GetComponent<GameController>();
        }

        soundManager = GameObject.Find("System").GetComponent<SoundManager>();
        myButton = GetComponent<Button>();

        titleTxt = transform.FindChild("Title").GetComponent<Text>();
        countTxt = transform.FindChild("Count").GetComponent<Text>();
        costTxt = transform.FindChild("Cost").GetComponent<Text>();
        descTxt = transform.FindChild("Desc").GetComponent<Text>();

        SetTexts();
    }

	public void BuyItem(){
        if (gameController.TotalCookies >= ItemCost)
        {
            gameController.TotalCookies -= ItemCost;
            gameController.CookiePerSecond += CPSGain;
            gameController.ClickPerTap += PerTapGain;
            ItemCount++;
            UpdateCost();
            SetTexts();

            Debug.Log("Successfully");
        }
        else 
        {
            soundManager.PlayErrorSound();
            Debug.Log("Not enough money");
        }
	}

    
    public void SetTexts() {
        
        
        if (ItemCount == 0)
        {
            titleTxt.text = ItemTitle;
            costTxt.text = ItemCost.ToString() + "$";
            countTxt.text = ItemCount.ToString();
            descTxt.text = "Each " + ItemTitle + " produce " + CPSGain + " cookies per second\n";
        }
        else 
        {
            total = ItemCount * CPSGain;
            titleTxt.text = ItemTitle;
            costTxt.text = ItemCost.ToString() + "$";
            countTxt.text = ItemCount.ToString();
            descTxt.text = "Each " + ItemTitle + " produce " + CPSGain + " cookies per second\n" + ItemCount + " " + ItemTitle + " producing " + total + " ccokies per second";
        }
    }

    void UpdateCost() {
        ItemCost = baseCost * coefficient;
        baseCost = ItemCost;
    }

}
