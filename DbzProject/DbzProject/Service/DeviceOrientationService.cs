    using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbzProject.Service; //ducp tous les autes endroits dans Platforms contenant DeviceOrientation doivent avoir le meme namespace

public partial class DeviceOrientationService //et donc partt public partial
{
    public QueueBuffer SerialBuffer = new();
    //Queue --> methode Enqueue est virtual et peut donc etre redefinie
    public partial void ConfigureScanner(bool isOpen);

    public class QueueBuffer : Queue
    {
        public event EventHandler ?Changed;
        public override void Enqueue(object obj)
        {
            base.Enqueue(obj);
            Changed?.Invoke(this,EventArgs.Empty);
        }
    }

    public static void fetchedComPort(ObservableCollection<string> availablePorts) 
    {
        availablePorts.Clear();
        // on ajoute les com ports disponibles sur le pc 
        foreach (var port in SerialPort.GetPortNames())
        {
            availablePorts.Add(port);
        }
        
    }
}