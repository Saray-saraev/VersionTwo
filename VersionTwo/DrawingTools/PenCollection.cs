using System.Drawing;


namespace VersionTwo.DrawingTools
{
    internal class PenCollection
    {
        static Pen[] PensCollection;
        static SolidBrush[] BrushsCollection;
        
        static int AddNewPenInArray(Pen pen)
        {
            if(PensCollection is null)
            {
                PensCollection = new Pen[1];
                PensCollection[0] = pen;
                return 0;
            }
            else
            {
                int count = PensCollection.Length;
                Pen[] temp = new Pen[count+1];
                for (int i = 0; i < count; i++)
                {
                    temp[i] = PensCollection[i];
                }
                temp[count] = pen;
                PensCollection = temp;
                return count;
            }
        }

        static int AddNewBrushInArray(SolidBrush brush)
        {
            if (BrushsCollection is null)
            {
                BrushsCollection = new SolidBrush[1];
                
                BrushsCollection[0] = brush;
                return 0;
            }
            else
            {
                int count = BrushsCollection.Length;
                SolidBrush[] temp = new SolidBrush[count + 1];
                for (int i = 0; i < count; i++)
                {
                    temp[i] = BrushsCollection[i];
                }
                temp[count] = brush;
                BrushsCollection = temp;
                return count;
                
                
                
            }
        }
        public static int CreateNewTools(bool IsPen,Color color, int Width = 1)
        {
            if (IsPen == true)
            {
                return AddNewPenInArray(new Pen(color, Width));
            }
            else
            {
                return AddNewBrushInArray(new SolidBrush(color));
            }
        }
        public  static Pen GetPen(int index)
        { return PensCollection[index]; }
        public static SolidBrush GetBrush(int index)
        {  return BrushsCollection[index]; }
    }
}
