using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class BaseWalkingAnimSprite : BaseGameObject
{
    protected int animSpeed = 210;
    protected float walkSpeed = 50.0f;
    protected Vector2 hitVect = Vector2.zero;
    public enum State
    {
        IDLE,
        WALKING,
        ATTACKING,
        HIT
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
            stateCount = 0;
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
        addAnimation(new FAnimation(State.HIT + "_" + Direction.DOWN, new int[] { 1 }, animSpeed, true));
        addAnimation(new FAnimation(State.HIT + "_" + Direction.LEFT, new int[] { 2 }, animSpeed, true));
        addAnimation(new FAnimation(State.HIT + "_" + Direction.UP, new int[] { 3 }, animSpeed, true));
        addAnimation(new FAnimation(State.HIT + "_" + Direction.RIGHT, new int[] { 4 }, animSpeed, true));
        addAnimation(new FAnimation(State.WALKING + "_" + Direction.DOWN, new int[] { 5, 6, 7, 8 }, animSpeed, true));
        addAnimation(new FAnimation(State.WALKING + "_" + Direction.LEFT, new int[] { 9, 10, 11, 12 }, animSpeed, true));
        addAnimation(new FAnimation(State.WALKING + "_" + Direction.UP, new int[] { 13, 14, 15, 16 }, animSpeed, true));
        addAnimation(new FAnimation(State.WALKING + "_" + Direction.RIGHT, new int[] { 17, 18, 19, 20 }, animSpeed, true));
        play(State.IDLE + "_" + Direction.DOWN);
    }
    public void takeHit(int damage, Vector2 hitVect)
    {
        CurrentState = State.HIT;
        this.hitVect = hitVect;
        this.health -= damage;
    }
    private float stateCount = 0;
    private const float HIT_MAX_COUNT = 1;
    protected override void Update()
    {
        stateCount += UnityEngine.Time.deltaTime;
        if (CurrentState == State.ATTACKING)
        {
            this.alpha = 0;
            base.Update();
            return;
        }
        else
            this.alpha = 1;
        play(CurrentState + "_" + currentDirection);
        if (CurrentState == State.HIT)
        {
            if (hitVect.x > 0)
                moveRight(hitVect.x);
            else
                moveLeft(-hitVect.x);

            if (hitVect.y > 0)
                moveUp(hitVect.y);
            else
                moveDown(-hitVect.y);
            hitVect *= .93f;
            this.alpha = ((int)(stateCount * 1000) % 10 < 5 ? 0 : 1);
            if (stateCount > HIT_MAX_COUNT)
            {
                CurrentState = State.IDLE;
            }
        }
        else
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
