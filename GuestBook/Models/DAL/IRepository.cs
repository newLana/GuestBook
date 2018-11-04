using System.Collections.Generic;

namespace GuestBook.Models
{
    public interface IRepository
    {
        IEnumerable<Record> GetRecords();

        void Create(Record record);

        Record Find(int id);

        void Update(Record record);

        void Delete(int id);
    }
}
