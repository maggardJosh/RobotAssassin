using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Scientist : BaseWalkingAnimSprite
{
    Vector2 startPos = Vector2.zero;
    enum Dir { UP, LEFT, DOWN, RIGHT}

    Dir currentDirection = Dir.DOWN;
    float lastDecision = 0;
    float MIN_DECISION = 1;
    bool moving;
    public Scientist(float startX, float startY)
        : base("Scientist")
    {
        startPos = new Vector2(startX, startY);
        SetPosition(startPos);
        play("walk_down");
        this.walkSpeed *= .5f;
    }

    protected override void Update()
    {
        lastDecision += Time.deltaTime;
        if (lastDecision >= MIN_DECISION)
        {
            if (RXRandom.Float() < .3f)
            {
                moving = !moving;
            }else
            if (RXRandom.Float() > .7f)
            {
                if (RXRandom.Bool())
                    currentDirection += 1;
                else
                    currentDirection -= 1;

                if (currentDirection < 0)
                    currentDirection = Dir.RIGHT;
                else
                    if (!Enum.IsDefined(typeof(Dir), currentDirection))
                    {
                        currentDirection = 0;
                    }
            }
            lastDecision = 0;
        }

        if (moving)
        {
            switch(currentDirection)
            {
                case Dir.UP:
                    moveUp();
                    break;
                case Dir.LEFT:
                    moveLeft();
                    break;
                case Dir.DOWN:
                    moveDown();
                    break;
                case Dir.RIGHT:
                    moveRight();
                    break;
            }
        }
    }

    
}
