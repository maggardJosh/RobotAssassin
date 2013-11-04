using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    FCamObject camera;
    Player player;
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
        FTilemap tilemapTiles = (FTilemap)(tilemap.getLayerNamed("Tilemap"));

        Futile.stage.AddChild(tilemap);

        camera = new FCamObject();

        player = new Player();
        player.SetPosition(100, -100);
        Futile.stage.AddChild(player);

        camera.follow(player);
        camera.setWorldBounds(new Rect(0, -tilemapTiles.height, tilemapTiles.width, tilemapTiles.height));
        tilemap.setClipNode(camera);

        for (int x = 0; x < 10; x++)
        {
            Scientist s = new Scientist(RXRandom.Float() * 200, -RXRandom.Float() * 200);
            Futile.stage.AddChild(s);
        }

        Futile.stage.AddChild(camera);
	}
	
	// Update is called once per frame
    void Update()
    {

        
	}
}
