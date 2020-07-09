using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetris_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GameBoard.Focus();
            GameBoard.InitalizeGrid();
            GameBoard.DropBrick();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) GameBoard._currentBlock.Rotate();
            else if (e.Key == Key.Down) GameBoard._currentBlock.MoveDown();
            else if (e.Key == Key.Left) GameBoard._currentBlock.MoveLeft();
            else if (e.Key == Key.Right) GameBoard._currentBlock.MoveRight();

            //Redraw all Game Board Graphics
            GameBoard.ClearGrid();
            GameBoard.DrawShape(GameBoard._currentBlock, false);
        }
    }
}
