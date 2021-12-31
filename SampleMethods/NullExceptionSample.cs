using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMethods
{
    public class NullExceptionSample
    {
        public static void Execute()
        {
            throw new NullReferenceException();
        }
    }
}
