using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Vector2 parallaxEffectMultiplier;

    private float textureUnitSizeX;
    private Transform camTransform;
    private Vector3 lastCamTransform;

    private void Start()
    {
        camTransform = Camera.main.transform;
        lastCamTransform = camTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = camTransform.position - lastCamTransform;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCamTransform = camTransform.position;

        if(Mathf.Abs(camTransform.position.x  - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPosX = (camTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(camTransform.position.x + offsetPosX, transform.position.y);
        }
    }
}
