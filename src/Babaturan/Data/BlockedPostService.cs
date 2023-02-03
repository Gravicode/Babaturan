using Microsoft.EntityFrameworkCore;
using Babaturan.Data;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class BlockedPostService : ICrud<BlockedPost>
    {
        BabaturanDB db;

        public BlockedPostService()
        {
            if (db == null) db = new BabaturanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.BlockedPosts.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.BlockedPosts.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<BlockedPost> FindByKeyword(string Keyword)
        {
            var data = from x in db.BlockedPosts.Include(c=>c.Post)
                       where x.Post.Message.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<BlockedPost> GetAllData()
        {
            return db.BlockedPosts.OrderBy(x => x.Id).ToList();
        }

        public BlockedPost GetDataById(object Id)
        {
            return db.BlockedPosts.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(BlockedPost data)
        {
            try
            {
                db.BlockedPosts.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(BlockedPost data)
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
            return db.BlockedPosts.Max(x => x.Id);
        }
    }

}
/*











 */
