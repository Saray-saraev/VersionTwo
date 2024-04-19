using System.Drawing;

namespace VersionTwo.DrawingTools

{
    public struct DrawTool()
    {
        //Координаты. 0 - pen, 1 - brush
        public int[] IndexInCollection = new int[2];
        //Запрашивают из общего хранилища инструменты
        public Pen GetPen()
        {
            return PenCollection.GetPen(IndexInCollection[0]);
        }
        public SolidBrush GetBrush()
        {
            return PenCollection.GetBrush(IndexInCollection[1]);
        }

    }
    internal class RegistryDrawTools
    {
        //Структура для инструмента
        
        //Регистрирует новый инструмент по кодировке ARGB
        public static DrawTool RegistryTools(bool isOnlyBrush/*Создавать только Brush*/, int Alpha, int Red, int Green, int Blue, int width = 0)
        {
            DrawTool tool = new DrawTool();
            if (isOnlyBrush == false) {
                //Создает Pen в хранилище и возвращает индекс
                tool.IndexInCollection[0] = PenCollection.CreateNewTools(true, Color.FromArgb(Alpha, Red, Green, Blue), width);
                tool.IndexInCollection[1] = PenCollection.CreateNewTools(false, Color.FromArgb(Alpha, Red, Green, Blue));
                return tool;
            }
            else
            {
                tool.IndexInCollection[1] = PenCollection.CreateNewTools(false, Color.FromArgb(Alpha, Red, Green, Blue));
                return tool;
            }
            
        }


    }
}
