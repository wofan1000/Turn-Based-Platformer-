using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    public Transform target;

    public Transform farBG, middleBG;

    private float lastX;
    private Vector2 lastPos;

    public float minHight, maxHight;

    public bool stopFollow;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        lastX = transform.position.x;
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopFollow)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

            //float clampY = Mathf.Clamp(transform.position.y, minHight, maxHight);
            // transform.position = new Vector3(transform.position.x, clampY, transform.position.z);

            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHight, maxHight), transform.position.z);

            // float amountToMoveX = transform.position.x - lastX;
            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            farBG.position = transform.position + new Vector3(amountToMove.x, 0f, 0f);
            middleBG.position += new Vector3(amountToMove.y, amountToMove.y, 0f) * .5f;

            // lastX = transform.position.x;
            lastPos = transform.position;
        }

    }
}
