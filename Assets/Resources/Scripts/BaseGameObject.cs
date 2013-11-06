using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class BaseGameObject : FAnimatedSprite
{
    private FTilemap collisionTilemap;

    private FAnimatedSprite[] otherAnims = new FAnimatedSprite[3];
    private string elementBase = "";

    public BaseGameObject(string elementBase)
        : base(elementBase+"_1")
    {
        this.elementBase = elementBase;
        for (int x = 0; x < 3; x++)
        {
            otherAnims[x] = new FAnimatedSprite(elementBase + "_" + (x + 1));
        }
    }

    public void SetPosition(Vector2 newPosition)
    {
        base.SetPosition(newPosition);
        foreach (FAnimatedSprite s in otherAnims)
            s.SetPosition(newPosition);
    }

    public static bool isWalkable(FTilemap map, float xPos, float yPos)
    {
        int tileFrame = map.getFrameNumAt(xPos, yPos);
        int[] wallFrames = new int[] { 1, -1 };
        return !wallFrames.Contains(tileFrame);
    }
   
    protected virtual void Update()
    {
        pushAwayFromWalls();
       
        this.sortZ = -y;
        for(int x=0; x<3; x++)
        {
            FAnimatedSprite sprite = otherAnims[x];
            sprite.SetPosition(this.GetPosition());
            sprite.sortZ = this.sortZ;
            sprite.SetElementByName(this.elementBase + "_" + (x + 1) + "/"+this.currentFrame);
            sprite.alpha = this.alpha;
            sprite.rotation = this.rotation;

        }
      
        showCurrentLevel();
    }
    private float pushFromWallsSpeed = 15;
    private void pushAwayFromWalls()
    {
        if (collisionTilemap == null)
            return;
        if (!isWalkable(collisionTilemap, x - width / 2, y))
        {
            x += pushFromWallsSpeed * UnityEngine.Time.deltaTime;
        }
        if (!isWalkable(collisionTilemap, x + width / 2, y))
        {
            x -= pushFromWallsSpeed * UnityEngine.Time.deltaTime;
        }
        if (!isWalkable(collisionTilemap, x, y))
        {
            y -= pushFromWallsSpeed * UnityEngine.Time.deltaTime;
        }
        if (!isWalkable(collisionTilemap, x , y-height/2))
        {
            y += pushFromWallsSpeed * UnityEngine.Time.deltaTime;
        }
    }

    private void showCurrentLevel()
    {
        int currentLevel = GlitchManager.getInstance().CurrentLevel;
        this.isVisible = false;
        foreach (FAnimatedSprite sprite in otherAnims)
            sprite.isVisible = false;
        if (currentLevel == 0)
            this.isVisible = true;
        else
            otherAnims[currentLevel - 1].isVisible = true;
    }

    public override void HandleAddedToStage()
    {
        foreach (FAnimatedSprite sprite in otherAnims)
            this.container.AddChild(sprite);
        Futile.instance.SignalUpdate += Update;
        base.HandleAddedToStage();
    }

    public override void HandleRemovedFromStage()
    {
        foreach (FAnimatedSprite sprite in otherAnims)
            sprite.RemoveFromContainer();
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
        if (BaseWalkingAnimSprite.isWalkable(collisionTilemap, (x - speed * UnityEngine.Time.deltaTime) - width / 2, y-height/4))
            x -= speed * UnityEngine.Time.deltaTime;
        this.sortZ = -y;
    }

    protected void moveRight(float speed)
    {
        if (BaseWalkingAnimSprite.isWalkable(collisionTilemap, (x + speed * UnityEngine.Time.deltaTime) + width / 2, y-height/4))
            x += speed * UnityEngine.Time.deltaTime;
        this.sortZ = -y;
    }
}
