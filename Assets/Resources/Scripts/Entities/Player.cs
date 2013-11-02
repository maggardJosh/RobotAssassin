using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : BaseWalkingAnimSprite
{
    public Player()
        : base("Player")
    {
        play("walk_down");
    }

    protected override void Update()
    {
        HandleControls();
    }

    
    private void HandleControls()
    {
        if (UnityEngine.Input.GetKey(KeyCode.W))
            moveUp();
        if (UnityEngine.Input.GetKey(KeyCode.S))
            moveDown();
        if (UnityEngine.Input.GetKey(KeyCode.D))
            moveRight();
        if (UnityEngine.Input.GetKey(KeyCode.A))
            moveLeft();
    }
}
