using Microsoft.EntityFrameworkCore;
using Babaturan.Data;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class ReportPostService : ICrud<ReportPost>
    {
        BabaturanDB db;

        public ReportPostService()
        {
            if (db == null) db = new BabaturanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.ReportPosts.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.ReportPosts.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<ReportPost> FindByKeyword(string Keyword)
        {
            var data = from x in db.ReportPosts.Include(c=>c.Post)
                       where x.Post.Message.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<ReportPost> GetAllData()
        {
            return db.ReportPosts.OrderBy(x => x.Id).ToList();
        }

        public ReportPost GetDataById(object Id)
        {
            return db.ReportPosts.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(ReportPost data)
        {
            try
            {
                db.ReportPosts.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(ReportPost data)
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
            return db.ReportPosts.Max(x => x.Id);
        }
    }

}
/*











 */
