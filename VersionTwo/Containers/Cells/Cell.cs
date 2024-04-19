using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersionTwo.TerrainTools.terrainTypes;

namespace VersionTwo.Containers.Cells
{
    class Cell : BasicContainer<Cell_>
    {
        float height;

        public Cell(int id)
        {
            this.id = id;
        }
        public void SetHeight(float height)
        {
            this.height = height;
        }
        public float GetHeight()
        { return this.height; }
    }
}
