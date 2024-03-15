﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VerteMark.ObjectClasses {
    /// <summary>
    /// Hlavní třída projektu.
    /// Propojuje ostatní třídy a drží informace o aktuálním stavu 
    /// 
    /// Zahrnuje:
    /// * Všechny anotace
    /// * Přihlášeného uživatele
    /// * Manipulaci s anotacemi, uživatelem a soubory (nepřímo)
    /// </summary>
    internal class Project {

        FileManager fileManager;
        User? loggedInUser; // Info o uživateli
        List<Anotace> anotaces; // Objekty anotace
        BitmapImage? originalPicture; // Fotka toho krku
        Metadata? metadata; // Metadata projektu

        public Project() {
            fileManager = new FileManager();
            anotaces = new List<Anotace>();
            originalPicture = new BitmapImage();
        }

        public bool TryOpeningProject(string path) {
            FolderState folderState = fileManager.CheckFolderType(path);
            switch (folderState) {
                case FolderState.New:
                    CreateNewProject(path);
                    return true;
                case FolderState.Existing:
                    LoadProject(path);
                    return true;
                case FolderState.Nonfunctional:
                    return false;
            }
            return false;
        }
        public void CreateNewProject(string path) {
            // Vytvoř čistý metadata
            metadata = new Metadata();
            // Vytvoř čistý anotace
            CreateNewAnotaces();
            // Získej čistý (neoříznutý) obrázek do projektu ((filemanagerrrr))
            originalPicture = fileManager.GetPictureAsBitmapImage();
        }
        public void LoadProject(string path) {
            // Získej metadata
            metadata = fileManager.GetProjectMetada();
            // Získej anotace
            anotaces = fileManager.GetProjectAnotaces();
            // Získej uložený obrázek do projektu
            originalPicture = fileManager.GetPictureAsBitmapImage();
        }
        public void SaveProject() {
            // zavolá filemanager aby uložil všechny instance (bude na to možná pomocná třída co to dá dohromady jako 1 json a 1 csv)
            // záležitosti správných složek a správných formátů souborů má na starost filemanager
        }
        
        public void LoginNewUser(string id, bool validator) {
            loggedInUser = new User(id, validator);
        }
        public void LogoutUser() {
            loggedInUser = null;
        }

        void CreateNewAnotaces() {
            anotaces.Add(new Anotace(0, "C1", System.Drawing.Color.Red));
            anotaces.Add(new Anotace(1, "C2", System.Drawing.Color.Blue));
            anotaces.Add(new Anotace(2, "C3", System.Drawing.Color.Green));
            anotaces.Add(new Anotace(3, "C4", System.Drawing.Color.Yellow));
            anotaces.Add(new Anotace(4, "C5", System.Drawing.Color.Purple));
            anotaces.Add(new Anotace(5, "C6", System.Drawing.Color.MediumPurple));
            anotaces.Add(new Anotace(6, "C7", System.Drawing.Color.RebeccaPurple));
            anotaces.Add(new Anotace(7, "Implantát", System.Drawing.Color.Magenta));
        }
        public void UpdateAnotaceCanvas(int idAnotace) {
            FindAnotaceById(idAnotace).UpdateCanvas();
        }

        void ClearAnotace(int idAnotace) {
            FindAnotaceById(idAnotace).ClearCanvas();
        }

        Anotace FindAnotaceById(int idAnotace) {
            Anotace? foundAnotace = anotaces.Find(anotace => anotace.Id == idAnotace);
            if (foundAnotace != null) {
                return foundAnotace;
            } else {
                throw new InvalidOperationException($"Anotace with ID {idAnotace} not found.");
            }
        }


    }
}
