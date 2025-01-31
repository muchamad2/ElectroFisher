using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public enum EnvironmentType { Forest, Sea }
public enum Language { Indo, Eng }
public static class GameUtility
{
    public static string PlayerName;
    public static string PlayerGrade;
    public static int PlayerScore { get; set; }
    public static int PlayerHealth { get; set; }
    public static int FinalScore{get;set;}
    public static bool isPlaying = false;
    public static bool mute = false;

    public static EnvironmentType environmentType { get; set; }

    public static Language LangType { get; set; }
    public const string xmlind= "id";
    public const string xmleng = "en";
    public static string xmlFileName {get; set;}
    public static string xmlFilePath
    {
        get
        {
            return Application.dataPath + "/StreamingAssets/"+ ((LangType == Language.Indo) ? xmlind : xmleng) + xmlFileName;
        }
    }
}

/// <summary>
/// mendapatkan data dari xml file kedalam kelas data dan meyimpan data ke dalam bentuk xmlfile,
/// fungsi **Write(Data data)** untuk menyimpan data kedalam xml file
/// 
/// fungsi **Fetch(out bool result)** mendapatkan data dari xml file dan diperlihatkan hasil dalam bentuk bool
/// result
/// 
/// #Example Fetch
/// <code>
/// var d = Data.Fetch(out bool result);
/// if(result){
///    data = d;
/// }
/// </code>
/// </summary>
[System.Serializable]
public class Data
{

    public Language lang;
    public string filename;
    public Question[] Questions = new Question[0];
    public Data() { }

    public static void Write(Data data, string filename)
    {
        GameUtility.xmlFileName = filename;
        XmlSerializer serializer = new XmlSerializer(typeof(Data));
        using (Stream stream = new FileStream(GameUtility.xmlFilePath, FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }
    }
    public static Data Fetch(string filename)
    {
        return Fetch(out bool result,filename);
    }
    public static Data Fetch(out bool result,string filename)
    {
        GameUtility.xmlFileName = filename;
        if (!File.Exists(GameUtility.xmlFilePath)) { result = false; return new Data(); }
        XmlSerializer deserializer = new XmlSerializer(typeof(Data));
        using (Stream stream = new FileStream(GameUtility.xmlFilePath, FileMode.Open))
        {
            var data = (Data)deserializer.Deserialize(stream);

            result = true;

            return data;
        }
    }
}