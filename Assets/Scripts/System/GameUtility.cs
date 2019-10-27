using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public enum EnvironmentType {Forest,Sea}
public enum Language{Indo,Eng}
public static class GameUtility{
    public static int PlayerScore { get; set; }
    public static int PlayerHealth { get; set; }
    
    public static EnvironmentType environmentType { get; set; }

    public static Language LangType { get; set; }
    public const string  xmlFileName = "Question_Data.xml";
    public static string xmlFilePath{
        get{
            return Application.dataPath+ "/Resources/"+((LangType == Language.Indo) ? "id_":"eng_")+xmlFileName;
        }
    }
}
[System.Serializable]
public class Data{

    public Language lang = Language.Indo;
    public Question[] Questions = new Question[0];
    public Data(){}

    public static void Write(Data data){
        XmlSerializer serializer = new XmlSerializer(typeof(Data));
        using (Stream stream = new FileStream(GameUtility.xmlFilePath, FileMode.Create)){
            serializer.Serialize(stream, data);
        }
    }
    public static Data Fetch(){
        return Fetch(out bool result);
    }
    public static Data Fetch(out bool result)
    {
        if(!File.Exists(GameUtility.xmlFilePath)){result = false; return new Data();}
        XmlSerializer deserializer = new XmlSerializer(typeof(Data));
        using (Stream stream = new FileStream(GameUtility.xmlFilePath,FileMode.Open)){
            var data = (Data)deserializer.Deserialize(stream);
            
            result = true;
            
            return data;
        }
    }
}