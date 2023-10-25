using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocity = 3.0f;

    public Vector2 direction = Vector2.left;

    private Transform pTransform;
    // Start is called before the first frame update

    private void Start()
    {
        pTransform = FindObjectOfType<PlayerController>().gameObject.transform;
        if(pTransform == null)
        {
            Destroy(this.gameObject);

        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        


        pos.y += Time.deltaTime * velocity * direction.y;
        pos.x += Time.deltaTime * velocity * direction.x;
        this.transform.position = pos;


        if(Vector3.Distance(pTransform.position,this.transform.position) > 30)
        {
            //suicide
            Destroy(this.gameObject);
        }

    }


}
