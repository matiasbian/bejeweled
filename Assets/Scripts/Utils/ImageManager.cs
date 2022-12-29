using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    static ImageManager self;

    public Sprite[] icons;
    public Color[] colors;
    // Start is called before the first frame update
    public static ImageManager Get () {
        if (!self) self = GameObject.FindObjectOfType<ImageManager>();
        return self;
    }
}
