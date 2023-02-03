using Microsoft.EntityFrameworkCore;
using Babaturan.Data;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class SavedPostService : ICrud<SavedPost>
    {
        BabaturanDB db;

        public SavedPostService()
        {
            if (db == null) db = new BabaturanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.SavedPosts.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.SavedPosts.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<SavedPost> FindByKeyword(string Keyword)
        {
            var data = from x in db.SavedPosts.Include(c=>c.Post)
                       where x.Post.Message.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<SavedPost> GetAllData()
        {
            return db.SavedPosts.OrderBy(x => x.Id).ToList();
        }

        public SavedPost GetDataById(object Id)
        {
            return db.SavedPosts.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(SavedPost data)
        {
            try
            {
                db.SavedPosts.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(SavedPost data)
        {
            try
            {
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                /*
                if (sel != null)
                {
                    sel.Nama = data.Nama;
                    sel.Keterangan = data.Keterangan;
                    sel.Tanggal = data.Tanggal;
                    sel.DocumentUrl = data.DocumentUrl;
                    sel.StreamUrl = data.StreamUrl;
                    return true;

                }*/
                return true;
            }
            catch
            {

            }
            return false;
        }

        public long GetLastId()
        {
            return db.SavedPosts.Max(x => x.Id);
        }
    }

}
/*











 */
