using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class saveSystem //reads (loadplayer) and writes (saveplayer) to/from the binary file
{
	public static void savePlayer(player p){
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/player.json";
		FileStream s = new FileStream(path, FileMode.Create);

		playerData save = new playerData(p);
		Debug.Log("SAVING: " + save);

		formatter.Serialize(s, save);
		s.Close();

	}

	public static playerData loadPlayer(){
		string path = Application.persistentDataPath + "/player.json";
		if (File.Exists(path)){
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

				// playerData data = formatter.Deserialize(stream) as playerData;
				// stream.Close();
				// return data;

			playerData data = null;
			try{
				data = formatter.Deserialize(stream) as playerData;
				//stream.Close();
				//return data;

			} catch{ 
				Debug.LogWarning("couldn't deserialize data");
				//stream.Close();
				//return null;
				//playerData data = null;
			} finally{
				stream.Close();
			}
			return data;

		}else {
			Debug.LogWarning("Save file not found in " + path);
			return null;
		}
	}
}
