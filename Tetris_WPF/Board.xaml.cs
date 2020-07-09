using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris_WPF
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : UserControl
    {
        /// <summary>
        /// Tetris Block colours
        /// </summary>
        private readonly Brush[] _colours = new[] { Brushes.Green, Brushes.Blue, Brushes.Orange, Brushes.Yellow, Brushes.Purple, Brushes.Red };
        /// <summary>
        /// Timer handling block drop
        /// </summary>
        private System.Windows.Forms.Timer _dropTimer;
        /// <summary>
        /// x and y values of bricks that have dropped
        /// </summary>
        public List<int[]> landedXY
        {
            get
            {
                List<int[]> output = new List<int[]>();
                for (int y = 10; y >= 0; y--)
                {
                    for (int x = 10; x >= 0; x--)
                    {
                        if (GridArray[y][x].Dropped)

                        {
                            output.Add(new[] { y, x });
                        }
                    }
                }
                return output;
            }
        }
        /// <summary>
        /// Current playing block
        /// </summary>
        public TetrisBlock _currentBlock;
        /// <summary>
        /// Drop rate of currentBlock
        /// </summary>
        private int _dropRate = 1030;
        /// <summary>
        /// 11 X 11 Grid holding tetris blocks
        /// </summary>
        public Brick[][] GridArray;
        /// <summary>
        /// Board for Tetris Game
        /// </summary>
        public Board()
        {
            InitializeComponent();
            InitalizeGrid();
        }
        /// <summary>
        /// Create new instance of tetris playing block
        /// </summary>
        public void DropBrick()
        {
            _currentBlock = new TetrisBlock(this) { BrickColour = _colours[TetrisBlock.randomizer.Next(0, 6)] };

            System.Windows.Forms.Timer focusTimer = new System.Windows.Forms.Timer { Interval = 1 };
            focusTimer.Tick += FocusTimer_Tick;
            focusTimer.Start();

            if(_dropRate > 500)
            _dropRate -= 10;
            _dropTimer = new System.Windows.Forms.Timer { Interval = _dropRate };
            _dropTimer.Tick += DropTimer_Tick;
            _dropTimer.Start();

            ClearGrid();
            DrawShape(_currentBlock, false);
        }
        /// <summary>
        /// Timer to focus on playing board every second to allow use of arrow keys
        /// </summary>
        private void FocusTimer_Tick(object sender, EventArgs e) => Focus();
        /// <summary>
        /// Clear game board for next drawing
        /// </summary>
        public void ClearGrid()
        {
            for (int y = 10; y >= 0; y--)
            {
                for (int x = 10; x >= 0; x--)
                {
                    GridArray[y][x].Background = Brushes.Black;
                }
            }
            RedrawOldShapes();
        }
        /// <summary>
        /// Clear game board for next drawing
        /// </summary>
        public void RedrawOldShapes()
        {
            foreach (var brick in landedXY)
            {
                GridArray[brick[0]][brick[1]].Background = GridArray[brick[0]][brick[1]]._droppedColour;
            }
        }
        public IEnumerable<int[]> GetBoardGridPoints(TetrisBlock p)
        {
            var brickPos = p.blockPoints;
            for (int y = brickPos.Length - 1; y >= 0; y--)
            {
                for (int x = brickPos.Length - 1; x >= 0; x--)
                {
                    if (brickPos[y][x]) yield return new[] { y + p.y, x + p.x };
                }
            }

        }
        /// <summary>
        /// Draw current shape on grid at it's current location
        /// </summary>
        public void DrawShape(TetrisBlock p, bool isOnGround)
        {
            foreach (var brick in GetBoardGridPoints(p))
            {
                int y = brick[0];
                int x = brick[1];
                if(x >= 11) p.x = 10;

                try
                {
                    GridArray[y][x].Background = p.BrickColour;
                }
                catch (Exception)
                {

                }
                


                if (isOnGround)
                {
                    GridArray[y][x].Dropped = true;
                    GridArray[y][x]._droppedColour = p.BrickColour;
                    landedXY.Add(new[] { y, x });
                }
            }
        }
        /// <summary>
        /// Drop timer tick event
        ///     Handles tetris block dropping logic
        /// </summary>
        private void DropTimer_Tick(object sender, EventArgs e)
        {
            if(_currentBlock.MoveDown())
            {
                //Clear Grid and drop new shape
                ClearGrid();
                DrawShape(_currentBlock, true);
                DropBrick();

                foreach (var y in WinningLines())
                {
                    for (int x = 10; x >= 0; x--)
                    {
                        GridArray[y][x].Dropped = false;
                        GridArray[y][x]._droppedColour = Brushes.Black;
                        foreach (var item in landedXY.Where(b => b.SequenceEqual(new int[] { y, x })))
                        {
                            landedXY.Remove(item);
                        }
                    }
                    foreach (var item in landedXY)
                    {
                        if (item[0] < y)
                        {
                            GridArray[item[0]][item[1]].Dropped = false;
                            GridArray[item[0] + 1][item[1]].Dropped = true;

                            GridArray[item[0] + 1][item[1]]._droppedColour = GridArray[item[0]][item[1]]._droppedColour;
                            GridArray[item[0]][item[1]]._droppedColour = Brushes.Black;
                            item[0]++;
                        }
                    }
                }


                //Get rid of old drop timer object
                _dropTimer.Stop();
                _dropTimer = null;
            }
            else
            {
                ClearGrid();
                DrawShape(_currentBlock, false);
            }
        }

        private IEnumerable<int> WinningLines()
        {
            for (int y = 10; y >= 0; y--)
            {
                bool win = true;
                for (int x = 10; x >= 0; x--)
                {
                    win &= GridArray[y][x].Dropped;
                }
                if (win) yield return y;
            }
        }

        /// <summary>
        /// Initialize Game Board items used in game logic
        /// </summary>
        public void InitalizeGrid()
        {
            GridArray = new Brick[11][];
            for (int i = 0; i < 11; i++)
            {
                GridArray[i] = new Brick[11];
                for (int j = 0; j < 11; j++)
                {
                    GridArray[i][j] = new Brick()
                    {
                        Width = 50,
                        Height = 50
                    };

                    Grid.SetRow(GridArray[i][j], i);
                    Grid.SetColumn(GridArray[i][j], j);

                    BoardGrid.Children.Add(GridArray[i][j]);
                }
            }
        }
    }
}
