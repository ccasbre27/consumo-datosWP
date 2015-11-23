using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM.Common;

namespace MVVM.ViewModels
{
    public class CamerasViewModel : NotificationEnabledObject
    {
        // el ObservableCollection es adecuado para enlaces de datos en las tecnologías xaml
        private ObservableCollection <Camera> cameraList;
        ServiceModel serviceModel = new ServiceModel();

        public CamerasViewModel()
        {
            serviceModel.GetCamerasCompleted += (s, a) =>
                {
                    cameraList = new ObservableCollection<Camera>(a.Results);
                };
        }
        public ObservableCollection <Camera> CameraList
        {
            get 
            { 
                if (cameraList == null)
                {
                    cameraList = new ObservableCollection<Camera>();
                }

                // esto es para agregar elementos y tener blendability, si se está en tiempo de diseño entonces se muestren datos de ejemplo
                if(DesignerProperties.IsInDesignTool)
                {
                    for (int i = 0 ; i < 20 ; i++)
                    {
                        cameraList.Add(new Camera() { Name = "Camara #" });
                    }
                }

                return cameraList; 
            }
            set 
            { 
                cameraList = value;
                OnPropertyChanged();
            }
        }

        ActionCommand getCamerasCommand;
        public ActionCommand GetCamerasCommand
        {
            get 
            { 
                if (getCamerasCommand == null)
                {
                    getCamerasCommand = new ActionCommand(() => 
                    {
                        serviceModel.getCameras();
                    });
                }
                return getCamerasCommand;
            }
            
        }
        
    }
}
