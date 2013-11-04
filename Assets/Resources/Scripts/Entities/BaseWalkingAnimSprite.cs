using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class BaseWalkingAnimSprite : FAnimatedSprite
{
    protected float walkSpeed = 50.0f;
    public BaseWalkingAnimSprite(string baseElement)
        : base(baseElement)
    {
        addAnimation(new FAnimation("walk_down", new int[] { 0 }, 100, true));
        addAnimation(new FAnimation("walk_left", new int[] { 1 }, 100, true));
        addAnimation(new FAnimation("walk_up", new int[] { 2 }, 100, true));
        addAnimation(new FAnimation("walk_right", new int[] { 3 }, 100, true));
    }

    private FTilemap collisionTilemap;

    protected abstract void Update();

    public void setTilemap(FTilemap tilemap)
    {
        this.collisionTilemap = tilemap;
    }

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

    public static bool isWalkable(FTilemap map, float xPos, float yPos)
    {
        int tileFrame = map.getFrameNumAt(xPos,yPos);
        int[] wallFrames = new int[] {1,-1};
        return !wallFrames.Contains(tileFrame);
    }

    protected void moveUp()
    {
        if (BaseWalkingAnimSprite.isWalkable(collisionTilemap, x, (y + walkSpeed * UnityEngine.Time.deltaTime)))
            y += walkSpeed * UnityEngine.Time.deltaTime;
        play("walk_up");
    }

    protected void moveDown()
    {
        if (BaseWalkingAnimSprite.isWalkable(collisionTilemap, x, (y - walkSpeed * UnityEngine.Time.deltaTime) - height/2))
            y -= walkSpeed * UnityEngine.Time.deltaTime;
        play("walk_down");
    }

    protected void moveLeft()
    {
        if (BaseWalkingAnimSprite.isWalkable(collisionTilemap, (x - walkSpeed * UnityEngine.Time.deltaTime) - width / 2, y))
        x -= walkSpeed * UnityEngine.Time.deltaTime;
        play("walk_left");
    }

    protected void moveRight()
    {
        if (BaseWalkingAnimSprite.isWalkable(collisionTilemap, (x + walkSpeed * UnityEngine.Time.deltaTime) + width / 2, y))
        x += walkSpeed * UnityEngine.Time.deltaTime;
        play("walk_right");
    }

}
