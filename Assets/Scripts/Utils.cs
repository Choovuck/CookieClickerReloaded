using UnityEngine;
using System.Collections.Generic;

public static class Utils
{
  private static Dictionary<int, string> postfixes = new Dictionary<int, string>
  {
    { 2, "million" },
    { 3, "billion" },
    { 4, "trillion" },
    { 5, "quadrillion" },
    { 6, "quintillion" },
    { 7, "sextillion" },
    { 8, "septillion" },
    { 9, "octillion" },
    { 10, "nonillion" },
    { 11, "decillion" },
    { 12, "undecillion" }
  };

  public static string ShortNumberString(float number)
  {
    float value = number;
    int   order = 0;

    while (value >= 1000.0f)
    {
      value /= 1000.0f;
      order++;
    }

    if (!postfixes.ContainsKey(order))
      return Mathf.FloorToInt(number).ToString();

    return value.ToString("G6") + " " + postfixes[order];
  }
}
