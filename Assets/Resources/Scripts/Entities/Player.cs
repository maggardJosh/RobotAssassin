using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : BaseWalkingAnimSprite
{
    private Sword sword;
    private Vector2 swordDisp = Vector2.zero;
    public Player()
        : base("scientist")
    {
        play("walk_down");
        sword = new Sword();
    }

    public override void HandleAddedToContainer(FContainer container)
    {
        container.AddChild(sword);
        base.HandleAddedToContainer(container);
    }

    protected override void Update()
    {
        HandleControls();
        base.Update();
        sword.SetPosition(this.GetPosition() + swordDisp);
    }

    
    private void HandleControls()
    {
        if (UnityEngine.Input.GetKey(KeyCode.D))
            walkRight();
        if (UnityEngine.Input.GetKey(KeyCode.A))
            walkLeft();
        if (UnityEngine.Input.GetKey(KeyCode.W))
            walkUp();
        if (UnityEngine.Input.GetKey(KeyCode.S))
            walkDown();

        if (UnityEngine.Input.GetKey(KeyCode.X))
            attack();
    }
    private int scalarDisp = 13;
    private void attack()
    {
        
        sword.play("attack_down", true);
        switch(currentDirection)
        {
            case Direction.DOWN:
                sword.rotation = 0;
                swordDisp = Vector2.up * -scalarDisp;
                break;
            case Direction.LEFT:
                sword.rotation = 90;
                swordDisp = Vector2.right * -scalarDisp;
                break;
            case Direction.RIGHT:
                sword.rotation = -90;
                swordDisp = Vector2.right * scalarDisp;
                break;
            case Direction.UP:
                sword.rotation = 180;
                swordDisp = Vector2.up * scalarDisp;
                break;
                
        }
    }
}
