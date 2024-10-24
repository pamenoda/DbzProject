using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbzProject.Service;

public partial class DeviceOrientationService // PAS METTRE DE CODE GÉNÉRIQUE DANS CETTE PARTIE DU PROJET --> FAIS BUG
{
    SerialPort? mySerialPort;
    

    public partial void ConfigureScanner(bool isOpen)
    {
      
        try{
            if (Globals.comConnected != string.Empty) 
            {
                if (isOpen)
                {
                    try 
                    {
                        mySerialPort = new SerialPort();
                        mySerialPort.BaudRate = 9600;
                        mySerialPort.PortName = Globals.comConnected;
                        mySerialPort.Parity = Parity.None;
                        mySerialPort.DataBits = 8;
                        mySerialPort.StopBits = StopBits.One;
                        mySerialPort.ReadTimeout = 1000;
                        mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataHandler);
                        mySerialPort.Open();

                    }catch (Exception ex) 
                    {
                      //  Globals.popUP("erreur d'ouverture du port ", "probleme de connexion via port ", "OK");
                    }
                   

                }
                else if (!isOpen) mySerialPort.Close();
            }
            
        }
        catch(Exception ex)
        {
            Shell.Current.DisplayAlert("Error", ex.ToString(),"OK");
        }
    }
   private void DataHandler(object sender, EventArgs ard)
    {
        SerialPort sp = (SerialPort)sender;
        SerialBuffer.Enqueue(sp.ReadTo("\r"));

    }


}