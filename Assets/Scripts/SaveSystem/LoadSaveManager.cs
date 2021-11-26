using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LoadSaveManager : MonoBehaviour
{
	
	public List<ObjectData> allSavedObjects = new List<ObjectData>();

	[XmlInclude(typeof(PlayerData))]
	public void Save(string fileName = "GameData.xml")
	{
		// Save game data
		XmlSerializer serializer = new XmlSerializer(typeof(List<ObjectData>));
		FileStream stream = new FileStream(fileName, FileMode.Create);
		serializer.Serialize(stream, allSavedObjects);
		stream.Flush();
		stream.Dispose();
		stream.Close();
	}

	[XmlInclude(typeof(PlayerData))]
	// Load game data from XML file
	public void Load(string fileName = "GameData.xml")
	{
		XmlSerializer serializer = new XmlSerializer(typeof(List<ObjectData>));
		FileStream stream = new FileStream(fileName, FileMode.Open);
		allSavedObjects = serializer.Deserialize(stream) as List<ObjectData>;
		stream.Flush();
		stream.Dispose();
		stream.Close();

		GameManager.SaveManager.allSavedObjects.Clear();
	}
}
