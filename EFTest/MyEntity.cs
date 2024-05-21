using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    public class MyEntity
    {
        private string name;
        private Guid id;

        private MyEntity(Guid id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public static MyEntity Create() => new(Guid.NewGuid(), Guid.NewGuid().ToString());

        public override string ToString() =>
            $"{nameof(MyEntity)}({nameof(id)} = {id}, {nameof(name)} = {name})";
    }
}
