using System.Drawing;
using VersionTwo.DrawingTools;

namespace VersionTwo.TerrainTools.terrainTypes
{
    public struct Chances(float generate, float BonusThisType, float BonusUpType, float BonusDownType)
    {
        float chanceToGenerate = generate;
        float BonusForNeighboursThisType = BonusThisType;
        float BonusForNeighboursDownRankType = BonusDownType;
        float BonusForNeighboursUpRankType = BonusUpType;
        public float GetChanceToGenerate()
        { return chanceToGenerate; }

    }
    public struct heights(float min, float max, float defaultH)
    {
        public float mininalHeight { get; } = min;
        public float maximumHeight { get; } = max;
        public float defaultHeight { get; } = defaultH;
        
    }
    public class TerrainType
    {
        DrawTool ColorOfTerrain;
        int TerrainRank;
        Chances chances;
        heights heights;

        public TerrainType(heights heights, Chances chances)
        {
            this.heights = heights;
            this.chances = chances;
        }
        public void SetDrawTools(int alpha, int red, int green, int blue, int width)
        {
            ColorOfTerrain = RegistryDrawTools.RegistryTools(true, alpha, red, green, blue, width);
        }
        public SolidBrush GetBrush()
        {
            return ColorOfTerrain.GetBrush();
        }
        public void SetRank(int rank)
        {
            TerrainRank = rank;
        }
        public float GetChance()
        {
           return chances.GetChanceToGenerate();
        }
        public heights GetHeight()
        {
            return heights;
        }
    }
}
