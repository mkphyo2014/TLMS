using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLMS.Entity.Dto
{

    
    public class HelloRequest
    {
        public string InputMessage { get; set; }
    }

    public class HelloResponse
    {
        public string Message { get; set; }
    }
}
