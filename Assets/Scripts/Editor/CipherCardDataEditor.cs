using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Com.SakuraStudios.FECipherPlayer
{
    // A custom editor for the Scriptable objects CipherCardData that displays the information in a more legible format for easier work.
    [CustomEditor(typeof(CipherCardData)), CanEditMultipleObjects]
    public class CipherCardDataEditor : Editor
    {
        /* CardData reference
        public string cardNumber;
        public string charTitle;
        public string charQuote;
        public string cardIllustrator;
        
        public string[] cardSkills;
        public bool[] skillTypes = new bool[CipherData.NumSkillTypes];
        public string charName;
        public string classTitle;
        public int deploymentCost;
        public bool canPromote;
        public int promotionCost;
        public bool[] cardColor = new bool[CipherData.NumColors];
        public bool[] charGender = new bool[CipherData.NumGenders];
        public bool[] charWeaponType = new bool[CipherData.NumWeapons];
        public bool[] unitTypes = new bool[CipherData.NumTypes];
        public int baseAttack;
        public int baseSupport;
        public bool[] baseRange = new bool[CipherData.NumRanges];
        */

        SerializedProperty cardNumber;
        SerializedProperty charTitle;
        SerializedProperty charQuote;
        SerializedProperty cardIllustrator;
        SerializedProperty cardSkills;
        SerializedProperty skillTypes;

        SerializedProperty charName;
        SerializedProperty classTitle;
        SerializedProperty deploymentCost;
        SerializedProperty canPromote;
        SerializedProperty promotionCost;
        SerializedProperty cardColor;
        SerializedProperty charGender;
        SerializedProperty charWeaponType;
        SerializedProperty unitTypes;
        SerializedProperty baseAttack;
        SerializedProperty baseSupport;
        SerializedProperty baseRange;

        bool skillFoldout = false;
        bool colorFoldout = false;
        bool genderFoldout = false;
        bool weaponFoldout = false;
        bool unitFoldout = false;
        bool rangeFoldout = false;

        void OnEnable()
        {
            cardNumber = serializedObject.FindProperty("cardNumber"); ;
            charTitle = serializedObject.FindProperty("charTitle");
            charQuote = serializedObject.FindProperty("charQuote");
            cardIllustrator = serializedObject.FindProperty("cardIllustrator");
            cardSkills = serializedObject.FindProperty("cardSkills");
            skillTypes = serializedObject.FindProperty("skillTypes");

            charName = serializedObject.FindProperty("charName");
            classTitle = serializedObject.FindProperty("classTitle");
            deploymentCost = serializedObject.FindProperty("deploymentCost");
            canPromote = serializedObject.FindProperty("canPromote");
            promotionCost = serializedObject.FindProperty("promotionCost");
            cardColor = serializedObject.FindProperty("cardColor");
            charGender = serializedObject.FindProperty("charGender");
            charWeaponType = serializedObject.FindProperty("charWeaponType");
            unitTypes = serializedObject.FindProperty("unitTypes");
            baseAttack = serializedObject.FindProperty("baseAttack");
            baseSupport = serializedObject.FindProperty("baseSupport");
            baseRange = serializedObject.FindProperty("baseRange");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(cardNumber);
            EditorGUILayout.PropertyField(charTitle);
            EditorGUILayout.PropertyField(charQuote);
            EditorGUILayout.PropertyField(cardIllustrator);
            EditorGUILayout.PropertyField(cardSkills, true, GUILayout.ExpandHeight(true));
            skillFoldout = ShowCipherList(skillFoldout, skillTypes, typeof(CipherData.SkillTypesEnum));

            EditorGUILayout.PropertyField(charName);
            EditorGUILayout.PropertyField(classTitle);
            EditorGUILayout.PropertyField(deploymentCost);
            EditorGUILayout.PropertyField(canPromote);
            EditorGUILayout.PropertyField(promotionCost);

            colorFoldout = ShowCipherList(colorFoldout, cardColor, typeof(CipherData.ColorsEnum));
            genderFoldout = ShowCipherList(genderFoldout, charGender, typeof(CipherData.GendersEnum));
            weaponFoldout = ShowCipherList(weaponFoldout, charWeaponType, typeof(CipherData.WeaponsEnum));
            unitFoldout = ShowCipherList(unitFoldout, unitTypes, typeof(CipherData.UnitTypesEnum));

            EditorGUILayout.PropertyField(baseAttack);
            EditorGUILayout.PropertyField(baseSupport);

            rangeFoldout = ShowCipherList(rangeFoldout, baseRange, typeof(CipherData.RangesEnum));


            serializedObject.ApplyModifiedProperties();
        }

        // This method helps draw all boolean arrays for the different card attributes consistently.
        private bool ShowCipherList(bool foldout, SerializedProperty list, Type cipherEnum)
        {
            //check in case we try to show an non-array or pass in a Type that's not an Enum!
            if (!list.isArray)
            {
                EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
                return foldout;
            }
            else if (!cipherEnum.IsEnum)
            {
                EditorGUILayout.HelpBox(cipherEnum.Name + " is not an Enum!", MessageType.Error);
                return foldout;
            }

            //Begins a foldout group and returns the value based on user input.
            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, list.displayName);
            
            if (foldout)
            {
                EditorGUI.indentLevel += 1;
                SerializedProperty size = list.FindPropertyRelative("Array.size");
                EditorGUILayout.PropertyField(size);
                string[] enumNames = Enum.GetNames(cipherEnum);
                for (int i = 0; i < list.arraySize; i++)
                {
                    string name = "";
                    if (Enum.IsDefined(cipherEnum,i))
                        name = enumNames[i];

                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent(name));
                }
                EditorGUI.indentLevel -= 1;
            }

            // Be sure to end the Foldout Group.
            EditorGUILayout.EndFoldoutHeaderGroup();

            return foldout;
        }



        /*

        //Custom Editor for the Color List 
        public static bool ShowColorList(bool colorFoldout, SerializedProperty list)
        {
            //check in case we try to show an non-array!
            if (!list.isArray)
            {
                EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
                return colorFoldout;
            }

            colorFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(colorFoldout, list.displayName);


            //EditorGUILayout.PropertyField(list, false);

            
            if (colorFoldout)
            {
                EditorGUI.indentLevel += 1;
                SerializedProperty size = list.FindPropertyRelative("Array.size");
                EditorGUILayout.PropertyField(size);
                for (int i = 0; i < list.arraySize; i++)
                {
                    string name = "";
                    if (Enum.IsDefined(typeof(CipherData.ColorsEnum), i))
                        name = ((CipherData.ColorsEnum)i).ToString();

                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent(name));
                }
                EditorGUI.indentLevel -= 1;
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            return colorFoldout;
        }

        //Custom Editor for the Gender List 
        public static void ShowGenderList(SerializedProperty list)
        {
            //check in case we try to show an non-array!
            if (!list.isArray)
            {
                EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
                return;
            }

            EditorGUILayout.PropertyField(list);

            EditorGUI.indentLevel += 1;
            if (list.isExpanded)
            {
                SerializedProperty size = list.FindPropertyRelative("Array.size");
                EditorGUILayout.PropertyField(size);
                for (int i = 0; i < list.arraySize; i++)
                {
                    string name = "";
                    if (Enum.IsDefined(typeof(CipherData.GendersEnum), i))
                        name = ((CipherData.GendersEnum)i).ToString();

                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent(name));
                }
            }

            EditorGUI.indentLevel -= 1;
        }

        //Custom Editor for the Weapon List
        public static void ShowWeaponList(SerializedProperty list)
        {
            //check in case we try to show an non-array!
            if (!list.isArray)
            {
                EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
                return;
            }

            EditorGUILayout.PropertyField(list);

            EditorGUI.indentLevel += 1;
            if (list.isExpanded)
            {
                SerializedProperty size = list.FindPropertyRelative("Array.size");
                EditorGUILayout.PropertyField(size);
                for (int i = 0; i < list.arraySize; i++)
                {
                    string name = "";
                    if (Enum.IsDefined(typeof(CipherData.WeaponsEnum), i))
                        name = ((CipherData.WeaponsEnum)i).ToString();

                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent(name));
                }
            }

            EditorGUI.indentLevel -= 1;
        }

        //Custom Editor for the Unit Types List
        public static void ShowUnitList(SerializedProperty list)
        {
            //check in case we try to show an non-array!
            if (!list.isArray)
            {
                EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
                return;
            }

            EditorGUILayout.PropertyField(list);

            EditorGUI.indentLevel += 1;
            if (list.isExpanded)
            {
                SerializedProperty size = list.FindPropertyRelative("Array.size");
                EditorGUILayout.PropertyField(size);
                for (int i = 0; i < list.arraySize; i++)
                {
                    string name = "";
                    if (Enum.IsDefined(typeof(CipherData.TypesEnum), i))
                        name = ((CipherData.TypesEnum)i).ToString();

                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent(name));
                }
            }

            EditorGUI.indentLevel -= 1;
        }

        //Custom Editor for the Range List
        public static void ShowRangeList(SerializedProperty list)
        {
            //check in case we try to show an non-array!
            if (!list.isArray)
            {
                EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
                return;
            }

            EditorGUILayout.PropertyField(list);

            EditorGUI.indentLevel += 1;
            if (list.isExpanded)
            {
                SerializedProperty size = list.FindPropertyRelative("Array.size");
                EditorGUILayout.PropertyField(size);
                for (int i = 0; i < list.arraySize; i++)
                {
                    string name = "";
                    if (Enum.IsDefined(typeof(CipherData.RangesEnum), i))
                        name = ((CipherData.RangesEnum)i).ToString();

                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent(name));
                }
            }

            EditorGUI.indentLevel -= 1;
        }

        //Custom Editor for the Skill Types List
        public static void ShowSkillTypeList(SerializedProperty list)
        {
            //check in case we try to show an non-array!
            if (!list.isArray)
            {
                EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
                return;
            }

            EditorGUILayout.PropertyField(list);

            EditorGUI.indentLevel += 1;
            if (list.isExpanded)
            {
                SerializedProperty size = list.FindPropertyRelative("Array.size");
                EditorGUILayout.PropertyField(size);
                for (int i = 0; i < list.arraySize; i++)
                {
                    string name = "";
                    if (Enum.IsDefined(typeof(CipherData.SkillTypeEnum), i))
                        name = ((CipherData.SkillTypeEnum)i).ToString();

                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent(name));
                }
            }

            EditorGUI.indentLevel -= 1;
        }
        */
    }
}
