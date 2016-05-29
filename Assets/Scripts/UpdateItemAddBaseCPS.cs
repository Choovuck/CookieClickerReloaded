using UnityEngine;
using System.Collections;

public class UpdateItemAddBaseCPS : UpdateItem
{
  public int CPSGain = 0;

  protected override void UpdateDescriptionText()
  {
    base.UpdateDescriptionText();

    descTxt.text = item.ItemTitle + " gain +" + CPSGain + " base CPS";
  }

  protected override void Activate()
  {
    base.Activate();

    item.CPSAdditive += CPSGain;
  }
}
