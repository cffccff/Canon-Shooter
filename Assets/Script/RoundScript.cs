using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI textScore;
   // public static int point;
    public bool isTargeted = false;
    public static int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        textScore = GameObject.Find("TextScore").GetComponent<TMPro.TextMeshProUGUI>();
        textScore.text = "Score: " + score.ToString();
     //   point = 0;
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    // called when the cube hits the floor
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "10point")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            score += 10;
            
        }
        if (collision.gameObject.tag == "20point")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            score += 20;

        }
        if (collision.gameObject.tag == "30point")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            score += 30;

        }
        textScore.text = "Score: " + score.ToString();
        DestroyGameObject();
    }
    void DestroyGameObject()
    {
        Destroy(gameObject);
    

    }
}
