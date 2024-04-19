using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersionTwo.TerrainTools.terrainTypes;

namespace VersionTwo.Containers
{
    class BasicContainer<TypeContainer>
    {
        protected int id;
        protected TypeContainer[] Sides = new TypeContainer[4];
        protected internal void SetNeighbour(int Side, TypeContainer Neighbour)
        {
            Sides[Side] = Neighbour;
        }
        protected internal TypeContainer GetNeighbour(int Side)
        {
            return Sides[Side];
        }


    }
    

   

    


    
}
