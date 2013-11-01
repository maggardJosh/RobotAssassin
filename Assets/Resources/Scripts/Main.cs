using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    FCamObject camera;
	// Use this for initialization
	void Start () {
        FutileParams futileParams = new FutileParams(true, false, false, false);

        futileParams.AddResolutionLevel(160, 1.0f, 1.0f, "");
        futileParams.origin = new Vector2(0.5f, 0.5f);
        futileParams.backgroundColor = new Color(0.494f, 0.561f, 0.639f);
        Futile.instance.Init(futileParams);

        Futile.atlasManager.LoadAtlas("Atlases/atlasOne");
        FTmxMap tilemap = new FTmxMap();

        tilemap.LoadTMX("Maps/testMap");

        Futile.stage.AddChild(tilemap);

        camera = new FCamObject();

        Futile.stage.AddChild(camera);

	}
	
	// Update is called once per frame
    void Update()
    {

        if (UnityEngine.Input.GetKey(KeyCode.W))
            camera.y += 1;
        if (UnityEngine.Input.GetKey(KeyCode.S))
            camera.y -= 1;
        if (UnityEngine.Input.GetKey(KeyCode.D))
            camera.x += 1;
        if (UnityEngine.Input.GetKey(KeyCode.A))
            camera.x -= 1;
	}
}
