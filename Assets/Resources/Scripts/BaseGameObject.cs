﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class BaseGameObject : FAnimatedSprite
{
    private FTilemap collisionTilemap;

    private FAnimatedSprite[] otherAnims = new FAnimatedSprite[3];
    protected int currentLevel = 0;

    public BaseGameObject(string elementBase)
        : base(elementBase+"_1")
    {
        
        for (int x = 0; x < 3; x++)
        {
            otherAnims[x] = new FAnimatedSprite(elementBase + "_" + (x + 1));
        }
    }

    public static bool isWalkable(FTilemap map, float xPos, float yPos)
    {
        int tileFrame = map.getFrameNumAt(xPos, yPos);
        int[] wallFrames = new int[] { 1, -1 };
        return !wallFrames.Contains(tileFrame);
    }
    private float count = 0;
    private float nextCount = .5f;
    private float maxCount = 1.5f;
    protected virtual void Update()
    {

        count += UnityEngine.Time.deltaTime;
        if (count > .05f)
        {
            currentLevel = RXRandom.Int(4);
            count = 0;
        }
        
        foreach (FAnimatedSprite sprite in otherAnims)
        {
            sprite.SetPosition(this.GetPosition());
            sprite.sortZ = this.sortZ;
            sprite.setFAnim(this.currentAnim);
        }
        showCurrentLevel();
    }

    private void showCurrentLevel()
    {
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