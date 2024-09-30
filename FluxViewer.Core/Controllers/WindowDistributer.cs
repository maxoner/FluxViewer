using System;
using System.Collections.Generic;

namespace FluxViewer.Core.Controllers
{
    internal static class WindowDistributer
    {

        /// <summary>
        /// Метод позволяет равномерно распределить окна
        /// в виде сетки использую 3 параметра
        /// 1. Ширина экрана
        /// 2. Высота экрана
        /// 3. Количество окон
        /// </summary>
        public static List<WindowInfo> DistributeWindows(int screenWidth, int screenHeight, int windowCount)
        {
            List<WindowInfo> windows = new List<WindowInfo>();

            // Определяем количество строк (стремимся к равному числу окон в строках)
            int rows = (int)Math.Ceiling(Math.Sqrt(windowCount));
            int totalWindows = windowCount;
            int yOffset = 0;

            // Высота каждой строки
            int rowHeight = screenHeight / rows;

            for (int row = 0; row < rows; row++)
            {
                // Количество окон в текущей строке
                int windowsInRow = totalWindows / (rows - row);
                totalWindows -= windowsInRow;

                // Ширина каждого окна в строке, чтобы покрыть всю ширину экрана
                int windowWidth = screenWidth / windowsInRow;

                for (int i = 0; i < windowsInRow; i++)
                {
                    int x = i * windowWidth;
                    int y = yOffset;

                    windows.Add(new WindowInfo(x, y, windowWidth, rowHeight));
                }

                yOffset += rowHeight;
            }

            return windows;
        }
    }


    /// <summary>
    /// Класс описывает информацию об окне канала
    /// 1. Координата верхнего левого угла
    /// 2. Ширина и Высота окна
    /// </summary>
    public class WindowInfo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public WindowInfo(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, Width: {Width}, Height: {Height}";
        }
    }
}
