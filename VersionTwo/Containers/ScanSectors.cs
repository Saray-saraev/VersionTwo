using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using VersionTwo.Containers.Continets;

namespace VersionTwo.Containers
{
    internal class ScanSectors
    {
        public static bool ScanContinentSectorsGen(int lim, ContinentSector Point, int i = 0)
        {

            return _Scaning(lim, Point)[1,1];

        }

        static bool[,] _Scaning(int lim, ContinentSector Point)
        {
            ContinentSector[,] resultTable = new ContinentSector[lim * 2 + 1, lim * 2 + 1];
            resultTable[lim,lim] = Point;
            bool[] flag = new bool[2];
            
            
            for (int i = 1; i <= lim; i++)
            {
                
                if (!flag[0])
                {
                    _ScanHorizontalLine(i, resultTable, lim);
                    flag = _ScanVerticalLine(i, resultTable, lim);
                    
                }
                if (!flag[1])
                {
                    _ScanHorizontalLine(-i, resultTable, lim);
                    flag = _ScanVerticalLine(-i, resultTable, lim);
                    
                }
            }

            
            return new bool[2, 2];
        }




        static bool[] _ScanVerticalLine(int iteration, ContinentSector[,] resultTable, int lim)
        {
            bool[] tempResult = new bool[2];
            
            ContinentSector nextPoint;
            if (iteration < 0) {
                nextPoint = resultTable[lim, lim].GetContinent().GetNeighbour(0);
            }
            else
            {
                nextPoint = resultTable[lim, lim].GetContinent().GetNeighbour(1);
            }
            
            if (nextPoint.FlafsOfSide is not null)
            {
                
                resultTable[lim + iteration, lim] = nextPoint;

                for (int i = 1;i<=lim; i++)
                {
                    
                    if (!tempResult[1])
                    {

                        tempResult = _ScanHorizontalLine(i, resultTable, lim, lim + iteration);
                    }
                    if (!tempResult[0])
                    {
                        tempResult = _ScanHorizontalLine(-i, resultTable, lim, lim + iteration);
                    }
                }
                return tempResult;
            }
            else
            {
                if (iteration < 0)
                {
                    tempResult[1] = true;
                }
                
                else
                {
                    tempResult[0] = true;
                }
                return tempResult;
            }
                 
            

        }
        static bool[] _ScanHorizontalLine(int iteration, ContinentSector[,] resultTable, int lim, int verticalOrientation)
        {
            bool[] tempResult = new bool[2];
            ContinentSector nextPoint;
            if (iteration < 0)
            {
                nextPoint = resultTable[verticalOrientation, lim].GetContinent().GetNeighbour(3);
            }
            else
            {
                nextPoint = resultTable[verticalOrientation, lim].GetContinent().GetNeighbour(2);
            }

            if (nextPoint.FlafsOfSide is not null)
            {
                resultTable[verticalOrientation, lim + iteration] = nextPoint;
                return tempResult;
            }

            else
            {
                if (iteration <0)
                {
                    tempResult[0] = true;
                }
                else
                {
                    tempResult[1] = true;
                }
                
                return tempResult;
            }



        }
        static bool[] _ScanHorizontalLine(int iteration, ContinentSector[,] resultTable, int lim)
        {
            bool[] tempResult = new bool[2];
            ContinentSector nextPoint;
            if (iteration < 0)
            {
                nextPoint = resultTable[lim, lim].GetContinent().GetNeighbour(2);
            }
            else
            {
                nextPoint = resultTable[lim, lim].GetContinent().GetNeighbour(3);
            }

            if (nextPoint.FlafsOfSide is not null)
            {
                resultTable[lim, lim + iteration] = nextPoint;
                return tempResult;
            }

            else
            {
                if (iteration < 0)
                {
                    tempResult[0] = true;
                }
                else
                {
                    tempResult[1] = true;
                }

                return tempResult;
            }



        }



       
        
    }
    
    
}
