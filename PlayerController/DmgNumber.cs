using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DmgNumber : MonoBehaviour
{
    public Text dmgText;
    public float lifeTime;
    public float moveSpeed;

    public float jitter = .5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
        transform.position = new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
    }

    public void setDmg(int dngAmount)
    {
        dmgText.text = dngAmount.ToString();
        transform.position += new Vector3(Random.Range(-jitter, jitter), Random.Range(-jitter, jitter), 0f);
    }
}
