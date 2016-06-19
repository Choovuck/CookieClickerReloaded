using UnityEngine;
using System.Collections;

public class UpdateItemMultiplyBaseCPS : UpdateItem
{
  public float Multiplier = 1.0f;

  protected override void UpdateDescriptionText()
  {
    base.UpdateDescriptionText();

    descTxt.text = item.ItemTitle + " base CPS multiplies by " + (Multiplier + 1.0f).ToString();
  }

  protected override void Activate()
  {
    base.Activate();

    item.CPSMultiplicative += Multiplier;
  }
}
