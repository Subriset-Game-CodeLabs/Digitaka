using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoDotFiveDimension;
using UnityEditor;
public class DataManagerWindow : OdinMenuEditorWindow
    {
        private static Type[] _questData = TypeCache.GetTypesDerivedFrom(typeof(QuestData))
            .OrderBy(d => d.Name)
            .ToArray();
        private static Type[] _playerData = TypeCache.GetTypesDerivedFrom(typeof(StatsData))
            .OrderBy(d => d.Name)
            .ToArray();
        private static Type[] _itemData = TypeCache.GetTypesDerivedFrom(typeof(ItemBaseSO))
            .OrderBy(d => d.Name)
            .ToArray();
        // private static Type[] _buildingArea = TypeCache.GetTypesDerivedFrom(typeof(BuildableTile))
        //     .OrderBy(d => d.Name)
        //     .ToArray();
        
        [MenuItem("Digitaka/DataEditor")]
        public static void OpenWindow()
        {
            GetWindow<DataManagerWindow>().Show();
        }
        protected override OdinMenuTree BuildMenuTree()
        {
            var menu = new OdinMenuTree();
            
            foreach(var data in _questData)
            {
                menu.AddAllAssetsAtPath(data.Name, "Assets/", data, true, true);
            }
            foreach(var data in _playerData)
            {
                menu.AddAllAssetsAtPath(data.Name, "Assets/", data, true, true);
            }
            foreach(var data in _itemData)
            {
                menu.AddAllAssetsAtPath(data.Name, "Assets/", data, true, true);
            }
            // foreach(var data in _buildingArea)
            // {
            //     menu.AddAllAssetsAtPath(data.Name, "Assets/", data, true, true);
            // }
            return menu;
        }
    }
