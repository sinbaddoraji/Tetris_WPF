using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Tetris_WPF
{
    public class Brick : UserControl
    {
        public bool Dropped = false;
        public Brush _droppedColour;

        public Brick()
        {
            Focusable = false;
            BorderBrush = Brushes.Black;
            Background = Brushes.Black;
            BorderThickness = new Thickness
            {
                Left = 1,
                Right = 1,
                Top = 1,
                Bottom = 1
            };
        }
    }
}
