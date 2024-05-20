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

        private MyEntity(Guid id, string name)
        {
            Id = id;
            this.name = name;
        }

        public Guid Id { get; private set; }

        public static MyEntity Create() => new(Guid.NewGuid(), Guid.NewGuid().ToString());

        public override string ToString() =>
            $"{nameof(MyEntity)}({nameof(Id)} = {Id}, {nameof(name)} = {name})";
    }
}
