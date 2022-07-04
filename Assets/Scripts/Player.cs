using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RoomEntity
{
    public static Player Instance;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        Type = RoomEntityType.Player;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
