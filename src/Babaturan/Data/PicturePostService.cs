using Microsoft.EntityFrameworkCore;
using Babaturan.Data;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class PicturePostService : ICrud<PicturePost>
    {
        BabaturanDB db;

        public PicturePostService()
        {
            if (db == null) db = new BabaturanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.PicturePosts.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.PicturePosts.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<PicturePost> FindByKeyword(string Keyword)
        {
            var data = from x in db.PicturePosts.Include(c=>c.Album)
                       where x.Album.Name.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<PicturePost> GetAllData()
        {
            return db.PicturePosts.OrderBy(x => x.Id).ToList();
        }

        public PicturePost GetDataById(object Id)
        {
            return db.PicturePosts.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(PicturePost data)
        {
            try
            {
                db.PicturePosts.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(PicturePost data)
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
            return db.PicturePosts.Max(x => x.Id);
        }
    }

}
/*











 */
