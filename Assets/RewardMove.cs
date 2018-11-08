using UnityEngine;
using System.Collections;

public class RewardMove : MonoBehaviour
{
    private float offest;
    private float speed;
    private float parm;
    void Start()
    {
        offest = transform.position.x - Camera.main.transform.position.x;
        if (offest<0)
        {
            parm = Mathf.Abs(offest)/3;
            speed = 2* parm;
        }
        else
        {
            parm = Mathf.Abs(offest) / 3;
            speed = -2 * parm;
        }
        print(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right*speed*Time.deltaTime);
        if (transform.position.y<-(Screen.height/200))
        {
            PoolManager.Instance.Delete(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name=="Knife")
        {
            if(transform.name== "RewardBomb(Clone)")
            {
                return;
            }
            RewardManager.hitcount++;
            PoolManager.Instance.Delete(gameObject);
        }
    }
}
