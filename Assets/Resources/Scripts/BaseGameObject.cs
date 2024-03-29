﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class BaseGameObject : FAnimatedSprite
{
    private FTilemap collisionTilemap;

    private FAnimatedSprite[] otherAnims = new FAnimatedSprite[3];
    private string elementBase = "";
    private int maxLevel = 1;
    protected int health = 1;

    protected Map currentMap;

    public BaseGameObject(string elementBase)
        : base(elementBase + "_1")
    {
        this.elementBase = elementBase;
        for (int x = 1; x < 4; x++)
        {
            if (Futile.atlasManager.elementExists(elementBase + "_" + (x + 1) + "/0"))
            {
                otherAnims[x - 1] = new FAnimatedSprite(elementBase + "_" + (x + 1));
                maxLevel = x + 1;
            }
            else
            {
                otherAnims[x - 1] = new FAnimatedSprite(elementBase + "_1");
            }
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
        for (int x = 0; x < 3; x++)
        {

            FAnimatedSprite sprite = otherAnims[x];
            sprite.SetPosition(this.GetPosition());
            sprite.sortZ = this.sortZ;
            if (maxLevel >= x + 2)
                sprite.SetElementByName(this.elementBase + "_" + (x + 2) + "/" + this.currentFrame);
            sprite.alpha = this.alpha;
            sprite.rotation = this.rotation;

        }

        showCurrentLevel();
    }
    private float pushFromWallsSpeed = 25;
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
        if (!isWalkable(collisionTilemap, x, y - height / 2))
        {
            y += pushFromWallsSpeed * UnityEngine.Time.deltaTime;
        }
    }

    private void showCurrentLevel()
    {
        int currentLevel = Math.Min(maxLevel-1, GlitchManager.getInstance().CurrentLevel);
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

    public void setMap(Map map)
    {
        this.currentMap = map;
        this.collisionTilemap = map.tilemapCollision;
    }

    public void moveUp(float speed)
    {
        if (BaseWalkingAnimSprite.isWalkable(collisionTilemap, x, (y + speed * UnityEngine.Time.deltaTime)))
            y += speed * UnityEngine.Time.deltaTime;
        this.sortZ = -y;
    }

    public void moveDown(float speed)
    {
        if (BaseWalkingAnimSprite.isWalkable(collisionTilemap, x, (y - speed * UnityEngine.Time.deltaTime) - height / 2))
            y -= speed * UnityEngine.Time.deltaTime;
        this.sortZ = -y;
    }

    public void moveLeft(float speed)
    {
        if (BaseWalkingAnimSprite.isWalkable(collisionTilemap, (x - speed * UnityEngine.Time.deltaTime) - width / 2, y - height / 4))
            x -= speed * UnityEngine.Time.deltaTime;
        this.sortZ = -y;
    }

    public void moveRight(float speed)
    {
        if (BaseWalkingAnimSprite.isWalkable(collisionTilemap, (x + speed * UnityEngine.Time.deltaTime) + width / 2, y - height / 4))
            x += speed * UnityEngine.Time.deltaTime;
        this.sortZ = -y;
    }
}
