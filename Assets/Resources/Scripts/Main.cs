using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class Main : MonoBehaviour
{

    FCamObject camera;
    Player player;
    List<BaseGameObject> gameObjects = new List<BaseGameObject>();
    FContainer backgroundLayer;
    FContainer playerLayer;
    FContainer foregroundLayer;

    List<FNode> spawnPoints = new List<FNode>();
    List<WarpPoint> warpPoints = new List<WarpPoint>();

    // Use this for initialization
    void Start()
    {
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

        GlitchManager.getInstance();       

        camera = new FCamObject();
        player = new Player();

        loadMap("testMap");

        playerLayer.AddChild(player);
        camera.follow(player);

        FConvoLabel labelOne = new FConvoLabel("gameFont", "I will destroy everyone with\nmy floating sword of DOOOM!\n          - Jif");
        labelOne.SetPosition(0, -50);
        camera.AddChild(labelOne);
        
        playerLayer.shouldSortByZ = true;

        Futile.stage.AddChild(backgroundLayer);
        Futile.stage.AddChild(playerLayer);
        Futile.stage.AddChild(foregroundLayer);
        Futile.stage.AddChild(camera);
    }

    private void loadMap(string mapName)
    {
        warpPoints.Clear();
        spawnPoints.Clear();

        backgroundLayer.RemoveAllChildren();

        FTmxMap tmxMap = new FTmxMap();
        tmxMap.LoadTMX("Maps/"+mapName);
        
        FTilemap tilemap = (FTilemap)(tmxMap.getLayerNamed("Tilemap"));
        FTilemap tilemapCollision = (FTilemap)(tmxMap.getLayerNamed("Meta"));
        FContainer objectGroup = (FContainer)(tmxMap.getLayerNamed("Objects"));
        
        backgroundLayer.AddChild(tilemap);
        backgroundLayer.AddChild(tilemapCollision);
        backgroundLayer.AddChild(objectGroup);

        foreach (XMLNode xml in tmxMap.objects)
        {
            switch (xml.attributes["type"])
            {
                case "Spawn":
                    FNode spawnPoint = new FNode();
                    spawnPoint.SetPosition(int.Parse(xml.attributes["x"]), int.Parse(xml.attributes["y"]));
                    spawnPoints.Add(spawnPoint);
                    break;
                case "Warp":
                    int warpX = 0;
                    int warpY = 0;
                    XMLNode propertiesNode = (XMLNode)xml.children[0];
                    foreach (XMLNode property in propertiesNode.children)
                    {
                        switch (property.attributes["name"])
                        {
                            case "warpTileX":
                                warpX = int.Parse(property.attributes["value"]);
                                break;
                            case "warpTileY":
                                warpY = int.Parse(property.attributes["value"]);
                                break;
                        }
                    }
                    WarpPoint warpPoint = new WarpPoint(warpX, warpY, xml.attributes["name"], int.Parse(xml.attributes["x"]) + 8, -int.Parse(xml.attributes["y"]) + 8);
                    warpPoints.Add(warpPoint);
                    break;
            }
        }

        player.setTilemap(tilemapCollision);
        player.SetPosition(100, -100);
        
        camera.setWorldBounds(new Rect(0, -tilemap.height, tilemap.width, tilemap.height));
        tmxMap.setClipNode(camera);


    }

    // Update is called once per frame
    void Update()
    {
        WarpPoint destWarpPoint = null;
        foreach (WarpPoint wp in warpPoints)
        {
            float distSquared = (player.GetPosition() - wp.GetPosition()).sqrMagnitude;
            
            if (distSquared <= 10)
            {
                destWarpPoint = wp;
                break;
                
            }
        }
        if (destWarpPoint != null)
        {
            loadMap(destWarpPoint.mapName);
            player.SetPosition(destWarpPoint.warpTileX * 16 + 8, destWarpPoint.warpTileY * -16 + 8);
        }
    }
}
