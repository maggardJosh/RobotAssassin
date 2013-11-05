using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class BaseWalkingAnimSprite : BaseGameObject
{
    protected int animSpeed = 300;
    protected float walkSpeed = 50.0f;
    public enum State
    {
        IDLE,
        WALKING
    }
    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }
    protected Direction currentDirection = Direction.DOWN;
    protected State currentState = State.IDLE;
    public BaseWalkingAnimSprite(string baseElement)
        : base(baseElement+"_1")
    {
        addAnimation(new FAnimation(State.IDLE+ "_" + Direction.DOWN, new int[] { 1 }, animSpeed, true));
        addAnimation(new FAnimation(State.IDLE+ "_" + Direction.LEFT, new int[] { 5 }, animSpeed, true));
        addAnimation(new FAnimation(State.IDLE+ "_" + Direction.UP, new int[] { 9 }, animSpeed, true));
        addAnimation(new FAnimation(State.IDLE+ "_" + Direction.RIGHT, new int[] { 13 }, animSpeed, true));
        addAnimation(new FAnimation(State.WALKING+"_" + Direction.DOWN, new int[] { 1,2,3,4 }, animSpeed, true));
        addAnimation(new FAnimation(State.WALKING+"_" + Direction.LEFT, new int[] { 5,6,7,8 }, animSpeed, true));
        addAnimation(new FAnimation(State.WALKING+"_" + Direction.UP, new int[] { 9,10,11,12 },animSpeed, true));
        addAnimation(new FAnimation(State.WALKING+"_" + Direction.RIGHT, new int[] { 13,14,15,16 }, animSpeed, true));
    }

    protected override  void Update()
    {
        play(currentState + "_" + currentDirection);
        currentState = State.IDLE;

    }

    protected void walkUp()
    {
        currentState = State.WALKING;
        currentDirection = Direction.UP;
        moveUp(walkSpeed);
    }

    protected void walkDown()
    {
        currentState = State.WALKING;
        currentDirection = Direction.DOWN;
        moveDown(walkSpeed);
    }

    protected void walkLeft()
    {

        currentState = State.WALKING;
        currentDirection = Direction.LEFT;
        moveLeft(walkSpeed);
    }

    protected void walkRight()
    {
        currentState = State.WALKING;
        currentDirection = Direction.RIGHT;
        moveRight(walkSpeed);
    }


}
