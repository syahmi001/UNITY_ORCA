using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour {
    public Vector3 Point1, Point2;
    public float waitTime1;
    public float waitTime2;
    public float speed = 10f;
    private bool moving = true;
    public float smooth = 1f;
    //private Vector3 targetAngles;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
            moving = false;
            StopAllCoroutines();
            StartCoroutine(Rotating(waitTime2));
            StartCoroutine(MoveToNextPoint(waitTime1));

        }
    }

    private IEnumerator Rotating(float waitTime2) {
        if (moving==false) // some condition to rotate 180
            //targetAngles = transform.eulerAngles + 180f * Vector3.up; // what the new angles should be
            transform.RotateAround(transform.position, transform.up, 180f);
            //transform.Rotate(0, Time.deltaTime * 30, 0, Space.Self);

        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime); // lerp to new angles
        yield return new WaitForSeconds(.05f);
        yield return new WaitForSeconds(waitTime2);
    }

    private IEnumerator MoveToNextPoint(float waitTime1)
    {
        float distOne = Vector3.Distance(transform.position, Point1);
        float distTwo = Vector3.Distance(transform.position, Point2);
        Vector3 dest = distOne > distTwo ? Point1 : Point2;
        while (Vector3.Distance(transform.position, dest) >= .5f)
        {
            transform.position += (dest - transform.position).normalized * Time.deltaTime * speed;
            yield return new WaitForSeconds(.05f);
        }
        yield return new WaitForSeconds(waitTime1);
        moving = true;
    }


}
