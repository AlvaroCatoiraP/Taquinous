

namespace Taquin
{
    /// <summary>
    /// Represents a tile in the Taquin game.
    /// </summary>
    public class Tile
    {
        private int index_Symbol;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> class.
        /// </summary>
        /// <param name="index_Symbol">The symbol associated with the tile.</param>
        public Tile(int index_Symbol)
        {
            this.index_Symbol = index_Symbol;
        }

        /// <summary>
        /// Gets the symbol of the tile.
        /// </summary>
        /// <returns>The symbol as an integer.</returns>
        public int Get_symbol_index()
        {
            return index_Symbol;
        }

        /// <summary>
        /// Returns a string representation of the tile.
        /// </summary>
        /// <returns>A string representing the symbol of the tile.</returns>
        public override string ToString()
        {
            return index_Symbol.ToString();
        }
    }
}
