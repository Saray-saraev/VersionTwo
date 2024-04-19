
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Reflection;
using System.Security.Cryptography;
using VersionTwo.Containers;
using VersionTwo.Containers.Cells;
using VersionTwo.Containers.Continets;
using VersionTwo.Containers.TerrainSectors;
using VersionTwo.TerrainTools.terrainTypes;

namespace VersionTwo.generator.ContinentGeneration
{
    internal class basicAlgorithm
    {
        public static ContinentSector[,] StartContinentGeneration(int size,float ChanceToGenerate, float islandFactor, float LandFactor)
        {
            return SectorsCollection.CreateContinentMap(size,ChanceToGenerate, islandFactor, LandFactor);

        }
        public static TerrainContainer[,] StartTerrainCell(ContinentSector[,] Map)
        {
            return SectorsCollection.CreateTerrainMap(Map);
        }
        public static Cell_[,] StartCell(TerrainContainer[,] Map)
        {
            return SectorsCollection.CreateCellMap(Map);
        }

        public static Highpoints GenerationHighPoint(ContinentSector[,] Map,float factor, int radius = 4)
        {

       
            int size = (int)Math.Sqrt(Map.Length);
            int LandCount = (int)(Math.Pow(size,2) * factor);
            int HighPointCount = LandCount / 5;
            int count = 0;
            Highpoints result = new Highpoints(HighPointCount);
            while(count != HighPointCount)
            {
                for (int i = 0; i < size; i++)
                {
                    if (count == HighPointCount) break;
                    for (int j = 0; j < size; j++)
                    {
                        if ((float)((new Random()).NextDouble()) <= 0.1)
                        {
                            Map[i, j].GetContinent().SetNewSettings(false);
                            Continent test = Map[i, j].GetContinent();
                            result.Map[count] = Map[i, j];
                            count++;
                        }
                        if (count == HighPointCount) break;
                    }
                    
                }
            }
            
            _debug(Map, size);
            count = generatePlace(Map, result, LandCount, count);
            while(count < LandCount)
            {
                count = EndGeneratePlace(Map, LandCount, count);
            }
            
                return result;


        }
        static int generatePlace(ContinentSector[,] Map, Highpoints points, int LandCount, int count)
        {
            int size = (int)Math.Sqrt(Map.Length);
            for (int i = 0;i < points.Map.Length;i++)
            {
                ContinentSector[] neighbors = new ContinentSector[4];
                for (int j=0; j<4; j++)
                {
                    neighbors[j] = points.Map[i].GetContinent().GetNeighbour(j);
                    if (neighbors[j].FlafsOfSide is not null)
                    {
                        if (LandCount != count && neighbors[j].GetContinent().GetContinentSettings().IsLand() != true)
                        {
                            int[] id = neighbors[j].GetGlobalId();
                            
                            Map[id[0], id[1]].GetContinent().SetNewSettings(false);
                            count++;
                        }
                        else
                        {
                            break;
                        }
                        
                    }
                    
                }
                if(LandCount == count) { break; }
            }
            _debug(Map, size);

            return count;
        }
        static int EndGeneratePlace(ContinentSector[,] Map, int landCount, int Count)
        {
            int size = (int)Math.Sqrt(Map.Length);
            int neighborCounter = 0; 
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (Map[i, j].GetContinent().GetContinentSettings().IsLand() == false)
                    {
                        if (landCount != Count)
                        {
                            ContinentSector[] tempElements = new ContinentSector[4];
                            for (int g = 0; g < 4; g++)
                            {
                                tempElements[g] = Map[i, j].GetContinent().GetNeighbour(g);
                                if (tempElements[g].FlafsOfSide is not null)
                                {
                                    if (tempElements[g].GetContinent().GetContinentSettings().IsLand())
                                    {
                                        neighborCounter++;
                                    }
                                }

                            }

                            if ((float)((new Random()).NextDouble()) * neighborCounter * 0.5f < 0.08 || neighborCounter == 3 )
                            {
                                Map[i, j].GetContinent().SetNewSettings(false);
                                Count++;
                            }
                            
                        }
                        else
                        {
                            break;
                        }
                    }
                        
                }
                if (landCount == Count) { break; }
            }
            
            
           _debug(Map, size);
            return Count;
        }

        public static void TransformContinentIntoTerrain(TerrainContainer[,] Terrain, ContinentSector[,] Continents)
        {
            int continentSize = (int)Math.Sqrt(Continents.Length);
            
            for (int i = 0;i< continentSize*5; i++)
            {
                for (int j = 0; j < continentSize * 5; j++)
                {
                    if (Continents[(int)(i/5),(int)(j/5)].GetContinent().GetContinentSettings().IsLand())
                    {
                        Terrain[i, j].GetTerrain().SetTerrain(-3);
                    }
                    else
                    {
                        Terrain[i, j].GetTerrain().SetTerrain(-1);
                    }
                }
            }
            HeighboursSearch.HeighborsSearch(Terrain);
            _debug(Terrain, continentSize * 5);
           
        }
        public static void TransformTerrainIntoHeigtmap(TerrainContainer[,] Terrain, Cell_[,] Heightmap)
        {
            int continentSize = (int)Math.Sqrt(Terrain.Length);

            for (int i = 0; i < continentSize * 3; i++)
            {
                for (int j = 0; j < continentSize * 3; j++)
                {
                    Heightmap[i,j].GetCell().SetHeight(Terrain[(int)(i / 3), (int)(j / 3)].GetTerrain().GetTerrain().GetTerrain().GetHeight().maximumHeight);

                }
            }

            HeighboursSearch.HeighborsSearch(Heightmap);
            

        }
        public static void GenerationRandomeHeightmap(TerrainContainer[,] Terrain, Cell_[,] Heightmap)
        {

            int continentSize = (int)Math.Sqrt(Terrain.Length);

            for (int i = 0; i < continentSize * 3; i++)
            {
                for (int j = 0; j < continentSize * 3; j++)
                {
                    bool stopFlag = false;
                    for (int k = 0; k < 3; k++)
                    {
                        float random = (float)(new Random()).NextDouble();
                        if (Terrain[(int)(i / 3), (int)(j / 3)].GetTerrain().GetTerrain().rank ==-1)
                        {
                            Heightmap[i, j].GetCell().SetHeight(-0.6f);
                            stopFlag = true;
                        }
                        else if (Terrain[(int)(i / 3), (int)(j / 3)].GetTerrain().GetTerrain().rank < 0
                            && (Terrain[(int)(i / 3), (int)(j / 3)].GetTerrain().GetTerrain().GetTerrain().GetHeight().mininalHeight < -random
                            && Terrain[(int)(i / 3), (int)(j / 3)].GetTerrain().GetTerrain().GetTerrain().GetHeight().maximumHeight >= -random))
                        {
                            Heightmap[i, j].GetCell().SetHeight(random);
                            stopFlag = true;
                        }
                        else if (Terrain[(int)(i / 3), (int)(j / 3)].GetTerrain().GetTerrain().rank >= 0
                            && (Terrain[(int)(i / 3), (int)(j / 3)].GetTerrain().GetTerrain().GetTerrain().GetHeight().mininalHeight < random
                            && Terrain[(int)(i / 3), (int)(j / 3)].GetTerrain().GetTerrain().GetTerrain().GetHeight().maximumHeight >= random))
                        {
                            Heightmap[i, j].GetCell().SetHeight(random);
                            stopFlag = true;
                        }
                        if (stopFlag)
                        {
                            break;
                        }
                    }
                   
                }
            }
            GetSmoothBorder(Heightmap);
            GetSmoothBorder(Heightmap);

        }
        static  void GetSmoothBorder(Cell_[,] Heightmap)
        {
            int size = (int)Math.Sqrt(Heightmap.Length);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Cell_[] temp = { Heightmap[i, j].GetCell().GetNeighbour(0), Heightmap[i, j].GetCell().GetNeighbour(1), Heightmap[i, j].GetCell().GetNeighbour(2), Heightmap[i, j].GetCell().GetNeighbour(3) };
                    int counter = 0;
                    float[] temp2 = new float[4];
                    for (int k = 0; k < 4; k++)
                    {
                        if (temp[k].FlagsOfSides is not null)
                        {
                            temp2[k] = temp[k].GetCell().GetHeight();
                            counter++;
                        }
                    }
                    Heightmap[i, j].GetCell().SetHeight((temp2[0] + temp2[1] + temp2[2] + temp2[3]+ Heightmap[i, j].GetCell().GetHeight())/(counter+1));
                }
            }
        }

        public static TerrainContainer[,] TransformCellIntoTerritory(TerrainContainer[,] Terrain)
        {
            int Size = (int)Math.Sqrt(Terrain.Length);
            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if (Terrain[i, j].GetTerrain().GetTerrain().IsLand())
                        {
                            int count = 0;
                            TerrainContainer[] temp = { Terrain[i, j].GetTerrain().GetNeighbour(0), Terrain[i, j].GetTerrain().GetNeighbour(1), Terrain[i, j].GetTerrain().GetNeighbour(2), Terrain[i, j].GetTerrain().GetNeighbour(3) };
                            for (int g = 0; g < 4; g++)
                            {
                                if (temp[g].FlagsOfSides is not null && !temp[g].GetTerrain().GetTerrain().IsLand())
                                {
                                    count++;
                                }
                            }

                            if (count == 3)
                            {
                                Terrain[i, j].GetTerrain().SetTerrain(-1);

                            }
                            else if (count == 2 && (new Random()).NextDouble() < 0.5)
                            {
                                Terrain[i, j].GetTerrain().SetTerrain(-1);
                            }
                            else if (count == 1 && (new Random()).NextDouble() < 0.25)
                            {
                                Terrain[i, j].GetTerrain().SetTerrain(-1);
                            }
                        }
                    }
                }
            }
            
           _debug(Terrain, Size);
            SetBankOnTerritory(Terrain);
            return Terrain;
        }

        static void SetBankOnTerritory(TerrainContainer[,] Terrain)
        {
            int Size = (int)Math.Sqrt(Terrain.Length);

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Terrain[i, j].GetTerrain().GetTerrain().IsLand())
                    {
                        int count = 0;
                        TerrainContainer[] temp = { Terrain[i, j].GetTerrain().GetNeighbour(0), Terrain[i, j].GetTerrain().GetNeighbour(1), Terrain[i, j].GetTerrain().GetNeighbour(2), Terrain[i, j].GetTerrain().GetNeighbour(3) };
                        for (int g = 0; g < 4; g++)
                        {
                            if (temp[g].FlagsOfSides is not null && !temp[g].GetTerrain().GetTerrain().IsLand())
                            {
                                count++;
                            }
                        }
                        if (count >0)
                        {
                            Terrain[i, j].GetTerrain().SetTerrain(-2);
                        }
                    }
                }
            }
            _debug(Terrain, Size);
        }

        public static void SetHighPoint(TerrainContainer[,] Terrain, Highpoints highpoints)
        {
            int Size = (int)Math.Sqrt(Terrain.Length);
            

            for (int i = 0; i < highpoints.Map.Length; i++)
            {
                int pointCount = 0;
                while(pointCount != 3)
                {
                    for (int j = highpoints.Map[i].GetGlobalId()[0] * 5; j < highpoints.Map[i].GetGlobalId()[0] * 5 + 5; j++)
                    {
                        for (int k = highpoints.Map[i].GetGlobalId()[1] * 5; k < highpoints.Map[i].GetGlobalId()[1] * 5 + 5; k++)
                        {
                            if (Terrain[j, k].GetTerrain().GetTerrain().IsLand() && (new Random()).NextDouble() < 0.25 && Terrain[j, k].GetTerrain().GetTerrain().rank != -2 && pointCount != 3)
                            {
                                Terrain[j, k].GetTerrain().SetTerrain(0);
                                pointCount++;
                            }
                            else if (pointCount == 3) { break; };
                        }
                        if (pointCount == 3) { break; };

                    }
                }
                
            }
            _debug(Terrain, Size);
        }

        public static void FormMountianMassive(TerrainContainer[,] Terrain)
        {
            int Size = (int)Math.Sqrt(Terrain.Length);

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    TerrainContainer[] neighbor = { Terrain[i,j].GetTerrain().GetNeighbour(0), Terrain[i, j].GetTerrain().GetNeighbour(1), Terrain[i, j].GetTerrain().GetNeighbour(2), Terrain[i, j].GetTerrain().GetNeighbour(3) };
                    int counter = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        if (neighbor[k].FlagsOfSides is not null)
                        {
                            if (neighbor[k].GetTerrain().GetTerrain().rank == 0)
                            {
                                counter++;
                            }
                        }
                    }
                    if (Terrain[i,j].GetTerrain().GetTerrain().rank != -2 && counter >1)
                    {
                        Terrain[i, j].GetTerrain().SetTerrain(0);
                    }
                }
            }
           _debug(Terrain, Size);

        }

        public static void FormFootMontianMassive(TerrainContainer[,] Terrain)
        {
            int Size = (int)Math.Sqrt(Terrain.Length);
            

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    int counter = 0;
                    TerrainContainer[] neighbor = { Terrain[i, j].GetTerrain().GetNeighbour(0), Terrain[i, j].GetTerrain().GetNeighbour(1), Terrain[i, j].GetTerrain().GetNeighbour(2), Terrain[i, j].GetTerrain().GetNeighbour(3) };

                    for (int k = 0; k <4; k++)
                    {
                        if (neighbor[k].FlagsOfSides is not null)
                        {
                            if (neighbor[k].GetTerrain().GetTerrain().rank == 0 && neighbor[k].GetTerrain().GetTerrain().rank != 1)
                            {
                                counter++;
                            }
                        }

                    }
                    if (Terrain[i,j].GetTerrain().GetTerrain().rank != 0 && Terrain[i, j].GetTerrain().GetTerrain().rank != -2 && Terrain[i, j].GetTerrain().GetTerrain().IsLand() && counter >0)
                    {

                        Terrain[i, j].GetTerrain().SetTerrain(1);
                    }
                   
                }
            }
            formPlatoStepOne(Size, Terrain);
            formPlatoStepTwo(Size, Terrain);
            formPlatoStepTrhee(Size, Terrain);
           _debug(Terrain, Size);
        }

        static void formPlatoStepOne(int Size, TerrainContainer[,] Terrain)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    int counter = 0;
                    TerrainContainer[] neighbor = { Terrain[i, j].GetTerrain().GetNeighbour(0), Terrain[i, j].GetTerrain().GetNeighbour(1), Terrain[i, j].GetTerrain().GetNeighbour(2), Terrain[i, j].GetTerrain().GetNeighbour(3) };

                    for (int k = 0; k < 4; k++)
                    {
                        if (neighbor[k].FlagsOfSides is not null)
                        {
                            if (neighbor[k].GetTerrain().GetTerrain().rank == -2)
                            {
                                counter++;
                            }
                        }


                    }
                    if (Terrain[i, j].GetTerrain().GetTerrain().rank == 0 && counter > 0)
                    {
                        Terrain[i, j].GetTerrain().SetTerrain(1);
                    }
                }
            }
        }

        static void formPlatoStepTrhee(int Size, TerrainContainer[,] Terrain)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    int counter = 0;
                    int counterMountian = 0;
                    TerrainContainer[] neighbor = { Terrain[i, j].GetTerrain().GetNeighbour(0), Terrain[i, j].GetTerrain().GetNeighbour(1), Terrain[i, j].GetTerrain().GetNeighbour(2), Terrain[i, j].GetTerrain().GetNeighbour(3) };

                    for (int k = 0; k < 4; k++)
                    {
                        if (neighbor[k].FlagsOfSides is not null)
                        {
                            if (neighbor[k].GetTerrain().GetTerrain().rank == 2)
                            {
                                counter++;
                            }
                            else if (neighbor[k].GetTerrain().GetTerrain().rank == 0)
                            {
                                counterMountian++;
                            }
                        }


                    }
                    if (Terrain[i, j].GetTerrain().GetTerrain().rank == 1 && counter > 0 && counterMountian == 0)
                    {
                        Terrain[i, j].GetTerrain().SetTerrain(2);
                    }
                }
            }
        }
        static void formPlatoStepTwo(int Size, TerrainContainer[,] Terrain)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    int counterFOOT = 0;
                    int counter = 0;
                    TerrainContainer[] neighbor = { Terrain[i, j].GetTerrain().GetNeighbour(0), Terrain[i, j].GetTerrain().GetNeighbour(1), Terrain[i, j].GetTerrain().GetNeighbour(2), Terrain[i, j].GetTerrain().GetNeighbour(3) };

                    for (int k = 0; k < 4; k++)
                    {
                        if (neighbor[k].FlagsOfSides is not null)
                        {
                            if (neighbor[k].GetTerrain().GetTerrain().rank == 0)
                            {
                                counter++;
                            }
                            else if(neighbor[k].GetTerrain().GetTerrain().rank == 1)
                            {
                                counterFOOT++;
                            }
                        }


                    }
                    if (Terrain[i, j].GetTerrain().GetTerrain().rank == 1 && counter ==0 && counterFOOT >1)
                    {
                        Terrain[i, j].GetTerrain().SetTerrain(2);
                    }
                }
            }
        }

        public static void FormLowTerrainForFirstTemplate(TerrainContainer[,] Terrain, int terrainRank, bool altFlag = false)
        {
            int Size = (int)Math.Sqrt(Terrain.Length);

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    int counter = 0;
                    TerrainContainer[] neighbor = { Terrain[i, j].GetTerrain().GetNeighbour(0), Terrain[i, j].GetTerrain().GetNeighbour(1), Terrain[i, j].GetTerrain().GetNeighbour(2), Terrain[i, j].GetTerrain().GetNeighbour(3) };

                    for (int k = 0; k < 4; k++)
                    {
                        if (neighbor[k].FlagsOfSides is not null)
                        {
                            if (neighbor[k].GetTerrain().GetTerrain().rank == terrainRank-1 && !altFlag)
                            {
                                counter++;
                            }
                            if ((neighbor[k].GetTerrain().GetTerrain().rank == terrainRank - 1|| neighbor[k].GetTerrain().GetTerrain().rank == terrainRank - 2) && altFlag)
                            {
                                counter++;
                            }

                        }
                    }
                    if (Terrain[i,j].GetTerrain().GetTerrain().rank == -3 && counter>0  )
                    {
                        Terrain[i, j].GetTerrain().SetTerrain(terrainRank);
                    }
                    
                    
                }
            }
            _debug(Terrain, Size);
        }
        public static void FormLowTerrainForSecondTemplate(TerrainContainer[,] Terrain, int TerrainRank, float TerrainRankFactor = 0.4f, int defaultType = -3)
        {
            int Size = (int)Math.Sqrt(Terrain.Length);
            int landCounter = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {

                    if (Terrain[i,j].GetTerrain().GetTerrain().rank == defaultType)
                    {
                        landCounter++;
                    }
                }
            }
            int countNewTerrain = (int)(landCounter * TerrainRankFactor);
            int counterTerrain = 0;
            int countFirstPoint = countNewTerrain / 10;
            float chance = TerrainTypeCollection.GetTerrain(TerrainRank).GetChance();
            while (counterTerrain != countFirstPoint)
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        int counter = 0;
                        TerrainContainer[] neighbor = { Terrain[i, j].GetTerrain().GetNeighbour(0), Terrain[i, j].GetTerrain().GetNeighbour(1), Terrain[i, j].GetTerrain().GetNeighbour(2), Terrain[i, j].GetTerrain().GetNeighbour(3) };

                        for (int k = 0; k < 4; k++)
                        {
                            if (neighbor[k].FlagsOfSides is not null)
                            {
                                if (neighbor[k].GetTerrain().GetTerrain().rank == TerrainRank)
                                {
                                    counter++;
                                }


                            }
                        }
                        
                        if (Terrain[i,j].GetTerrain().GetTerrain().rank == defaultType && ((float)(new Random()).NextDouble() < chance))
                        {
                            Terrain[i, j].GetTerrain().SetTerrain(TerrainRank);
                            
                            counterTerrain++;
                        }
                        if (counterTerrain == countFirstPoint)
                        {
                            break;
                        }
                    }
                    if (counterTerrain == countFirstPoint)
                    {
                        break;
                    }
                }
                Console.WriteLine(counterTerrain);
            }
            _debug(Terrain, Size);
            while (counterTerrain != countNewTerrain)
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        int counter = 0;
                        TerrainContainer[] neighbor = { Terrain[i, j].GetTerrain().GetNeighbour(0), Terrain[i, j].GetTerrain().GetNeighbour(1), Terrain[i, j].GetTerrain().GetNeighbour(2), Terrain[i, j].GetTerrain().GetNeighbour(3) };

                        for (int k = 0; k < 4; k++)
                        {
                            if (neighbor[k].FlagsOfSides is not null)
                            {
                                if (neighbor[k].GetTerrain().GetTerrain().rank == TerrainRank)
                                {
                                    counter++;
                                }
                               

                            }
                        }

                        if (Terrain[i, j].GetTerrain().GetTerrain().rank == defaultType && (float)(new Random()).NextDouble() < chance && counter>0)
                        {
                            Terrain[i, j].GetTerrain().SetTerrain(TerrainRank);
                            counterTerrain++;
                        }
                        if (counterTerrain == countNewTerrain)
                        {
                            break;
                        }
                        
                    }
                    if (counterTerrain == countNewTerrain)
                    {
                        break;
                    }
                }
                //Console.WriteLine(counterTerrain + "" + counter);

            }
            _debug(Terrain, Size);
        }
        static void _debug(ContinentSector[,] map, int lim)
        {
            for (int i = 0; i < lim; i++)
            {
                for (int j = 0; j < lim; j++)
                {
                    if (map[i, j].GetContinent().GetContinentSettings().IsLand())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"[h{i},{j}]");

                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"[n{i}:{j}]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void _debug(TerrainContainer[,] map, int lim)
        {
            for (int i = 0; i < lim; i++)
            {
                for (int j = 0; j < lim; j++)
                {

                    if (map[i, j].GetTerrain().GetTerrain().rank == -2)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"[B]");
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else if (map[i, j].GetTerrain().GetTerrain().rank == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write($"[M]");

                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else if (map[i, j].GetTerrain().GetTerrain().rank == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write($"[F]");

                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else if (map[i, j].GetTerrain().GetTerrain().rank == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"[P]");

                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else if (map[i, j].GetTerrain().GetTerrain().rank == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write($"[H]");

                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else if (map[i, j].GetTerrain().GetTerrain().rank == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"[D]");

                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else if (map[i, j].GetTerrain().GetTerrain().IsLand())
                    { 
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"[L]");

                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"[O]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

}
