
using VersionTwo.TerrainTools.terrainTypes;

namespace VersionTwo.Containers.TerrainSectors
{
    internal class TerrainSector : BasicContainer<TerrainContainer>
    {
        Terrain terrain;
        public TerrainSector(int ID)
        {
            id = ID;
        }
        public void SetTerrain(int Rank)
        {
            terrain = new Terrain(Rank);
        }
        public Terrain GetTerrain()
        {
            return terrain;
        }
    }
}
