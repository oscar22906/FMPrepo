using System.ComponentModel;
using UnityEngine;
using static TMPro.Examples.TMP_ExampleScript_01;

[CreateAssetMenu(fileName = "New Object", menuName = "Object/Create New Object")]
public class ObjectItem : ScriptableObject
{
    public string objectName;
    public Sprite icon;
    public ObjectType itemType;

    public enum ObjectType
    {
        Tree,
        Weapon,
        Material,
        Collectable
    }
}
