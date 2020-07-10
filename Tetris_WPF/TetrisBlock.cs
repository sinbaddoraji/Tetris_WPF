using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris_WPF
{
    public class TetrisBlock
    {
        /// <summary>
        /// Points filled by an abstract 4 x 4 grid
        /// </summary>
        public bool[][] blockPoints;
        /// <summary>
        /// Width and height of the tetris block
        /// </summary>
        public int width = 1, height = 1;
        /// <summary>
        /// X and Y values of current tetris Block
        /// </summary>
        public int x = 5, y = 0;
        /// <summary>
        /// Enum holding the names of different block types
        /// </summary>
        public enum BlockType
        {
            IBlock, IBlock2, 
            JBlock, JBlock2, JBlock3, JBlock4,
            LBlock, LBlock2, LBlock3, LBlock4,
            OBlock,
            SBlock, SBlock2,
            TBlock, TBlock1, TBlock2, TBlock3,
            ZBlock, ZBlock2
        }
        /// <summary>
        /// BlockType of current TetrisBlock
        /// </summary>
        public BlockType _blockType = (BlockType)KeyBlocks[randomizer.Next(0, 7)];
        /// <summary>
        /// Object for creating random values in Game
        /// </summary>
        public static readonly Random randomizer = new Random();
        /// <summary>
        /// Colour of Tetris Brick
        /// </summary>
        public Brush BrickColour;
        /// <summary>
        /// Parent Game Board
        /// </summary>
        public Board GameBoard;
        /// <summary>
        /// Array holding indexes of key Tetris blocks
        ///     (tetris Blocks not yet rotated)
        /// </summary>
        private static readonly int[] KeyBlocks = new[] { 0, 2, 6, 10, 11, 13, 16 };
        /// <summary>
        /// Tetris Block object class
        /// </summary>
        public TetrisBlock(Board GameBoard)
        {
            this.GameBoard = GameBoard;
            InitalizeBlock();
        }
        /// <summary>
        /// Fill abstract grid row with true values
        /// </summary>
        private void FillRow(int row, int from, int num)
        {
            for (int i = from + num - 1; i >= from; i--)
                blockPoints[row][i] = true;
        }
        /// <summary>
        /// Initialize tetris block properties
        /// </summary>
        public void InitalizeBlock()
        {
            blockPoints = new bool[][]
            {
                new[] { false, false, false, false },
                new[] { false, false, false, false },
                new[] { false, false, false, false },
                new[] { false, false, false, false },
            };

            switch (_blockType)
            {
                case BlockType.IBlock:
                    //*
                    //*
                    //*
                    //*
                    FillRow(0, 0, 1);
                    FillRow(1, 0, 1);
                    FillRow(2, 0, 1);
                    FillRow(3, 0, 1);
                    width = 0;
                    height = 3;
                    break;
                case BlockType.IBlock2:
                    //****
                    FillRow(0, 0, 4);
                    width = 3; height = 0;
                    break;
                case BlockType.JBlock:
                    // *
                    // *
                    //**
                    FillRow(0, 1, 1);
                    FillRow(1, 1, 1);
                    FillRow(2, 0, 2);
                    width = 1;
                    height = 2;
                    break;
                case BlockType.JBlock2:
                    //*
                    //***
                    FillRow(0, 0, 1);
                    FillRow(1, 0, 3);
                    width = 2;
                    height = 1;
                    break;

                case BlockType.JBlock3:
                    //**
                    //*
                    //*
                    FillRow(0, 0, 2);
                    FillRow(1, 0, 1);
                    FillRow(2, 0, 1);
                    width = 1;
                    height = 2;
                    break;
                case BlockType.JBlock4:
                    //***
                    //  *
                    FillRow(0, 0, 3);
                    FillRow(1, 2, 1);
                    width = 2;
                    height = 1;
                    break;

                case BlockType.LBlock:
                    //*
                    //*
                    //**
                    FillRow(0, 0, 1);
                    FillRow(1, 0, 1);
                    FillRow(2, 0, 2);
                    width = 1;
                    height = 2;
                    break;
                case BlockType.LBlock2:
                    //  *
                    //***
                    FillRow(0, 2, 1);
                    FillRow(1, 0, 3);
                    width = 2;
                    height = 1;
                    break;
                case BlockType.LBlock3:
                    //**
                    // *
                    // *
                    FillRow(0, 0, 2);
                    FillRow(1, 1, 1);
                    FillRow(2, 1, 1);
                    width = 1;
                    height = 2;
                    break;
                case BlockType.LBlock4:
                    //***
                    //*
                    FillRow(0, 0, 3);
                    FillRow(1, 0, 1);
                    width = 2;
                    height = 1;
                    break;
                
                case BlockType.OBlock:
                    //**
                    //**
                    FillRow(0, 0, 2);
                    FillRow(1, 0, 2);
                    width = 1;
                    height = 1;
                    break;
                case BlockType.SBlock:
                    // **
                    //**
                    FillRow(0, 1, 2);
                    FillRow(1, 0, 2);
                    width = 2;
                    height = 1;
                    break;

                case BlockType.SBlock2:
                    //*
                    //**
                    // *
                    FillRow(0, 0, 1);
                    FillRow(1, 0, 2);
                    FillRow(2, 1, 1);
                    width = 1;
                    height = 2;
                    break;

                case BlockType.TBlock:
                    // *
                    //***
                    FillRow(0, 1, 1);
                    FillRow(1, 0, 3);
                    width = 2;
                    height = 1;
                    break;

                case BlockType.TBlock1:
                    //*
                    //**
                    //*
                    FillRow(0, 0, 1);
                    FillRow(1, 0, 2);
                    FillRow(2, 0, 1);
                    width = 1;
                    height = 2;
                    break;
                case BlockType.TBlock2:
                    // *
                    //***
                    FillRow(0, 0, 3);
                    FillRow(1, 1, 1);
                    
                    width = 2;
                    height = 1;
                    break;
                case BlockType.TBlock3:
                    // *
                    //**
                    // *
                    FillRow(0, 1, 1);
                    FillRow(1, 0, 2);
                    FillRow(2, 1, 1);
                    width = 1;
                    height = 2;
                    break;

                case BlockType.ZBlock:
                    //**
                    // **
                    FillRow(0, 0, 2);
                    FillRow(1, 1, 2);
                    width = 2;
                    height = 1;
                    break;
                
                case BlockType.ZBlock2:
                    // *
                    //**
                    //*
                    FillRow(0, 1, 1);
                    FillRow(1, 0, 2);
                    FillRow(2, 0, 1);
                    width = 1;
                    height = 2;
                    break;
            }
        }
        /// <summary>
        /// Rotate tetris block
        /// </summary>
        public void Rotate()
        {
            switch (_blockType)
            {
                case BlockType.IBlock:
                    _blockType = BlockType.IBlock2;
                    break;
                case BlockType.IBlock2:
                    _blockType = BlockType.IBlock;
                    break;
                case BlockType.JBlock:
                    _blockType = BlockType.JBlock2;
                    break;
                case BlockType.JBlock2:
                    _blockType = BlockType.JBlock3;
                    break;
                case BlockType.JBlock3:
                    _blockType = BlockType.JBlock4;
                    break;
                case BlockType.JBlock4:
                    _blockType = BlockType.JBlock;
                    break;
                case BlockType.LBlock:
                    _blockType = BlockType.LBlock2;
                    break;
                case BlockType.LBlock2:
                    _blockType = BlockType.LBlock3;
                    break;
                case BlockType.LBlock3:
                    _blockType = BlockType.LBlock4;
                    break;
                case BlockType.LBlock4:
                    _blockType = BlockType.LBlock;
                    break;
                case BlockType.OBlock: return;
                case BlockType.SBlock:
                    _blockType = BlockType.SBlock2;
                    break;
                case BlockType.SBlock2:
                    _blockType = BlockType.SBlock;
                    break;
                case BlockType.TBlock:
                    _blockType = BlockType.TBlock1;
                    break;
                case BlockType.TBlock1:
                    _blockType = BlockType.TBlock2;
                    break;
                case BlockType.TBlock2:
                    _blockType = BlockType.TBlock3;
                    break;
                case BlockType.TBlock3:
                    _blockType = BlockType.TBlock;
                    break;
                case BlockType.ZBlock:
                    _blockType = BlockType.ZBlock2;
                    break;
                case BlockType.ZBlock2:
                    _blockType = BlockType.ZBlock;
                    break;
            }
            InitalizeBlock();
        }
        /// <summary>
        /// Move tetris block left
        /// </summary>
        public void MoveLeft()
        {
            if (x == 0) return;
            x--;

            var a = GameBoard.DroppedBrickCordinates.ToList();
            var b = GameBoard.GetBoardGridPoints(this).ToList();
            var c = new List<int[]>();

            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < b.Count; j++)
                {
                    if (!a[i].SequenceEqual(b[j])) continue;
                    c.Add(b[j]);
                }
            }

            if (a.Count > 0 && c.Count > 0)
            {
                x++;
            }
        }
        /// <summary>
        /// Move tetris block right
        /// </summary>
        public void MoveRight()
        {
            if (width + x >= 10) return;
            x++;

            var a = GameBoard.DroppedBrickCordinates.ToList();
            var b = GameBoard.GetBoardGridPoints(this).ToList();
            var c = new List<int[]>();

            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < b.Count; j++)
                {
                    if (!a[i].SequenceEqual(b[j])) continue;
                    c.Add(b[j]);
                }
            }

            if (a.Count > 0 && c.Count > 0)
            {
                x--;
            }
        }
        /// <summary>
        /// Move tetris block and assert if it has landed
        /// </summary>
        public bool MoveDown()
        {
            if (height + y >= 9) return true;
            y++;

            var a = GameBoard.DroppedBrickCordinates.ToList();
            var b = GameBoard.GetBoardGridPoints(this).ToList();
            var c = new List<int[]>();


            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < b.Count; j++)
                {
                    if (!a[i].SequenceEqual(b[j])) continue;
                    c.Add(b[j]);
                }
            }

            if (a.Count > 0 && c.Count > 0)
            {
                y--;
                return true;
            }

            return false; //Has not landed
        }

    }
}
