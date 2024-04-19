using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersionTwo.Containers.Continets
{
    internal struct ContinentSector(int i, int j)
    {
        int[] globalId = {i,j};

        public bool[] FlafsOfSide = new bool[3];
        public Continent GetContinent()
        {
            return SectorsCollection.GetContinent(globalId);
        }
        public int[] GetGlobalId()
        { 
            return globalId;
        }
    }
    internal class RegistryContinents
    {
        public static Continent RegistryContinent(int iteration, float chanceToLand, float IslandFactor, float landFactor)
        {
           TypeContinent continentSettings = new TypeContinent(true, false, landFactor,IslandFactor);
           
           Continent continent = new Continent(iteration, continentSettings);

           return continent;
        }
    }
}
