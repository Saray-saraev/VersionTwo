using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersionTwo.Containers.Cells;
using VersionTwo.Containers.Continets;
using VersionTwo.Containers.TerrainSectors;

namespace VersionTwo.Containers
{
    internal class SectorsCollection
    {
        static Continent[,] MapOfContinent;
        static TerrainSector[,] MapOfTerrain;
        static Cell[,] MapOfCell;

        public static ContinentSector[,] CreateContinentMap(int Size, float ChanceToGenerate, float islandFactor, float LandFactor)
        {
            ContinentSector[,] ContinentMap = new ContinentSector[Size,Size];
            MapOfContinent = new Continent[Size,Size];
            int k = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    MapOfContinent[i, j] = RegistryContinents.RegistryContinent(k, ChanceToGenerate, islandFactor, LandFactor);
                    ContinentMap[i, j] = new ContinentSector(i,j);
                    k++;
                }
            }

            return ContinentMap;
        }

        public static TerrainContainer[,] CreateTerrainMap(ContinentSector[,] ContinentMap)
        {
            int size = (int)Math.Sqrt(ContinentMap.Length)*5;
            TerrainContainer[,] TerrainMap = new TerrainContainer[size, size];
            MapOfTerrain = new TerrainSector[size, size];
            int k = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    MapOfTerrain[i, j] = RegistryTerrainSectors.RegistryTerrainSector(k);
                    TerrainMap[i, j] = new TerrainContainer(i,j);
                    k++;
                }
            }
            return TerrainMap;

        }

        public static Cell_[,] CreateCellMap(TerrainContainer[,] TerrainMap)
        {
            int size = (int)Math.Sqrt(TerrainMap.Length) * 3;
            Cell_[,] CellMap = new Cell_[size, size];
            MapOfCell = new Cell[size, size];
            int k = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    MapOfCell[i, j] = RegistryCells.RegistryCellSector(k);
                    CellMap[i, j] = new Cell_(i, j);
                    k++;
                }
            }
            return CellMap;

        }
        public static Continent GetContinent(int[] GlobalId)
        {
     
                return MapOfContinent[GlobalId[0], GlobalId[1]];
        }
        public static TerrainSector GetTerrain(int[] GlobalId)
        {
            return MapOfTerrain[GlobalId[0], GlobalId[1]];
        }
        public static Cell GetCell(int[] GlobalId)
        {
            return MapOfCell[GlobalId[0], GlobalId[1]];
        }

    }
}
