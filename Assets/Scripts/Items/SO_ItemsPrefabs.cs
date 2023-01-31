using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Apollo11.Items
{
    [CreateAssetMenu(fileName = "ItemsPrefabsConfig", menuName = "Apollo11/SO/ItemsPrefabsConfig")]
    public class SO_ItemsPrefabs : SerializedScriptableObject
    {
        public Dictionary<Enums.Items, GameObject> Dictionary;
    }
}
