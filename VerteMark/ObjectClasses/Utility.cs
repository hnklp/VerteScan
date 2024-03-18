﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace VerteMark.ObjectClasses {
    /// <summary>
    /// Třída obsahující metody, které slouží jako interface pro UI
    /// (Prakticky metody co se volají tlačítky)
    /// </summary>
    internal class Utility {
        // Vlastnosti
        private static Utility instance;
        Project project;

        // Konstruktor
        public Utility() {
            project = new Project();
        }

        public void SaveBitmapToFile(BitmapSource bitmap, SaveFileDialog saveFileDialog)
        {
            // Create a SaveFileDialog to prompt the user for file save location
            // Show the dialog and get the result
            if (saveFileDialog.ShowDialog() == true)
            {
                // Create a BitmapEncoder based on the selected file format
                BitmapEncoder encoder = null;
                switch (System.IO.Path.GetExtension(saveFileDialog.FileName).ToUpper())
                {
                    case ".PNG":
                        encoder = new PngBitmapEncoder();
                        break;
                    case ".JPG":
                        encoder = new JpegBitmapEncoder();
                        break;
                    case ".BMP":
                        encoder = new BmpBitmapEncoder();
                        break;
                    default:
                        // Unsupported file format
                        return;
                }

                // Encode and save the bitmap to the selected file path
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    encoder.Save(stream);
                }
            }
        }
        public void LoginUser(string id, bool validator) {
            project.LoginNewUser(id, validator);
        }
        public void LogoutUser() {
            project.LogoutUser();
        }
        public User? GetLoggedInUser() {
            return project.GetLoggedInUser();
        }
        public BitmapImage? GetOriginalPicture() {
            return project.GetOriginalPicture();
        }

        // Returns true if project was loaded (in any way), returns false if loading has failed
        public bool ChooseProjectFolder(string path) {
            return project.TryOpeningProject(path);
        }
        public void SaveProject() {
             
        }
        public void ChangeSelectedAnotation(int id) {

        }
        public void UpdatedSelectedAnotation(int idAnotace) {
            project.UpdateAnotaceCanvas(idAnotace);
        }
        public void ClearSelectedAnotation() {

        }
        public void SwitchAnotationValidation(int id) {

        }
        // Vrátí JPEG/PNG toho krku aby se to mohlo načíst
        public void GetMainPicture() {

        }

        // Metoda pro získání instance třídy
        public static Utility GetInstance() {
            if (instance == null) {
                instance = new Utility();
            }
            return instance;
        }

    }
}