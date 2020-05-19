using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragNShoot_1 : MonoBehaviour
{
    public float power = 3;
    public Rigidbody[] rb;

    public Vector3 minPower;
    public Vector3 maxPower;

    public Camera mainCamera;
    public Camera cameraOne;
    bool cameraFlag = true;

    Camera cam;
    Vector3 force;
    Vector3 startPoint;
    Vector3 endPoint;

    //---------------------
    Vector3 originalPos;
    public Text HitCountLabel;
    int HitCountValue = 0;

    public Text ScoreLabel;
    int ScoreValue = 0;

    public int CollisionCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        HitCountValue = int.Parse(HitCountLabel.text);
        if (HitCountValue == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPoint = cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0.0f, 0.0f, 5.0f));
            }
            if (Input.GetMouseButtonUp(0))
            {
                originalPos = new Vector3(rb[HitCountValue].transform.position.x, rb[HitCountValue].transform.position.y, rb[HitCountValue].transform.position.z);
                //Debug.Log("originalPos: " + originalPos);

                endPoint = cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0.0f, 0.0f, 5.0f));

                force = new Vector3(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y), Mathf.Clamp(startPoint.z - endPoint.z, minPower.z, maxPower.z));

                rb[HitCountValue].useGravity = true;
                rb[HitCountValue].AddForce(force * power, ForceMode.Impulse);

                if (cameraFlag)
                {
                    cameraOne.enabled = true;
                    mainCamera.enabled = false;
                    cameraFlag = false;
                }

                StartCoroutine(DelayCount());
            }
        }
    }

    IEnumerator DelayCount()
    {
        // Add stop motion code to avoid this problem of ball moving after reset.
        yield return new WaitForSeconds(1);
        HitCountValue++;
        HitCountLabel.text = HitCountValue.ToString();
        rb[HitCountValue].transform.position = originalPos;
        if (!cameraFlag)
        {
            mainCamera.enabled = true;
            cameraOne.enabled = false;
            cameraFlag = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name != "RightPlane" && collision.gameObject.name != "Red_1" && collision.gameObject.name != "Red_I_1" && collision.gameObject.name != "Red_T_1" && 
            collision.gameObject.name != "Red_T_2" && CollisionCount < 3)
        {
            CollisionCount++;
            //Debug.Log("Colision With: " + collision.gameObject.name);
            Debug.Log(" +50 ");
            ScoreValue = int.Parse(ScoreLabel.text);
            ScoreValue += 50;
            ScoreLabel.text = ScoreValue.ToString();
            Destroy(collision.gameObject);
        }
    }
}
