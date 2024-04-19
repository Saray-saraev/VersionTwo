using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersionTwo.TerrainTools.terrainTypes
{
    internal class TerrainTypeCollection
    {
        static TerrainType[] Collection = new TerrainType[0];

        public static int GetRank()
        {
            return Collection.Length;
        }
        public static int AddNewTerrainTypeInArray(TerrainType type)
        {
            if (Collection is null)
            {
                Collection = new TerrainType[1];
                type.SetRank(0);
                Collection[0] = type;
                return 0;
            }
            else
            {
                int count = Collection.Length;
                TerrainType[] temp = new TerrainType[count + 1];
                for (int i = 0; i < count; i++)
                {
                    temp[i] = Collection[i];
                }
                type.SetRank(count);
                temp[count] = type;
                Collection = temp;
                return count;
            }
        }
        public static TerrainType GetTerrain(int Rank)
        {
            if(Rank<0)
            {
                return Collection[Collection.Length +Rank];
            }
            else
            {
                return Collection[Rank];
            }
            
        }
    }
}
