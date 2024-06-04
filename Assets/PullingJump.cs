using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PullingJump : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 clickPosition;
    private float jumpPower = 8;
    private bool isCanJump;

   void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        // 重力の設定
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;
        }

        // 床にいたらジャンプ可能
        if (isCanJump&&Input.GetMouseButtonUp(0))
        {
            Vector3 dist = clickPosition - Input.mousePosition;
            if(dist.sqrMagnitude == 0) { return; }
            rb.velocity = dist.normalized * jumpPower;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // 衝突している点の情報が複数格納されている
        ContactPoint[] contacts = collision.contacts;
        // 0番目の衝突情報から、衝突している点の法線を取得
        Vector3 otherNormal = contacts[0].normal;
        // 上方向を示すベクトル、長さは1
        Vector3 upVector = new Vector3(0, 1, 0);
        // 上方向と法線の内積。2つのベクトルはともに長さが1なので、cosθの結果がdotUN変数に入る
        float dotUN = Vector3.Dot(upVector, otherNormal);
        // 内積値に逆三角関数arccosを掛けて角度を算出。それを度数法へ変換する
        float dotDeg = Mathf.Acos(dotUN) * Mathf.Rad2Deg;
        // 2つのベクトルがなす角度が45度より小さければ再びジャンプ可能とする
        if(dotDeg <= 45)
        {
            isCanJump = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("離脱した");
        isCanJump = false;
    }
}
