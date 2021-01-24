using System;
using System.Drawing;
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