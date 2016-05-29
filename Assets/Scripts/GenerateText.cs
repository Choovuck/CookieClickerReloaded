using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GenerateText : MonoBehaviour {

    //  This script is attached to GeneratedText prefab
    //  Script generate text when user clicked to cookie

    //  Components
    Text text;
    GameController gc;


    //  System
    public float lifeTime;
    public float speed;

    void Start()
    {
        Init();
        Invoke("DeleteText", lifeTime);
    }

    void Init()
    {
        gc = GameObject.Find("System").GetComponent<GameController>();
        text = GetComponent<Text>();
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
