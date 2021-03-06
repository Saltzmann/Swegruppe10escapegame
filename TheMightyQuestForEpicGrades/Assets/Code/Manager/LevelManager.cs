﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Assets.Code.Scripts.FeatureScripts;
using Assets.Code.Scripts.UtilityScripts;
using UnityEngine;

namespace Assets.Code.Manager {
    public class LevelManager : MonoBehaviour {

        public readonly string ExecutingDirectory = Environment.CurrentDirectory;

        //Temporärer Link
        public BoardBuilderScript BoardBuilder_TMP;

        private char[,] _levelData;
        private int _loadedLevelIndex;

        #region Dateinamen-Konfiguration
        private readonly string Dateiendung = ".txt";
        private readonly string Dateipfad = "./Levels/";
        private readonly string Dateiprefix = "Level_";
        #endregion

        public void LoadFromFile(int index) {
            var Dateiname = Dateiprefix + index + Dateiendung;
            var levelData = new List<string>();
            levelData.Clear();

            using (var Datei = new StreamReader(Dateipfad + Dateiname)) {
                string data;

                while ((data = Datei.ReadLine()) != null)
                    levelData.Add(data);
            }
            _loadedLevelIndex = index;
            _levelData = to2dCharArray(levelData);
        }

        public void CopyFileToLevelFolder(string filePathToAdd) {
            var levelNumberToAdd = findNextFileName();
            
            File.Copy(filePathToAdd, Master.Instance().MyLevel.ExecutingDirectory + "\\Levels\\Level_" + levelNumberToAdd + ".txt");
            levelNumberToAdd++;
            Debug.Log("Datei: " + filePathToAdd + "\n" + "Kopiert in Level-Ordner als:" + "Level_" + (levelNumberToAdd - 1) +
                ".txt");
        }
        
        //Hilfsfunktionen
        private int findNextFileName() {
            var levelNumberToAdd = 1;
            for (var i = 1; i < 200; i++)
                if (File.Exists(Master.Instance().MyLevel.ExecutingDirectory + "\\Levels\\Level_" + i + ".txt"))
                    levelNumberToAdd++;
                else
                    break;
            //Debug.Log("Letztes gefundenes Level: " + (levelNumberToAdd - 1));
            return levelNumberToAdd;
        }

        private char[,] to2dCharArray(List<string> list) {
            var charArray2d = new char[list.Count, list[0].Length];

            for (var i = 0; i < list.Count; ++i)
                for (var j = 0; j < list[i].Length; ++j)
                    charArray2d[i, j] = list[i][j];

            return charArray2d;
        }

        public int GetLoadedLevelIndex() {
            return _loadedLevelIndex;
        }

        public char[,] GetLevelData() {
            return _levelData;
        }

        public int[] GetAllUseableLevelIndices() {
            var allLevelFileNames = Persist.GetAllLevelFileNames();
            var levelIndices = new int[allLevelFileNames.Count];

            for (var index = 0; index < allLevelFileNames.Count; index++) {
                var fileName = allLevelFileNames[index];
                var indexOfLevel = -1;
                if (!int.TryParse(fileName[fileName.Length - 1].ToString(), out indexOfLevel)) {
                    Debug.LogError("Gelesene Level: " + fileName + "\nIntepretierter Index = " + indexOfLevel);
                    throw new Exception("Parsen oder sonstwas schief gelaufen :o");
                }
                levelIndices[index] = indexOfLevel;
            }

            return levelIndices;

            //Linq Version
            //return Persist.GetAllLevelFileNames().Select(
            //    x => {
            //        var fileName = Path.GetFileNameWithoutExtension(x);
            //        return Int32.Parse(fileName[fileName.Length - 1].ToString());
            //    }).ToArray();
        }
    }
}