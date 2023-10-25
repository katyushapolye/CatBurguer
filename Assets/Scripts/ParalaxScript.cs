using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxScript : MonoBehaviour
{
    [SerializeField]
    private GameObject midLayerPanel;
    private Material midLayer;

    [SerializeField]
    private GameObject frontLayerPanel;
    private Material frontLayer;
    private float frontOffset = 0;
    private float midOffset = 0;
    [SerializeField]
    private Transform target;

    //Little trick

    float oldPos = 0;
    float newPos = 0;





    // Start is called before the first frame update
    void Start()
    {
        oldPos = target.position.x;

        midLayer = midLayerPanel.GetComponent<MeshRenderer>().material;
        frontLayer = frontLayerPanel.GetComponent<MeshRenderer>().material;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        newPos = target.transform.position.x;

        float pSpeed = (newPos - oldPos) / Time.deltaTime; //hehe

        frontOffset += pSpeed * Time.deltaTime * 0.01f;

        midOffset += (pSpeed * Time.deltaTime * 0.01f) / 2;

        frontLayer.SetTextureOffset("_MainTex", new Vector3(frontOffset, 0));


        midLayer.SetTextureOffset("_MainTex", new Vector3(midOffset, 0));


        oldPos = newPos;



    }
}
