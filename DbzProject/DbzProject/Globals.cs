global using System;

global using CommunityToolkit.Maui;
global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Mvvm.ComponentModel;


global using Microcharts.Maui;
global using Microcharts;
global using SkiaSharp;


global using DbzProject.View;
global using DbzProject.ViewModel;
global using DbzProject.Model;
global using DbzProject.Service;


public class Globals
{
    public static List<DbzCharacter> myCharacter = new();
    public static User currentUser = null;
    public static List<DbzCharacter> myDbzCharactersFromDb = new();
    public static string comConnected = string.Empty;


    public static void popUP(string title, string content, string buttonTxt)
    {
        Shell.Current.DisplayAlert(title, content, buttonTxt);
    }

}