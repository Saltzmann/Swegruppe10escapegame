﻿using System;
using System.Collections.Generic;
using System.IO;
using Assets.Code.GLOBALS;
using UnityEngine;

namespace Assets.Code.Models {

    [Serializable]
    public class Question {
        public List<Answer> Answers { get; set; }
        public string QuestionText { get; set; }
        private string _imgPath { get; set; }

        public string ImagePath {
            get {
                if (!string.IsNullOrEmpty(_imgPath))
                {
                    var a = _imgPath.Split(new string[] { "The Mighty Quest For Epic Grades" }, StringSplitOptions.None);
                    var b = Application.persistentDataPath;
                    var c = b + a[1];
                    return c;
                }
                return null;
            }
            set { _imgPath = value; }
        }

        public List<string> Hints { get; set; }
        public Difficulties Difficulty { get; set; }
        public string Modul { get; set; }
        public bool Used { get; set; }
        public string Chapter { get; set; }
        public int CorrectAnswer { get; set; }
        public TimeSpan QuestionDuration { get; set; }

        [Serializable]
        public class Answer {
            public string AnswerText { get; set; }
            private string _imgPath { get; set; }

            public string ImagePath {
                get {
                    if (!string.IsNullOrEmpty(_imgPath))
                        return Path.GetFullPath(_imgPath);
                    return _imgPath;
                }
                set { _imgPath = value; }
            }
        }

        public override string ToString()
        {
            return ("Frage: " + QuestionText +
                    "\nFragenbildpfad: " + _imgPath +
                        "\nAntwort1: " + Answers[0].AnswerText +
                        "\nAntwort1Bildpfad: " + Answers[0].ImagePath +
                        "\nAntwort2: " + Answers[1].AnswerText +
                        "\nAntwort2Bildpfad: " + Answers[1].ImagePath +
                        "\nAntwort3: " + Answers[2].AnswerText +
                        "\nAntwort3Bildpfad: " + Answers[2].ImagePath +
                        "\nHinweis1: " + Hints[0] +
                        "\nHinweis2: " + Hints[1] +
                        "\nHinweis3: " + Hints[2] +
                        "\nRichtige Antwort: " + CorrectAnswer +
                        "\nModul: " + Modul +
                        "\nSchwierigkeitsgrad: " + Difficulty +
                        "\nKapitel: " + Chapter +
                        "\nGenutzt: " + Used +
                        "\nZeit: " + QuestionDuration);
        }
    }
}