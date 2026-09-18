using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Taquin
{
    internal class TileMovementHandler
    {
        private GameGrid grid;
        private Board board;
        private List<List<Cell>> gridBoard;

        public TileMovementHandler(GameGrid grid, Board board, List<List<Cell>> gridBoard)
        {
            this.grid = grid;
            this.board = board;
            this.gridBoard = gridBoard;
        }

        public void HandleTileClick(Button clickedButton)
        {
            if (clickedButton == null || clickedButton.Parent != grid) return;
            int column = grid.GetPositionFromControl(clickedButton).Column;
            int row = grid.GetPositionFromControl(clickedButton).Row;
            // Une position invalide ne doit jamais servir d'index dans la grille.
            if (row < 0 || row >= gridBoard.Count) return;
            if (column < 0 || column >= gridBoard[row].Count) return;
            int moveDirection = board.move_tile(row, column);

            if (moveDirection == 0)
            {
                int grid_size = board.Get_GridSize();
                if (TryHandleHorizontal(row, column, grid_size)) return;
                TryHandleVertical(row, column, grid_size);
            }
            else
            {
                swap_controls(row, column, moveDirection);
            }
        }

        private bool TryHandleHorizontal(int row, int column, int grid_size)
        {
            for (int col = 0; col < grid_size; col++)
            {
                if (!gridBoard[row][col].Is_occupied())
                {
                    if (column < col)
                    {
                        for (int j = col - 1; j >= column; j--)
                        {
                            board.move_tile(row, j);
                            swap_controls(row, j, 2);
                        }
                    }
                    else if (column > col)
                    {
                        for (int j = col + 1; j <= column; j++)
                        {
                            board.move_tile(row, j);
                            swap_controls(row, j, -2);
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        private void TryHandleVertical(int row, int column, int grid_size)
        {
            for (int r = 0; r < grid_size; r++)
            {
                if (!gridBoard[r][column].Is_occupied())
                {
                    if (row < r)
                    {
                        for (int j = r - 1; j >= row; j--)
                        {
                            board.move_tile(j, column);
                            swap_controls(j, column, -1);
                        }
                    }
                    else if (row > r)
                    {
                        for (int j = r + 1; j <= row; j++)
                        {
                            board.move_tile(j, column);
                            swap_controls(j, column, 1);
                        }
                    }
                    return;
                }
            }
        }

        private void swap_controls(int row, int col, int direction)
        {
            Control control1 = grid.GetControlFromPosition(col, row);
            Control control2;

            switch (direction)
            {
                case 1:
                    control2 = grid.GetControlFromPosition(col, row - 1);
                    grid.SetCellPosition(control1, new TableLayoutPanelCellPosition(col, row - 1));
                    grid.SetCellPosition(control2, new TableLayoutPanelCellPosition(col, row));
                    break;
                case -1:
                    control2 = grid.GetControlFromPosition(col, row + 1);
                    grid.SetCellPosition(control1, new TableLayoutPanelCellPosition(col, row + 1));
                    grid.SetCellPosition(control2, new TableLayoutPanelCellPosition(col, row));
                    break;
                case 2:
                    control2 = grid.GetControlFromPosition(col + 1, row);
                    grid.SetCellPosition(control1, new TableLayoutPanelCellPosition(col + 1, row));
                    grid.SetCellPosition(control2, new TableLayoutPanelCellPosition(col, row));
                    break;
                case -2:
                    control2 = grid.GetControlFromPosition(col - 1, row);
                    grid.SetCellPosition(control1, new TableLayoutPanelCellPosition(col - 1, row));
                    grid.SetCellPosition(control2, new TableLayoutPanelCellPosition(col, row));
                    break;
            }
        }
    }
}
