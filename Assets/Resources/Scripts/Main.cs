using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class Main : MonoBehaviour
{
    Ym90_GUI camera;
    Player player;
    Map currentMap = new Map();

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

        GlitchManager.getInstance();       

        camera = new Ym90_GUI();
        player = new Player();

        currentMap = new Map();
        currentMap.setPlayer(player);
        loadMap("testMap");

        camera.follow(player);
        List<string> convo = new List<string>();
        convo.Add("This is the first convo");
        convo.Add("This is the second....");
        convo.Add("Last..\n           -Jif");
        FConvo convoOne = new FConvo(convo );
        
        camera.AddChild(convoOne);
        
        
        Futile.stage.AddChild(camera);
    }

    private void loadMap(string mapName)
    {
        currentMap.loadMap(mapName);
        camera.setWorldBounds(new Rect(0, -currentMap.tilemap.height, currentMap.tilemap.width, currentMap.tilemap.height));
        currentMap.setClipNode(camera);
    }

    // Update is called once per frame
    void Update()
    {
        WarpPoint destWarpPoint = currentMap.isWarpPointColliding(player);
        if (destWarpPoint!=null)
        {
            loadMap(destWarpPoint.mapName);
            player.SetPosition(destWarpPoint.warpTileX * 16 + 8, destWarpPoint.warpTileY * -16 + 8);
        }
        
    }
}
