using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersionTwo.TerrainTools.terrainTypes;

namespace VersionTwo.TerrainTools.TerraiGeneration
{
    internal class TerrainCollection
    {
        public static Terrain Mountian;
        public static Terrain FootMountian;
        public static Terrain Plato;
        public static Terrain Hills;
        public static Terrain Plain;
        public static Terrain LowPlain;
        public static Terrain Bank;
        public static Terrain Ocean;

        public TerrainCollection()
        {
             Mountian = RegistryTerrainTypes.RegistryTerrainType(0.7f, 1f, 0.6f, 0, 0, 0, 0, 255, 161, 161, 160, 1);
            FootMountian = RegistryTerrainTypes.RegistryTerrainType(0.4f, 0.6f, 0.5f, 0, 0, 0, 0, 255, 99, 99, 99, 1);
             Plato = RegistryTerrainTypes.RegistryTerrainType(0.4f, 0.5f, 0.45f, 0, 0, 0, 0, 255, 90, 90, 60, 1);
           Hills = RegistryTerrainTypes.RegistryTerrainType(0.2f, 0.6f, 0.3f, 0.017f, 0, 0, 0, 255, 92, 115, 83, 1);
             Plain = RegistryTerrainTypes.RegistryTerrainType(0f, 0.2f, 0.1f, 0.025f, 0, 0, 0, 255, 69, 117, 53, 1);
          LowPlain = RegistryTerrainTypes.RegistryTerrainType(-0.2f, 0f, -0.1f, 0, 0, 0, 0, 255, 29, 86, 9, 1);
             Bank = RegistryTerrainTypes.RegistryTerrainType(-0.3f, -0.2f, -0.2f, 0, 0, 0, 0, 255, 112, 108, 83, 1);
             Ocean = RegistryTerrainTypes.RegistryTerrainType(-1.0f, -0.3f, -0.5f, 0, 0, 0, 0, 255, 30, 109, 195, 1);


    }
    }
}
