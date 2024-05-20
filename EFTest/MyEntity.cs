using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    public class MyEntity
    {
        public static MyEntity Create() => new(Guid.NewGuid());

        private MyEntity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public override string ToString() =>
            $"{nameof(MyEntity)}({nameof(Id)} = {Id})";
    }
}
