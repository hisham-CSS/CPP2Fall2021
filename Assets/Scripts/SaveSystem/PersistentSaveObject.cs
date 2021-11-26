using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

//Put this on any object that needs at least it's transform values saved.
public class PersistentSaveObject : MonoBehaviour
{
    public ObjectData curObjectData = new ObjectData();

    public virtual void SaveGamePrepare()
    {
        curObjectData.posRotScale.posX = transform.position.x;
        curObjectData.posRotScale.posY = transform.position.y;
        curObjectData.posRotScale.posZ = transform.position.z;

        curObjectData.posRotScale.rotX = transform.localEulerAngles.x;
        curObjectData.posRotScale.rotY = transform.localEulerAngles.y;
        curObjectData.posRotScale.rotZ = transform.localEulerAngles.z;

        curObjectData.posRotScale.scaleX = transform.localScale.x;
        curObjectData.posRotScale.scaleY = transform.localScale.y;
        curObjectData.posRotScale.scaleZ = transform.localScale.z;

        GameManager.SaveManager.allSavedObjects.Add(curObjectData);
    }

    public virtual void LoadGameComplete()
    {
        // Set position
        transform.position = new Vector3(curObjectData.posRotScale.posX,
            curObjectData.posRotScale.posY, curObjectData.posRotScale.posZ);

        // Set rotation
        transform.localRotation = Quaternion.Euler(curObjectData.posRotScale.rotX,
            curObjectData.posRotScale.rotY, curObjectData.posRotScale.rotZ);

        // Set scale
        transform.localScale = new Vector3(curObjectData.posRotScale.scaleX,
            curObjectData.posRotScale.scaleY, curObjectData.posRotScale.scaleZ);
    }
}
