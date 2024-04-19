using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersionTwo.Containers;
using VersionTwo.Containers.Continets;
using VersionTwo.DrawingTools.BMPGenerator;
using VersionTwo.generator.ContinentGeneration;
using VersionTwo.TerrainTools.TerraiGeneration;
using VersionTwo.TerrainTools.terrainTypes;

namespace VersionTwo.generator
{
    internal struct Highpoints(int i)
    {
        public ContinentSector[] Map = new ContinentSector[i];
    }
    internal class SettingsGenerator
    {
        

        static SectorsCollection GlobalCollection = new SectorsCollection();
        static MaskCollection MaskCollection = new MaskCollection();
        
        public void StartGeneration(int Seed)
        {
            float chanceToGenerate = 0.6f;
            MaskCollection.ContinentCollection = basicAlgorithm.StartContinentGeneration(10 , chanceToGenerate, 0, 0);
            HeighboursSearch.HeighborsSearch(MaskCollection.ContinentCollection);
            var resukt = basicAlgorithm.GenerationHighPoint(MaskCollection.ContinentCollection, chanceToGenerate);
            MaskCollection.TerrainCollection = basicAlgorithm.StartTerrainCell(MaskCollection.ContinentCollection);
            basicAlgorithm.TransformContinentIntoTerrain(MaskCollection.TerrainCollection, MaskCollection.ContinentCollection);

            basicAlgorithm.TransformCellIntoTerritory(MaskCollection.TerrainCollection);
            basicAlgorithm.SetHighPoint(MaskCollection.TerrainCollection, resukt);
            basicAlgorithm.FormMountianMassive(MaskCollection.TerrainCollection);
            basicAlgorithm.FormFootMontianMassive(MaskCollection.TerrainCollection);
            basicAlgorithm.FormLowTerrainForFirstTemplate(MaskCollection.TerrainCollection, 3, true);
            basicAlgorithm.FormLowTerrainForSecondTemplate(MaskCollection.TerrainCollection, 4, 0.89f);

            basicAlgorithm.FormLowTerrainForSecondTemplate(MaskCollection.TerrainCollection, 3, 0.3f, 4);
            DrawingMap.DrawTerrainMap(MaskCollection.TerrainCollection,0);
            MaskCollection.CellCollection =  basicAlgorithm.StartCell(MaskCollection.TerrainCollection);
            basicAlgorithm.TransformTerrainIntoHeigtmap(MaskCollection.TerrainCollection, MaskCollection.CellCollection);
            basicAlgorithm.GenerationRandomeHeightmap(MaskCollection.TerrainCollection, MaskCollection.CellCollection);
            DrawingMap.DrawHeightmap(MaskCollection.CellCollection, 0);
            Seed++;

        }
    }
}
