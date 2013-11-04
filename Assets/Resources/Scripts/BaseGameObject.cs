using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class BaseGameObject : FAnimatedSprite
{
    private FTilemap collisionTilemap;

    public BaseGameObject(string elementBase)
        : base(elementBase)
    {
        
    }

    public static bool isWalkable(FTilemap map, float xPos, float yPos)
    {
        int tileFrame = map.getFrameNumAt(xPos, yPos);
        int[] wallFrames = new int[] { 1, -1 };
        return !wallFrames.Contains(tileFrame);
    }

    protected abstract void Update();

    public override void HandleAddedToStage()
    {
        Futile.instance.SignalUpdate += Update;
        base.HandleAddedToStage();
    }

    public override void HandleRemovedFromStage()
    {
        Futile.instance.SignalUpdate -= Update;
        base.HandleRemovedFromStage();
    }



    public void setTilemap(FTilemap tilemap)
    {
        this.collisionTilemap = tilemap;
    }

    protected void moveUp(float speed)
    {
        if (BaseWalkingAnimSprite.isWalkable(collisionTilemap, x, (y + speed * UnityEngine.Time.deltaTime)))
            y += speed * UnityEngine.Time.deltaTime;
        this.sortZ = -y;
    }

    protected void moveDown(float speed)
    {
        if (BaseWalkingAnimSprite.isWalkable(collisionTilemap, x, (y - speed * UnityEngine.Time.deltaTime) - height / 2))
            y -= speed * UnityEngine.Time.deltaTime;
        this.sortZ = -y;
    }

    protected void moveLeft(float speed)
    {
        if (BaseWalkingAnimSprite.isWalkable(collisionTilemap, (x - speed * UnityEngine.Time.deltaTime) - width / 2, y))
            x -= speed * UnityEngine.Time.deltaTime;
        this.sortZ = -y;
    }

    protected void moveRight(float speed)
    {
        if (BaseWalkingAnimSprite.isWalkable(collisionTilemap, (x + speed * UnityEngine.Time.deltaTime) + width / 2, y))
            x += speed * UnityEngine.Time.deltaTime;
        this.sortZ = -y;
    }
}
