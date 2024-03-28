using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderControls : MonoBehaviour
{
    [SerializeField]
    private Material materialDirt;
    [SerializeField]
    private Material materialGrass;
    [SerializeField]
    private float darknessLevel = 0.8f;
    private bool isLight = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isLight)
            {
                materialDirt.SetColor("_Color", new Color(darknessLevel, darknessLevel, darknessLevel));
                materialDirt.SetFloat("_Saturation", 0.5f);
                materialGrass.SetColor("_Color", new Color(darknessLevel, darknessLevel, darknessLevel));
                materialGrass.SetFloat("_Saturation", 0.5f);
            }
            else
            {
                materialDirt.SetColor("_Color", Color.white);
                materialDirt.SetFloat("_Saturation", 1f);
                materialGrass.SetColor("_Color", Color.white);
                materialGrass.SetFloat("_Saturation", 1f);
            }
            isLight = !isLight;
        }
    }
}
