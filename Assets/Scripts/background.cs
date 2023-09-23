using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{

    public float scroll_speed = 0.1f;

    private MeshRenderer mesh_renderer;

    private float y_scroll;

    // Start is called before the first frame update
    void Start()
    {
        mesh_renderer = GetComponent<MeshRenderer>();
    }

    void Scroll()
    {
        y_scroll = Time.time * scroll_speed;
        Vector2 offset = new Vector2(0f, y_scroll);
        mesh_renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }
}
