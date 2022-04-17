using System;

namespace ChessViewer.UI.Static
{
    public static class ChessRender
    {
        public static string GetSquareColor(int x, int y) 
        {
            if ((x + y) % 2 == 0)
                return "#aaaaaa";
            return "#eeeeee";
        }
    }
}