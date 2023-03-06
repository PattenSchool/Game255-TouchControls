using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTracker : MonoBehaviour
{
    public TMPro.TMP_Text txtState;
    public TMPro.TMP_Text txtPosition;
    public TMPro.TMP_Text txtTaps;

    public float lastDistance = -1f;

    public float minChange = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    txtState.text = "Phase is begin";
                    break;
                case TouchPhase.Moved:
                    txtState.text = "Phase is moved";
                    break;
                case TouchPhase.Stationary:
                    txtState.text = "Phase is stationary";
                    break;
                case TouchPhase.Ended:
                    txtState.text = "Phase is ended";
                    break;
                case TouchPhase.Canceled:
                    txtState.text = "Phase is Canceled";
                    break;
            }

            //txtPosition.text = string.Format($"X: {touch.position.x} Y: {touch.position.y}");
            txtPosition.text = string.Format($"X: {touch.deltaPosition.x} Y: {touch.deltaPosition.y}");
            txtTaps.text = string.Format($"Tap Count: {touch.tapCount}");
        }

        if (Input.touchCount == 2)
        {
            if (lastDistance == -1)
            {
                lastDistance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
            else
            {
                float currentDistance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                if ((lastDistance - currentDistance) > minChange)
                {
                    // do pinch
                    Camera.main.orthographicSize *= (0.1f * Time.deltaTime);
                    // Inaccurate due to frame rate

                    if (Camera.main.orthographicSize < 1)
                    {
                        Camera.main.orthographicSize = 1;
                    }
                }
                lastDistance= currentDistance;
            }
        }
        else
        {
            lastDistance = -1f;
        }
    }
}
