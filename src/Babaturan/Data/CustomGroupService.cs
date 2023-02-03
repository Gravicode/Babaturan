using Microsoft.EntityFrameworkCore;
using Babaturan.Data;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class CustomGroupService : ICrud<CustomGroup>
    {
        BabaturanDB db;

        public CustomGroupService()
        {
            if (db == null) db = new BabaturanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.CustomGroups.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.CustomGroups.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<CustomGroup> FindByKeyword(string Keyword)
        {
            var data = from x in db.CustomGroups
                       where x.GroupName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<CustomGroup> GetAllData()
        {
            return db.CustomGroups.OrderBy(x => x.Id).ToList();
        }

        public CustomGroup GetDataById(object Id)
        {
            return db.CustomGroups.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(CustomGroup data)
        {
            try
            {
                db.CustomGroups.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(CustomGroup data)
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
            return db.CustomGroups.Max(x => x.Id);
        }
    }

}
/*











 */
