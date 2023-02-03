using Microsoft.EntityFrameworkCore;
using Babaturan.Data;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class PostDislikeService : ICrud<PostDislike>
    {
        BabaturanDB db;

        public PostDislikeService()
        {
            if (db == null) db = new BabaturanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.PostDislikes.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.PostDislikes.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<PostDislike> FindByKeyword(string Keyword)
        {
            var data = from x in db.PostDislikes
                       where x.DislikedByUserName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<PostDislike> GetAllData()
        {
            return db.PostDislikes.OrderBy(x => x.Id).ToList();
        }

        public PostDislike GetDataById(object Id)
        {
            return db.PostDislikes.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(PostDislike data)
        {
            try
            {
                db.PostDislikes.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(PostDislike data)
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
            return db.PostDislikes.Max(x => x.Id);
        }
    }

}
/*











 */
