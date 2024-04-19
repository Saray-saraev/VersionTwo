namespace VersionTwo.TerrainTools.terrainTypes
{
    public struct Terrain(int Rank)
    {
        public int rank { get; } = Rank;
        public bool IsLand()
        {
            if (rank < -1) 
            { 
                return true; 
            }
            else
            {
                return false;
            }
        }

        public TerrainType GetTerrain()
        {

            return TerrainTypeCollection.GetTerrain(rank);
        }
    }
    internal class RegistryTerrainTypes
    {
        public static Terrain RegistryTerrainType(float min, float max, float defaultH, float generate, float BonusThisType, float BonusUpType, float BonusDownType, int alpha, int red, int green, int blue, int width)
        {

            TerrainType terrain = new TerrainType(new heights(min, max, defaultH), new Chances(generate, BonusThisType, BonusUpType, BonusDownType));
            terrain.SetDrawTools(alpha, red, green, blue, width);
            return new Terrain(TerrainTypeCollection.AddNewTerrainTypeInArray(terrain));
        }
    }
}
