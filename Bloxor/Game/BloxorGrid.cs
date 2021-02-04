using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Bloxor.Glazor;


namespace Bloxor.Game
{
    public class BloxorGrid : Grid
    {
        const int RowCount = 10;
        const int ColumnCount = 10;

        public BloxorGrid(Rectangle bounds, string lineColor): 
            base(bounds, RowCount, ColumnCount, lineColor)
        {
        }

        public string[,] Cells { get; } = new string[RowCount, ColumnCount];


        private bool IsRowFull(int row) 
        {
            return Enumerable.Range(0, ColumnCount).All(col => Cells[row, col] != null);
        }

        private bool IsColumnFull(int col) 
        {
            return Enumerable.Range(0, RowCount).All(row => Cells[row, col] != null);
        }
        
        /// <summary>
        /// Clear completed rows and columns
        /// </summary>
        public void ClearComplete()
        {
            var completeRows = Enumerable.Range(0, RowCount).Where(IsRowFull).ToList();
            var completeColumns = Enumerable.Range(0, ColumnCount).Where(IsColumnFull).ToList();

            if (completeRows.Count > 0)
            {
                Logger.Log($"complete rows: {completeRows}");
            }

            if (completeColumns.Count > 0)
            {
                Logger.Log($"complete columns: {completeRows}");
            }
            
            completeRows.ForEach(row =>
            {
                for (var col = 0; col < ColumnCount; col++)
                {
                    Cells[row, col] = null;
                }
            });
            
            completeColumns.ForEach(col =>
            {
                for (var row = 0; row < RowCount; row++)
                {
                    Cells[row, col] = null;
                }
            });
        }
        
        
        public override async ValueTask Render(ICanvas canvas)
        {
            var cellWidth = Width / ColumnCount;
            var  cellHeight = Height / RowCount;
            await base.Render(canvas);
            for (var row = 0; row < RowCount; row++)
            {
                for (var col = 0; col < ColumnCount; col++)
                {
                    var color = Cells[row, col];
                    if (color == null)
                        continue;

                    var left = Left + col * cellWidth;
                    var top = Top + row * cellWidth;
                    await canvas.DrawRectangle(left, top, cellWidth, cellHeight, fillColor: Cells[row, col]);
                }
            }
        }
    }
}