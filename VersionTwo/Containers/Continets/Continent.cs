using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersionTwo.Containers.Continets
{
    public struct TypeContinent(bool isOcean, bool isIsland, float LandFactor,float IslandFactor )
    {
        public bool IsOcean = isOcean;
        public bool IsIsland = isIsland;
        public float islandFactor = IslandFactor;
        public float landFactor = LandFactor;
        
        public bool IsLand()
        {
            if (IsOcean)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    internal class Continent : BasicContainer<ContinentSector>
    {
        TypeContinent Type;

        public void SetNewSettings(bool NewOceanFactor, bool NewIslandFactor = false)
        {
            Type.IsOcean = NewOceanFactor;
            Type.IsIsland = NewIslandFactor;
        }
        public Continent(int ID, TypeContinent type) 
        {
            id = ID;
            Type = type;   
        }
        public TypeContinent GetContinentSettings()
        {
            return Type;
        }
        
    }
}
