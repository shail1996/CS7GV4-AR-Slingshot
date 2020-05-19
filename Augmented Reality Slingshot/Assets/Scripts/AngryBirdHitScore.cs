using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngryBirdHitScore : MonoBehaviour
{
    public Text ScoreLabel;
    int ScoreValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        ScoreValue = int.Parse(ScoreLabel.text);
        if (collision.gameObject.name != "RightPlane" && ScoreValue > 0)
        {
            Debug.Log(" +100 ");
            ScoreValue += 100;
            ScoreLabel.text = ScoreValue.ToString();
            //StartCoroutine(DelayCount(collision));
            Destroy(gameObject);
        }

        if ((collision.gameObject.name == "Bomb" || collision.gameObject.name == "Matilda" || collision.gameObject.name == "Chuck") && ScoreValue > 0)
        {
            Debug.Log(" +200 ");
            ScoreValue += 200;
            ScoreLabel.text = ScoreValue.ToString();
            //StartCoroutine(DelayCount(collision));
            Destroy(gameObject);
        }
    }

    IEnumerator DelayCount(Collision collision1)
    {
        yield return new WaitForSeconds(5);
        Destroy(collision1.gameObject);
    }

}
