using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q.Shader._2D
{
    public class EXBasicBlur : Effect
    {

        public EXBasicBlur() : base("engine/shader/blur2vs.glsl", "engine/shader/blur2fs.glsl")
        {

        }
        public override void BindPars()
        {
            //base.BindPars();
            SetUniform("image", 0);
        }

    }
}
