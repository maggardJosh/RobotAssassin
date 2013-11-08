using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Map : FTmxMap
{

    public List<FNode> spawnPoints = new List<FNode>();
    List<WarpPoint> warpPoints = new List<WarpPoint>();
    private Player player;

    public FTilemap tilemap;
    public FTilemap tilemapCollision;
    public FContainer objectGroup;

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

    private void clearMap()
    {
        spawnPoints.Clear();
        warpPoints.Clear();
        if (tilemap != null)
            tilemap.RemoveFromContainer();
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
            Scientist s = new Scientist(tilemap.width*RXRandom.Float(), -tilemap.height*RXRandom.Float());
            while (BaseGameObject.isWalkable(tilemap, s.x, s.y))
                s.SetPosition(tilemap.width * RXRandom.Float(), -tilemap.height * RXRandom.Float());
            playerLayer.AddChild(s);
            s.setTilemap(tilemapCollision);
        }

        backgroundLayer.AddChild(tilemap);
        backgroundLayer.AddChild(tilemapCollision);
        backgroundLayer.AddChild(objectGroup);

        player.setTilemap(tilemapCollision);
        playerLayer.AddChild(player);
    }

    internal void Update()
    {
        throw new NotImplementedException();
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
