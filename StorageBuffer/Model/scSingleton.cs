using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageBuffer
{
    public sealed class scSingleton
    {
        private static scSingleton instance = null;
        private static readonly object padlock = new object();

        scSingleton()
        {
        }

        public static scSingleton Instance {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new scSingleton();
                    }
                    return instance;
                }
            }
        }
    }
}
