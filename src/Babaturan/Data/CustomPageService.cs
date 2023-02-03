using Microsoft.EntityFrameworkCore;
using Babaturan.Data;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class CustomPageService : ICrud<CustomPage>
    {
        BabaturanDB db;

        public CustomPageService()
        {
            if (db == null) db = new BabaturanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.CustomPages.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.CustomPages.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<CustomPage> FindByKeyword(string Keyword)
        {
            var data = from x in db.CustomPages
                       where x.PageName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<CustomPage> GetAllData()
        {
            return db.CustomPages.OrderBy(x => x.Id).ToList();
        }

        public CustomPage GetDataById(object Id)
        {
            return db.CustomPages.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(CustomPage data)
        {
            try
            {
                db.CustomPages.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(CustomPage data)
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
            return db.CustomPages.Max(x => x.Id);
        }
    }

}
/*











 */
