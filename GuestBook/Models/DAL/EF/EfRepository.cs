using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuestBook.Models.DAL.EF
{
    public class EfRepository : IRepository
    {
        private GuestBookContext _db = new GuestBookContext();

        public void Create(Record record)
        {
            _db.Records.Add(record);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Record record = Find(id);
            if(record != null)
            {
                _db.Records.Remove(record);
                _db.SaveChanges();
            }
        }

        public Record Find(int id)
        {
            return _db.Records.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Record> GetRecords()
        {
            return _db.Records.ToList();
        }

        public void Update(Record record)
        {
            Record recordFromDb = Find(record.Id);
            recordFromDb.Text = record.Text;
            recordFromDb.UpdationDate = record.UpdationDate;
            _db.SaveChanges();
        }
    }
}