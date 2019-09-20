using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class TouchMgr : MonoBehaviour
{
    private Camera arCamera;
    public GameObject arroom;
    //public Text measureText;

    private Vector3 firstPos;
    private Vector3 secondPos;
    private bool isFirstTouch = false;

    int t = 0;

    // Start is called before the first frame update
    void Start()
    {
        arCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;

        //첫번째 손가락 터치
        Touch touch = Input.GetTouch(0);

        //터치를 시작했을 때 여부를 판단
        if (touch.phase == TouchPhase.Began)
        {
            //레이캐스트의 충돌정보를 저장할 구조체
            TrackableHit hit;

            //레이캐스트 반응할 대상
            TrackableHitFlags flag = TrackableHitFlags.PlaneWithinPolygon
                                   | TrackableHitFlags.FeaturePointWithSurfaceNormal;

            //ARCore 전용 레이캐스트 (터치x좌표, 터치y좌표, 검출대상레이어, 결과값)
            if (Frame.Raycast(touch.position.x, touch.position.y, flag, out hit) && t < 1)
            {
                //3D 좌표를 기억하는 객체
                Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);
                GameObject obj = Instantiate(arroom, hit.Pose.position, Quaternion.identity, anchor.transform);
                t++;
                obj.transform.rotation = Quaternion.Euler(arCamera.transform.position.x,
                                                          obj.transform.position.y,
                                                          arCamera.transform.position.z);
                //if (isFirstTouch == false)
                //{
                //    firstPos = anchor.transform.position;
                //    isFirstTouch = true;
                //}
                //else
                //{
                //    secondPos = anchor.transform.position;
                //    isFirstTouch = false;
                //}
                //float dist = Vector3.Distance(firstPos, secondPos) * 100.0f;
                //measureText.text = dist.ToString("00.##");

            }
        }
    }
}