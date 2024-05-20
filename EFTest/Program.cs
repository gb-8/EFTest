namespace EFTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var writeContext = new MyDbContext())
            {
                var newEntity = MyEntity.Create();
                writeContext.Entities.Add(newEntity);
                writeContext.SaveChanges();
            }

            using (var readContext = new MyDbContext())
            {
                var entities = readContext.Entities.ToList();
                foreach (var entity in entities)
                {
                    Console.WriteLine(entity);
                }
            }

            Console.WriteLine("Hit any key to exit.");
            Console.ReadKey();
        }
    }
}
