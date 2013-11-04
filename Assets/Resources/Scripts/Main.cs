using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Main : MonoBehaviour {

    FCamObject camera;
    Player player;
    List<BaseGameObject> gameObjects = new List<BaseGameObject>();

    FContainer backgroundLayer;
    FContainer playerLayer;
    FContainer foregroundLayer;

	// Use this for initialization
	void Start () {
        FutileParams futileParams = new FutileParams(true, false, false, false);

        futileParams.AddResolutionLevel(160, 1.0f, 1.0f, "");
        futileParams.origin = new Vector2(0.5f, 0.5f);
        futileParams.backgroundColor = new Color(0.494f, 0.561f, 0.639f);
        Futile.instance.Init(futileParams);

        Futile.atlasManager.LoadAtlas("Atlases/atlasOne");
        Futile.atlasManager.LoadFont("gameFont", "fontOne_0", "Atlases/fontOne", 0, 0);

        backgroundLayer = new FContainer();
        playerLayer = new FContainer();
        foregroundLayer = new FContainer();
        
        
        FTmxMap tmxMap = new FTmxMap();


        tmxMap.LoadTMX("Maps/testMap");
        FTilemap tilemap = (FTilemap)(tmxMap.getLayerNamed("Tilemap"));
        FTilemap tilemapCollision = (FTilemap)(tmxMap.getLayerNamed("Meta"));
        backgroundLayer = new FContainer();
        backgroundLayer.AddChild(tilemap);
        backgroundLayer.AddChild(tilemapCollision);

        camera = new FCamObject();

        player = new Player();
        player.setTilemap(tilemapCollision);
        player.SetPosition(100, -100);
        playerLayer.AddChild(player);

        camera.follow(player);
        camera.setWorldBounds(new Rect(0, -tilemap.height, tilemap.width, tilemap.height));
        tmxMap.setClipNode(camera);

        FConvoLabel labelOne = new FConvoLabel("gameFont", "Hello! Making games is fun...\nAlso it took me way too\nlong to get this font to work\n           - Jif");
        labelOne.SetPosition(0, -50);
        camera.AddChild(labelOne);

        for (int x = 0; x < 10; x++)
        {
            Scientist s = new Scientist(RXRandom.Float() * tilemap.width, -RXRandom.Float() * tilemap.height);
            s.setTilemap(tilemapCollision);
            while (!BaseWalkingAnimSprite.isWalkable(tilemapCollision, s.x, s.y))
                s.SetPosition(RXRandom.Float() * tilemap.width, -RXRandom.Float() * tilemap.height);
            playerLayer.AddChild(s);
        }
        playerLayer.shouldSortByZ = true;
        Futile.stage.AddChild(backgroundLayer);
        Futile.stage.AddChild(playerLayer);
        Futile.stage.AddChild(foregroundLayer);
        Futile.stage.AddChild(camera);
	}
	
	// Update is called once per frame
    void Update()
    {
        
	}
}
