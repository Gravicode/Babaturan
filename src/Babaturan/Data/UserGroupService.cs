using Microsoft.EntityFrameworkCore;
using Babaturan.Data;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class UserGroupService : ICrud<UserGroup>
    {
        BabaturanDB db;

        public UserGroupService()
        {
            if (db == null) db = new BabaturanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.UserGroups.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.UserGroups.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<UserGroup> FindByKeyword(string Keyword)
        {
            var data = from x in db.UserGroups.Include(c=>c.Group)
                       where x.Group.GroupName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<UserGroup> GetAllData()
        {
            return db.UserGroups.OrderBy(x => x.Id).ToList();
        }

        public UserGroup GetDataById(object Id)
        {
            return db.UserGroups.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(UserGroup data)
        {
            try
            {
                db.UserGroups.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(UserGroup data)
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
            return db.UserGroups.Max(x => x.Id);
        }
    }

}
/*











 */
