﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Map : FTmxMap
{

    public List<FNode> spawnPoints = new List<FNode>();
    List<WarpPoint> warpPoints = new List<WarpPoint>();
    private Player player;
    public List<BaseGameObject> enemyList = new List<BaseGameObject>();

    public FTilemap tilemap;
    public FTilemap tilemapCollision;
    public FContainer objectGroup;

    private FTilemap[] otherTilemaps = new FTilemap[3];

    FContainer backgroundLayer;
    FContainer playerLayer;
    FContainer foregroundLayer;

    public Map()
        : base()
    {
        backgroundLayer = new FContainer();
        playerLayer = new FContainer();
        foregroundLayer = new FContainer();

        playerLayer.shouldSortByZ = true;

        Futile.stage.AddChild(backgroundLayer);
        Futile.stage.AddChild(playerLayer);
        Futile.stage.AddChild(foregroundLayer);
    }

    public void clearMap()
    {
        enemyList.Clear();
        spawnPoints.Clear();
        warpPoints.Clear();
        if (tilemap != null)
        {
            tilemap.RemoveFromContainer();
            tilemap.RemoveAllChildren();
        }
        foreach (FTilemap f in otherTilemaps)
        {
            if (f != null)
                f.RemoveFromContainer();
        }
        if (tilemapCollision != null)
            tilemapCollision.RemoveFromContainer();
        if (objectGroup != null)
            objectGroup.RemoveFromContainer();
        for (int x = 0; x < playerLayer.GetChildCount(); x++)
        {
            playerLayer.RemoveChild(playerLayer.GetChildAt(x));
            x--;
        }
    }

    internal void loadMap(string mapName)
    {
        this.clearMap();
        this.LoadTMX("Maps/" + mapName);

        tilemap = (FTilemap)(getLayerNamed("Tilemap"));

        for (int x = 0; x < 3; x++)
        {
            otherTilemaps[x] = new FTilemap(tilemap.BaseElementName + "_" + (x + 1), 1);
            otherTilemaps[x].LoadText(tilemap.dataString, false);
        }

        tilemapCollision = (FTilemap)(getLayerNamed("Meta"));
        objectGroup = (FContainer)(getLayerNamed("Objects"));



        foreach (XMLNode xml in this.objects)
        {
            switch (xml.attributes["type"])
            {
                case "Spawn":
                    FNode spawnPoint = new FNode();
                    spawnPoint.SetPosition(int.Parse(xml.attributes["x"]) + 8, -int.Parse(xml.attributes["y"]) + 8);
                    spawnPoints.Add(spawnPoint);
                    player.SetPosition(spawnPoint.GetPosition());
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
        for (int x = 0; x < 100; x++)
        {
            Scientist s = new Scientist(tilemap.width * RXRandom.Float(), -tilemap.height * RXRandom.Float());
            while (BaseGameObject.isWalkable(tilemap, s.x, s.y))
                s.SetPosition(tilemap.width * RXRandom.Float(), -tilemap.height * RXRandom.Float());
            addEnemy(s);
        }

        backgroundLayer.AddChild(tilemap);
        foreach (FTilemap f in otherTilemaps)
            backgroundLayer.AddChild(f);
        backgroundLayer.AddChild(tilemapCollision);
        backgroundLayer.AddChild(objectGroup);

        player.setMap(this);
        playerLayer.AddChild(player);
    }

    public void addEnemy(BaseGameObject enemy)
    {
        playerLayer.AddChild(enemy);
        enemy.setMap(this);
        enemyList.Add(enemy);
    }

    internal void Update()
    {
        showCorrectGlitchLevel();
    }

    private void showCorrectGlitchLevel()
    {
        int currentLevel = GlitchManager.getInstance().CurrentLevel;
        this.isVisible = false;
        foreach (FTilemap f in otherTilemaps)
            f.isVisible = false;
        if (currentLevel == 0)
            this.isVisible = true;
        else
            otherTilemaps[currentLevel - 1].isVisible = true;
    }

    internal WarpPoint isWarpPointColliding(Player player)
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
        return destWarpPoint;
    }

    internal void setPlayer(Player player)
    {
        playerLayer.AddChild(player);
        this.player = player;
    }
}
