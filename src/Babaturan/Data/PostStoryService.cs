using Microsoft.EntityFrameworkCore;
using Babaturan.Data;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class PostStoryService : ICrud<PostStory>
    {
        BabaturanDB db;

        public PostStoryService()
        {
            if (db == null) db = new BabaturanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.PostStorys.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.PostStorys.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<PostStory> FindByKeyword(string Keyword)
        {
            var data = from x in db.PostStorys
                       where x.UserName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<PostStory> GetAllData()
        {
            return db.PostStorys.OrderBy(x => x.Id).ToList();
        }

        public PostStory GetDataById(object Id)
        {
            return db.PostStorys.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(PostStory data)
        {
            try
            {
                db.PostStorys.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(PostStory data)
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
            return db.PostStorys.Max(x => x.Id);
        }
    }

}
/*











 */
