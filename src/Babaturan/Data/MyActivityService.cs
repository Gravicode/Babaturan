using Microsoft.EntityFrameworkCore;
using Babaturan.Data;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class MyActivityService : ICrud<MyActivity>
    {
        BabaturanDB db;

        public MyActivityService()
        {
            if (db == null) db = new BabaturanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.MyActivitys.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.MyActivitys.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<MyActivity> FindByKeyword(string Keyword)
        {
            var data = from x in db.MyActivitys
                       where x.Name.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<MyActivity> GetAllData()
        {
            return db.MyActivitys.OrderBy(x => x.Id).ToList();
        }

        public MyActivity GetDataById(object Id)
        {
            return db.MyActivitys.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(MyActivity data)
        {
            try
            {
                db.MyActivitys.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(MyActivity data)
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
            return db.MyActivitys.Max(x => x.Id);
        }
    }

}
/*











 */
