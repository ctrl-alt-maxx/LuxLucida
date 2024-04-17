using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Transform))]
public class player3DAsset : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Vector3 positionAdjuster;
    private Vector3 lookingDirection = Vector3.right;
    
    private Transform player3DAssetTransform;

    // Start is called before the first frame update
    void Start()
    {
        player3DAssetTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float controlX = Input.GetAxis("Horizontal");
        lookingDirection = controlX < -0.001 ? Vector3.left : lookingDirection;
        lookingDirection = controlX > 0.001 ? Vector3.right : lookingDirection;
        player3DAssetTransform.position = playerTransform.localPosition + positionAdjuster;
        player3DAssetTransform.rotation = Quaternion.LookRotation(lookingDirection);
    }
}
