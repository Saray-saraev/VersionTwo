using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersionTwo.Containers.Cells;

namespace VersionTwo.Containers.TerrainSectors
{
    internal struct TerrainContainer(int i, int j)
    {
        int[] globalId = { i, j };
        Cell_[,] CellCollection;
        public bool[] FlagsOfSides = { false, false, false };
        public TerrainSector GetTerrain()
        {
            return SectorsCollection.GetTerrain(globalId);
        }
    }
    internal class RegistryTerrainSectors
    {
        public static TerrainSector RegistryTerrainSector(int ID) 
        {
            TerrainSector sector = new TerrainSector(ID);
            return sector;
        }
    }
}
