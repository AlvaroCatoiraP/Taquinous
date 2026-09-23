using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taquin
{
    /// <summary>
    /// Represents a cell on the Taquin game board.
    /// </summary>
    public class Cell : Button
    {
        private Tile tile;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        public Cell()
        {
        }

        /// <summary>
        /// Checks if the cell is occupied by a tile.
        /// </summary>
        /// <returns>True if the cell contains a tile, otherwise false.</returns>
        public bool Is_occupied()
        {
            return tile != null;
        }

        /// <summary>
        /// Gets the tile contained in the cell.
        /// </summary>
        /// <returns>The tile present in the cell.</returns>
        public Tile Get_tile()
        {
            return tile;
        }

        /// <summary>
        /// Sets a tile in the cell.
        /// </summary>
        /// <param name="tile">The tile to place in the cell.</param>
        public void Set_Tile(Tile tile)
        {
            this.tile = tile;
        }

        /// <summary>
        /// Frees the cell by removing the tile it contains.
        /// </summary>
        public void Free_Cell()
        {
            this.tile = null;
        }

        /// <summary>
        /// Returns a string representation of the cell.
        /// </summary>
        /// <returns>A string representing the cell.</returns>
        public override string ToString()
        {
            return tile.ToString();
        }
    }
}
