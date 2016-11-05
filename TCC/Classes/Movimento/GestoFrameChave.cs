using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.Classes.Movimento.Poses;

namespace TCC.Classes.Movimento
{
    public class GestoFrameChave
    {
        public int QuadroLimiteInferior { get; private set; }
        public int QuadroLimiteSuperior { get; private set; }

        public BasePoses PoseChave { get; private set; }
        public GestoFrameChave(BasePoses poseChave, int limiteInferior, int limiteSuperior)
        {
            this.PoseChave = poseChave;
            this.QuadroLimiteInferior = limiteInferior;
            this.QuadroLimiteSuperior = limiteSuperior;
        }

    }
}
