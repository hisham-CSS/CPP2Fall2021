using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSavePlayer : PersistentSaveObject
{
    private void Start()
    {
        curObjectData = new PlayerData();
    }
    public override void SaveGamePrepare()
    {
        //save all other attributes for the player then call the base class function in order to add the object to the save game objects
        base.SaveGamePrepare();
    }

    public override void LoadGameComplete()
    {
        base.LoadGameComplete();
    }
}
