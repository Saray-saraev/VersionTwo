using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using System.Runtime.ConstrainedExecution;
using VersionTwo.Containers.Cells;
using VersionTwo.Containers.TerrainSectors;

namespace VersionTwo.DrawingTools.BMPGenerator
{
    internal class DrawingMap
    {

        public static void DrawTerrainMap(TerrainContainer[,] TerrainMap, int format)
        {
            int size = (int)Math.Sqrt(TerrainMap.Length);

             int SquartSizeTerrain = 30;
            
            Bitmap map = new Bitmap(size * SquartSizeTerrain, size * SquartSizeTerrain);
            Graphics graphics = Graphics.FromImage(map);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    SolidBrush tempPen = TerrainMap[i, j].GetTerrain().GetTerrain().GetTerrain().GetBrush();
                    graphics.FillRectangle(tempPen, j * SquartSizeTerrain, i * SquartSizeTerrain, SquartSizeTerrain, SquartSizeTerrain);

                }
            }
            switch (format)
            {
                case 0:
                    map.Save("C:/Users/User/Desktop/проект/TerrainMap.png", ImageFormat.Png);
                    break;
                case 1:
                    map.Save("C:/Users/User/Desktop/проект/TerrainMap.jpeg", ImageFormat.Jpeg);
                    break;
                case 2:
                    map.Save("C:/Users/User/Desktop/проект/TerrainMap.bmp", ImageFormat.Bmp);
                    break;

            }

           
        }

        public static void DrawHeightmap(Cell_[,] Heightmap, int format)
        {
            int size = (int)Math.Sqrt(Heightmap.Length);
            int SquartSizeCell = 10;

            Bitmap map = new Bitmap(size * SquartSizeCell, size * SquartSizeCell);
            Graphics graphics = Graphics.FromImage(map);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    SolidBrush tempPen = GetHeightmapColor(Heightmap[i, j].GetCell().GetHeight());
                    graphics.FillRectangle(tempPen, j * SquartSizeCell, i * SquartSizeCell, SquartSizeCell, SquartSizeCell);
                }
            }


            switch (format)
            {
                case 0:
                    map.Save("C:/Users/User/Desktop/проект/HeightMap.png", ImageFormat.Png);
                    break;
                case 1:
                    map.Save("C:/Users/User/Desktop/проект/HeightMap.jpeg", ImageFormat.Jpeg);
                    break;
                case 2:
                    map.Save("C:/Users/User/Desktop/проект/HeightMap.bmp", ImageFormat.Bmp);
                    break;

            }

        }
        static SolidBrush GetHeightmapColor(float height)
        {
            int clr = (int)(height * 70f + 70f);
            return new SolidBrush(Color.FromArgb(255, clr, clr, clr));
        }

    }
   
}
