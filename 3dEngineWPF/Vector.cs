using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace _3dEngineWPF
{
    internal static class Vector
    {
        [JitIntrinsic]
        public static bool IsHardwareAccelerated
        {
            get
            {
                return false;
            }
        }
    }
}
