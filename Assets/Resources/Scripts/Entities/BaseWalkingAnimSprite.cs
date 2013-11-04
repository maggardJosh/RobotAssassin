using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class BaseWalkingAnimSprite : BaseGameObject
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


    protected void walkUp()
    {
        moveUp(walkSpeed);
        play("walk_up");
       
    }

    protected void walkDown()
    {
        moveDown(walkSpeed);
        play("walk_down");
    }

    protected void walkLeft()
    {
        moveLeft(walkSpeed);
        play("walk_left");
    }

    protected void walkRight()
    {
        moveRight(walkSpeed);
        play("walk_right");
    }


}
