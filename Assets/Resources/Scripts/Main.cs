using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class Main : MonoBehaviour
{
    Ym90_GUI camera;
    Player player;
    Map currentMap = new Map();
    LoadingScreen loadingScreen;
    public static bool controlsLocked = false;
    List<Event> eventQueue;

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

        eventQueue = new List<Event>();

        GlitchManager.getInstance();

        camera = Ym90_GUI.getInstance();
        player = new Player();

        currentMap = new Map();
        currentMap.setPlayer(player);
        loadMap("testMap");

        loadingScreen = new LoadingScreen();
        camera.setLoadingScreen(loadingScreen);
        camera.follow(player);



        Futile.stage.AddChild(camera);
    }

    private void loadMap(string mapName)
    {
        currentMap.loadMap(mapName);
        camera.setWorldBounds(new Rect(0, -currentMap.tilemap.height, currentMap.tilemap.width, currentMap.tilemap.height));
        currentMap.setClipNode(camera);
    }

    private bool loadingNewMap = false;
    private WarpPoint wpToLoad;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            test();
        if (eventQueue.Count > 0)
        {
            Main.controlsLocked = true;
            eventQueue[0].Update();
            if (eventQueue[0].finished)
            {
                eventQueue.RemoveAt(0);
                Main.controlsLocked = false;
            }
            return;
        }
        if (loadingScreen.transitionOn && loadingScreen.finishedTransition)
        {
            loadMap(wpToLoad.mapName);
            player.SetPosition(wpToLoad.warpTileX * 16 + 8, -wpToLoad.warpTileY * 16 + 8);
            loadingScreen.startTransitionOff();
        }
        else if (!loadingScreen.active)
        {
            WarpPoint destWarpPoint = currentMap.isWarpPointColliding(player);
            if (destWarpPoint != null)
            {
                Main.controlsLocked = true;
                wpToLoad = destWarpPoint;
                loadingScreen.startTransitionOn();

            }
        }



    }

    private void test()
    {
        List<string> convo = new List<string>();
        convo.Add("This is the first convo");
        convo.Add("This is the second....");
        FConvo convoOne = new FConvo(convo);

        eventQueue.Add(new Event(new List<EventAction>() { 
            new Action_MoveCamera(200,-300,4.0f), 
            new Action_ShowConvo(convoOne),
            new Action_FollowNode(player, 3.0f)           
        }));
    }
}
