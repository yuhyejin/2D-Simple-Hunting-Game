using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public const float backSpeed = 3.0f;
    private Material thisMaterial;

    void Start()
    {
        thisMaterial = GetComponent<Renderer>().material;
    }


    void Update()
    {
        float newOffsetX = thisMaterial.mainTextureOffset.x + backSpeed * Time.deltaTime;
        Vector2 newOffset = new Vector2(newOffsetX, 0);
        thisMaterial.mainTextureOffset = newOffset;
    }
}
