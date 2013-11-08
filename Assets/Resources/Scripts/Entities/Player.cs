using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : BaseWalkingAnimSprite
{
    private Weapon primaryWeapon;
    private Weapon secondaryWeapon;
    public Player()
        : base("player")
    {
        play("walk_down");
        primaryWeapon = new Sword(this);
    }

    public override void HandleAddedToContainer(FContainer container)
    {
        container.AddChild(primaryWeapon);
        base.HandleAddedToContainer(container);
    }

    protected override void Update()
    {
        HandleControls();
        base.Update();
        primaryWeapon.SetPosition(this.GetPosition());
        primaryWeapon.Update();
        if (secondaryWeapon != null)
            secondaryWeapon.SetPosition(this.GetPosition());
    }

    
    private void HandleControls()
    {
        if (Main.controlsLocked)
            return;

        if (UnityEngine.Input.GetKeyDown(KeyCode.X))
            attack();
        if (this.CurrentState == State.ATTACKING)
            return;

        if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
            walkRight();
        if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
            walkLeft();
        if (UnityEngine.Input.GetKey(KeyCode.UpArrow))
            walkUp();
        if (UnityEngine.Input.GetKey(KeyCode.DownArrow))
            walkDown();
    }

    private void attack()
    {
        primaryWeapon.Attack();
    }
}
