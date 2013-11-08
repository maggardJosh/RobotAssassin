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
        WALKING,
        ATTACKING
    }
    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }
    protected Direction currentDirection = Direction.DOWN;
    private State _state = State.IDLE;

    public State CurrentState
    {
        get { return _state; }
        set
        {
            //Use this in the future to make sure we don't transition out of a state when we shouldn't (during attack or something)
            _state = value;
        }
    }
    
    public BaseWalkingAnimSprite(string baseElement)
        : base(baseElement)
    {
        addAnimation(new FAnimation(State.IDLE + "_" + Direction.DOWN, new int[] { 1 }, animSpeed, true));
        addAnimation(new FAnimation(State.IDLE + "_" + Direction.LEFT, new int[] { 2 }, animSpeed, true));
        addAnimation(new FAnimation(State.IDLE + "_" + Direction.UP, new int[] { 3 }, animSpeed, true));
        addAnimation(new FAnimation(State.IDLE + "_" + Direction.RIGHT, new int[] { 4 }, animSpeed, true));
        addAnimation(new FAnimation(State.WALKING + "_" + Direction.DOWN, new int[] { 5,6,7,8}, animSpeed, true));
        addAnimation(new FAnimation(State.WALKING + "_" + Direction.LEFT, new int[] { 9,10,11,12 }, animSpeed, true));
        addAnimation(new FAnimation(State.WALKING + "_" + Direction.UP, new int[] { 13,14,15,16 }, animSpeed, true));
        addAnimation(new FAnimation(State.WALKING + "_" + Direction.RIGHT, new int[] { 17,18,19,20 }, animSpeed, true));
        play(State.IDLE + "_" + Direction.DOWN);
    }


    protected override void  Update()
    {
        if (CurrentState == State.ATTACKING)
        {
            this.alpha = 0;
            base.Update();
            return;
        }
        else
            this.alpha = 1;
        play(CurrentState + "_" + currentDirection);
        CurrentState = State.IDLE;
        base.Update();
        
    }

    protected void walkUp()
    {
        CurrentState = State.WALKING;
        currentDirection = Direction.UP;
        moveUp(walkSpeed);
    }

    protected void walkDown()
    {
        CurrentState = State.WALKING;
        currentDirection = Direction.DOWN;
        moveDown(walkSpeed);
    }

    protected void walkLeft()
    {

        CurrentState = State.WALKING;
        currentDirection = Direction.LEFT;
        moveLeft(walkSpeed);
    }

    protected void walkRight()
    {
        CurrentState = State.WALKING;
        currentDirection = Direction.RIGHT;
        moveRight(walkSpeed);
    }


}
