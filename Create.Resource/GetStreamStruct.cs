using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Create.Resource
{
    public ref struct GetStreamStruct
    {
        object? sender;
        ResourceFile file;

        public GetStreamStruct() => throw new NotSupportedException();

        internal GetStreamStruct(object? sender, ResourceFile file)
        {
            this.sender = sender;
            this.file = file;
        }

        public object? Sender => sender;
        public ResourceFile File => file;
    }
}
