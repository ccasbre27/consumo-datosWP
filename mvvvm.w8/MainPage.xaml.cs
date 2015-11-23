using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MVVM.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MVVVM.W8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ServiceModel serviceModel = new ServiceModel();
        public MainPage()
        {
            this.InitializeComponent();
            serviceModel.GetCamerasCompleted += serviceModel_GetCamerasCompleted;
            this.Loaded += MainPage_Loaded;
        }

        void serviceModel_GetCamerasCompleted(object sender, FlickCamerasEventArgs e)
        {
            gridView.ItemsSource = e.Results;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            serviceModel.getCameras();
        }
    }
}
