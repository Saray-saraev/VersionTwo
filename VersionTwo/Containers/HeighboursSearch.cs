
using VersionTwo.Containers.Cells;
using VersionTwo.Containers.Continets;
using VersionTwo.Containers.TerrainSectors;


namespace VersionTwo.Containers
{
    internal class HeighboursSearch
    {
        public static Cell SearchCellSector(string Sector)
        {
            char[] temp = new char[Sector.Length-1];
            for(int i = 0; i<Sector.Length; i++)
            {
                if (Sector[i] != 'C' || Sector[i] != 'c' || Sector[i] != 'Я' || Sector[i] != 'я')
                {
                    temp[i] = Sector[i];

                }
                if (i == Sector.Length - 1)
                {
                    break;
                }
            }
            int index = int.Parse(temp.ToString());

            return null;
        }
        public static void HeighborsSearch(ContinentSector[,] Map)
        {
            int size = (int)Math.Sqrt(Map.Length);

            ///Пояснение: 0- верхний соседб 1- нижний, 2 - левый, 3 - правый ///
            //Левый верхний угол. 
            
            Map[0, 0].GetContinent().SetNeighbour(1, Map[1, 0]);
            Map[0, 0].GetContinent().SetNeighbour(3, Map[0, 1]);
            Map[0, 0].FlafsOfSide[0] = true;

            //Правый верхний угол
 
            Map[0, size-1].GetContinent().SetNeighbour(1, Map[1,size-1]);
            Map[0, size-1].GetContinent().SetNeighbour(2, Map[0,size-2]);
            Map[0, size - 1].FlafsOfSide[0] = true;
            //Левый нижний

            Map[size - 1, 0].GetContinent().SetNeighbour(0, Map[size-2, 0]);
            
            Map[size - 1, 0].GetContinent().SetNeighbour(3, Map[size-1, 1]);
            Map[size - 1, 0].FlafsOfSide[0] = true;

            //Правый нижний
            Map[size - 1, size - 1].GetContinent().SetNeighbour(0, Map[size - 2, size - 1]);
            Map[size - 1, size -1].GetContinent().SetNeighbour(2, Map[size - 1, size - 2]);
            Map[size - 1, size - 1].FlafsOfSide[0] = true;

            for (int i = 0; i < size; i++)
            {
                if(i==0)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (Map[i, j].FlafsOfSide[0] == false)
                        {
                            Map[i, j].GetContinent().SetNeighbour(1, Map[i+1,j]);
                            Map[i, j].GetContinent().SetNeighbour(2, Map[i, j-1]);
                            Map[i, j].GetContinent().SetNeighbour(3, Map[i, j+1]);
                            Map[i, j].FlafsOfSide[1] = true;
                        }
                    }
                }
                if (i==size-1)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (Map[i, j].FlafsOfSide[0] == false)
                        {
                            Map[i, j].GetContinent().SetNeighbour(0, Map[i - 1, j]);
                            Map[i, j].GetContinent().SetNeighbour(2, Map[i, j - 1]);
                            Map[i, j].GetContinent().SetNeighbour(3, Map[i, j + 1]);
                            Map[i, j].FlafsOfSide[1] = true;
                        }
                    }
                }
                if(i!=size-1 && i !=0) 
                {
                    Map[i, 0].GetContinent().SetNeighbour(0, Map[i - 1, 0]);
                    Map[i, 0].GetContinent().SetNeighbour(1, Map[i+1, 0]);
                    Map[i, 0].GetContinent().SetNeighbour(2, Map[i, 1]);
                    Map[i, 0].FlafsOfSide[1] = true;

                    Map[i, size-1].GetContinent().SetNeighbour(0, Map[i - 1, size - 1]);
                    Map[i, size-1].GetContinent().SetNeighbour(1, Map[i+1, size - 1]);
                    Map[i, size-1].GetContinent().SetNeighbour(3, Map[i, size-2]);
                    Map[i, size-1].FlafsOfSide[1] = true;
                    for (int j = 0; j < size; j++)
                    {
                        if (Map[i, j].FlafsOfSide[1]== false)
                        {
                            Map[i, j].GetContinent().SetNeighbour(0, Map[i - 1, j]);
                            Map[i, j].GetContinent().SetNeighbour(1, Map[i + 1, j]);
                            Map[i, j].GetContinent().SetNeighbour(2, Map[i, j - 1]);
                            Map[i, j].GetContinent().SetNeighbour(3, Map[i, j + 1]);
                            Map[i, j].FlafsOfSide[2] = true;
                        }
                       
                        
                    }
                }
                
            }
        }
        public static void HeighborsSearch(TerrainContainer[,] Map)
        {
            int size = (int)Math.Sqrt(Map.Length);

            ///Пояснение: 0- верхний соседб 1- нижний, 2 - левый, 3 - правый ///
            //Левый верхний угол. 

            Map[0, 0].GetTerrain().SetNeighbour(1, Map[1, 0]);
            Map[0, 0].GetTerrain().SetNeighbour(3, Map[0, 1]);
            Map[0, 0].FlagsOfSides[0] = true;

            //Правый верхний угол

            Map[0, size - 1].GetTerrain().SetNeighbour(1, Map[1, size - 1]);
            Map[0, size - 1].GetTerrain().SetNeighbour(2, Map[0, size - 2]);
            Map[0, size - 1].FlagsOfSides[0] = true;
            //Левый нижний

            Map[size - 1, 0].GetTerrain().SetNeighbour(0, Map[size - 2, 0]);

            Map[size - 1, 0].GetTerrain().SetNeighbour(3, Map[size - 1, 1]);
            Map[size - 1, 0].FlagsOfSides[0] = true;

            //Правый нижний
            Map[size - 1, size - 1].GetTerrain().SetNeighbour(0, Map[size - 2, size - 1]);
            Map[size - 1, size - 1].GetTerrain().SetNeighbour(2, Map[size - 1, size - 2]);
            Map[size - 1, size - 1].FlagsOfSides[0] = true;

            for (int i = 0; i < size; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (Map[i, j].FlagsOfSides[0] == false)
                        {
                            Map[i, j].GetTerrain().SetNeighbour(1, Map[i + 1, j]);
                            Map[i, j].GetTerrain().SetNeighbour(2, Map[i, j - 1]);
                            Map[i, j].GetTerrain().SetNeighbour(3, Map[i, j + 1]);
                            Map[i, j].FlagsOfSides[1] = true;
                        }
                    }
                }
                if (i == size - 1)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (Map[i, j].FlagsOfSides[0] == false)
                        {
                            Map[i, j].GetTerrain().SetNeighbour(0, Map[i - 1, j]);
                            Map[i, j].GetTerrain().SetNeighbour(2, Map[i, j - 1]);
                            Map[i, j].GetTerrain().SetNeighbour(3, Map[i, j + 1]);
                            Map[i, j].FlagsOfSides[1] = true;
                        }
                    }
                }
                if (i != size - 1 && i != 0)
                {
                    Map[i, 0].GetTerrain().SetNeighbour(0, Map[i - 1, 0]);
                    Map[i, 0].GetTerrain().SetNeighbour(1, Map[i + 1, 0]);
                    Map[i, 0].GetTerrain().SetNeighbour(2, Map[i, 1]);
                    Map[i, 0].FlagsOfSides[1] = true;

                    Map[i, size - 1].GetTerrain().SetNeighbour(0, Map[i - 1, size - 1]);
                    Map[i, size - 1].GetTerrain().SetNeighbour(1, Map[i + 1, size - 1]);
                    Map[i, size - 1].GetTerrain().SetNeighbour(3, Map[i, size - 2]);
                    Map[i, size - 1].FlagsOfSides[1] = true;
                    for (int j = 0; j < size; j++)
                    {
                        if (Map[i, j].FlagsOfSides[1] == false)
                        {
                            Map[i, j].GetTerrain().SetNeighbour(0, Map[i - 1, j]);
                            Map[i, j].GetTerrain().SetNeighbour(1, Map[i + 1, j]);
                            Map[i, j].GetTerrain().SetNeighbour(2, Map[i, j - 1]);
                            Map[i, j].GetTerrain().SetNeighbour(3, Map[i, j + 1]);
                            Map[i, j].FlagsOfSides[2] = true;
                        }


                    }
                }

            }
        }
        public static void HeighborsSearch(Cell_[,] Map)
        {
            int size = (int)Math.Sqrt(Map.Length);

            ///Пояснение: 0- верхний соседб 1- нижний, 2 - левый, 3 - правый ///
            //Левый верхний угол. 

            Map[0, 0].GetCell().SetNeighbour(1, Map[1, 0]);
            Map[0, 0].GetCell().SetNeighbour(3, Map[0, 1]);
            Map[0, 0].FlagsOfSides[0] = true;

            //Правый верхний угол

            Map[0, size - 1].GetCell().SetNeighbour(1, Map[1, size - 1]);
            Map[0, size - 1].GetCell().SetNeighbour(2, Map[0, size - 2]);
            Map[0, size - 1].FlagsOfSides[0] = true;
            //Левый нижний

            Map[size - 1, 0].GetCell().SetNeighbour(0, Map[size - 2, 0]);

            Map[size - 1, 0].GetCell().SetNeighbour(3, Map[size - 1, 1]);
            Map[size - 1, 0].FlagsOfSides[0] = true;

            //Правый нижний
            Map[size - 1, size - 1].GetCell().SetNeighbour(0, Map[size - 2, size - 1]);
            Map[size - 1, size - 1].GetCell().SetNeighbour(2, Map[size - 1, size - 2]);
            Map[size - 1, size - 1].FlagsOfSides[0] = true;

            for (int i = 0; i < size; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (Map[i, j].FlagsOfSides[0] == false)
                        {
                            Map[i, j].GetCell().SetNeighbour(1, Map[i + 1, j]);
                            Map[i, j].GetCell().SetNeighbour(2, Map[i, j - 1]);
                            Map[i, j].GetCell().SetNeighbour(3, Map[i, j + 1]);
                            Map[i, j].FlagsOfSides[1] = true;
                        }
                    }
                }
                if (i == size - 1)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (Map[i, j].FlagsOfSides[0] == false)
                        {
                            Map[i, j].GetCell().SetNeighbour(0, Map[i - 1, j]);
                            Map[i, j].GetCell().SetNeighbour(2, Map[i, j - 1]);
                            Map[i, j].GetCell().SetNeighbour(3, Map[i, j + 1]);
                            Map[i, j].FlagsOfSides[1] = true;
                        }
                    }
                }
                if (i != size - 1 && i != 0)
                {
                    Map[i, 0].GetCell().SetNeighbour(0, Map[i - 1, 0]);
                    Map[i, 0].GetCell().SetNeighbour(1, Map[i + 1, 0]);
                    Map[i, 0].GetCell().SetNeighbour(2, Map[i, 1]);
                    Map[i, 0].FlagsOfSides[1] = true;

                    Map[i, size - 1].GetCell().SetNeighbour(0, Map[i - 1, size - 1]);
                    Map[i, size - 1].GetCell().SetNeighbour(1, Map[i + 1, size - 1]);
                    Map[i, size - 1].GetCell().SetNeighbour(3, Map[i, size - 2]);
                    Map[i, size - 1].FlagsOfSides[1] = true;
                    for (int j = 0; j < size; j++)
                    {
                        if (Map[i, j].FlagsOfSides[1] == false)
                        {
                            Map[i, j].GetCell().SetNeighbour(0, Map[i - 1, j]);
                            Map[i, j].GetCell().SetNeighbour(1, Map[i + 1, j]);
                            Map[i, j].GetCell().SetNeighbour(2, Map[i, j - 1]);
                            Map[i, j].GetCell().SetNeighbour(3, Map[i, j + 1]);
                            Map[i, j].FlagsOfSides[2] = true;
                        }


                    }
                }

            }
        }

    }
}
