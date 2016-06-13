using UnityEngine;
using System.Collections;

public class ParticleFix : MonoBehaviour {

  public string sortingLayerName;
  public int sortingOrder;

  void Start () {
    var Renderer = GetComponent<ParticleSystem>().GetComponent<Renderer>();
    Renderer.sortingLayerName = sortingLayerName;
    Renderer.sortingOrder = sortingOrder;
  }
}
