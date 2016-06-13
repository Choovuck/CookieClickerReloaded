using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Notification : MonoBehaviour {

  public Text notificationText;
  private Image img;

  void Awake()
  {
    img = GetComponent<Image>();
  }

  public void Show(string text, float seconds)
  {
    notificationText.text = text;
    StartCoroutine(ShowForSeconds(seconds));
  }

  private IEnumerator ShowForSeconds(float seconds)
  {
    img.enabled = true;
    notificationText.enabled = true;

    yield return new WaitForSeconds(seconds);

    img.enabled = false;
    notificationText.enabled = false;
  }
}
