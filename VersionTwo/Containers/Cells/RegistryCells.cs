using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersionTwo.Containers.TerrainSectors;

namespace VersionTwo.Containers.Cells
{
    internal struct Cell_(int i, int j)
    {
        int[] GlobalId = {i,j};
        public bool[] FlagsOfSides = {false, false,false };

        public Cell GetCell()
        {
            return SectorsCollection.GetCell(GlobalId);
        }
        
    }
    class RegistryCells
    {
        public static Cell RegistryCellSector(int ID)
        {
            Cell sector = new Cell(ID);
            return sector;
        }
    }
}
