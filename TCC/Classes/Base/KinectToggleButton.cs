using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;
using Microsoft.Kinect.Toolkit.Interaction;//.KinectToggleButton ;
using System.Windows;
using System.Windows.Media;

namespace TCC.Classes.Base
{
    public class KinectToggleButton : KinectTileButton
    {
        public bool IsChecked { get; set; }
        public KinectToggleButton()
        {
            Click += AlterarEstado;
            //this.Background = Brushes.RoyalBlue;
        }
        private void AlterarEstado(object sender, RoutedEventArgs e)
        {
            IsChecked = !IsChecked;
/*            if (IsChecked)
                this.Background = Brushes.DarkBlue;
            else
                this.Background = Brushes.RoyalBlue;*/
        }
        
    }
}
