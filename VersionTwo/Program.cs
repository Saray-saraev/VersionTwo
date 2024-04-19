using VersionTwo.DrawingTools;
using System.Drawing;
using VersionTwo.TerrainTools.terrainTypes;
using VersionTwo.Containers;
using VersionTwo.generator;
using VersionTwo.TerrainTools.TerraiGeneration;

namespace VersionTwo
{
    internal class Program
    {
        static PenCollection pes = new PenCollection();
        static TerrainTypeCollection terrain = new TerrainTypeCollection();
        static TerrainCollection terrainCollection = new TerrainCollection();
        static SettingsGenerator generator = new SettingsGenerator();
       
        static void Main(string[] args)
        {
            
            
            generator.StartGeneration(1);
            Console.WriteLine(  "успешная генерация");

            Console.ReadLine();
        }
    }
}
