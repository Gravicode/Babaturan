using Microsoft.EntityFrameworkCore;
using Babaturan.Data;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class HidePostService : ICrud<HidePost>
    {
        BabaturanDB db;

        public HidePostService()
        {
            if (db == null) db = new BabaturanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.HidePosts.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.HidePosts.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<HidePost> FindByKeyword(string Keyword)
        {
            var data = from x in db.HidePosts.Include(c=>c.Post)
                       where x.Post.Message.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<HidePost> GetAllData()
        {
            return db.HidePosts.OrderBy(x => x.Id).ToList();
        }

        public HidePost GetDataById(object Id)
        {
            return db.HidePosts.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(HidePost data)
        {
            try
            {
                db.HidePosts.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(HidePost data)
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
            return db.HidePosts.Max(x => x.Id);
        }
    }

}
/*











 */
