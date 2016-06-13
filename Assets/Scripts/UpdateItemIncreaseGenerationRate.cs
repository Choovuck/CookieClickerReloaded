using UnityEngine;
using System.Collections;

public class UpdateItemIncreaseGenerationRate : UpdateItem
{
  public float RateIncrease = 0;

  protected override void UpdateDescriptionText()
  {
    base.UpdateDescriptionText();

    descTxt.text = "Generate " + RateIncrease.ToString() + "% more cookies while away.";
  }

  protected override void Activate()
  {
    base.Activate();

    gameController.ChangeGenerationRate(RateIncrease);
  }
}
