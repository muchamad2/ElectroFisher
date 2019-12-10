using UnityEngine;
using UnityEditor;

public class Data_Creator : EditorWindow
{
    [SerializeField] Data data = null;
    private Vector2 scrollPosition = Vector2.zero;
    SerializedObject serializedObject = null;
    SerializedProperty questionProp = null;
    SerializedProperty questionDataProp = null;
    


    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        data.Questions = new Question[0];
        questionDataProp = serializedObject.FindProperty("data");

    }
    [MenuItem("ElectroFisher/Data_Creator")]
    private static void ShowWindow()
    {
        var window = GetWindow<Data_Creator>("Creator");

        window.minSize = new Vector2(510.0f, 344.0f);

        //window.titleContent = new GUIContent("Data_Creator");
        window.Show();
    }

    private void OnGUI()
    {
        #region  Header Section

        Rect headerReact = new Rect(15, 15, this.position.width - 35, 65);
        GUI.Box(headerReact, GUIContent.none);
        GUIStyle headerstyle = new GUIStyle(EditorStyles.largeLabel)
        {
            fontSize = 26,
            alignment = TextAnchor.UpperLeft

        };
        headerReact.x += 5;
        headerReact.width -= 10;
        headerReact.y += 5;
        headerReact.height -= 10;
        GUI.Label(headerReact, "Data to XML Creator", headerstyle);

        Rect summaryRect = new Rect(headerReact.x + 25, (headerReact.y + headerReact.height) - 20, headerReact.width - 50, 15);
        GUI.Label(summaryRect, "Create the data that reads tobe included into the XML file");
        #endregion

        #region  Body Section


        Rect bodyRect = new Rect(15, (headerReact.y + headerReact.height) + 15, this.position.width - 30,
        this.position.height - (headerReact.y + headerReact.height) - 80);
        GUI.Box(bodyRect, GUIContent.none);

        var arraySize = data.Questions.Length;

        Rect viewRect = new Rect(bodyRect.x + 10, bodyRect.y + 10, bodyRect.width - 20,
        EditorGUI.GetPropertyHeight(questionDataProp));

        Rect scrollPostRect = new Rect(viewRect)
        {
            height = bodyRect.height - 20,
        };
        scrollPosition = GUI.BeginScrollView(scrollPostRect, scrollPosition, viewRect, false, false,
        GUIStyle.none, GUI.skin.verticalScrollbar);

        var drawSlider = viewRect.height > scrollPostRect.height;

        Rect propertyRect = new Rect(bodyRect.x + 10, bodyRect.y + 10, bodyRect.width - (drawSlider ? 40 : 20), 17);
        EditorGUI.PropertyField(propertyRect, questionDataProp, true);

        serializedObject.ApplyModifiedProperties();

        GUI.EndScrollView();

        #endregion
        
        #region Navigation

        Rect createButtonRect = new Rect(bodyRect.x + bodyRect.width - 85,bodyRect.y + bodyRect.height + 15,
        85,30);
        bool pressed = GUI.Button(createButtonRect, "Create",EditorStyles.miniButtonRight);
        if(pressed){
            GameUtility.LangType = data.lang;
            Data.Write(data,data.filename);
        }
        createButtonRect.x -= createButtonRect.width;
        pressed = GUI.Button(createButtonRect,"Fetch",EditorStyles.miniButtonLeft);
        if(pressed){
            var d = Data.Fetch(out bool result,data.filename);
            if(result){
                data = d;
            }
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
        #endregion

    }
}