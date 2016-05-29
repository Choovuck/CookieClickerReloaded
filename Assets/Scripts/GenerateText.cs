using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GenerateText : MonoBehaviour {

    //  This script is attached to GeneratedText prefab
    //  Script generate text when user clicked to cookie

    //  System
    public float lifeTime;
    public float speed;

    void Start()
    {
        Invoke("DeleteText", lifeTime);
    }

    void DeleteText()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
