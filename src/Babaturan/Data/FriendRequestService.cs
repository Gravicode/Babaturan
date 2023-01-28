using Microsoft.EntityFrameworkCore;
using Babaturan.Data;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class FriendRequestService : ICrud<FriendRequest>
    {
        BabaturanDB db;

        public FriendRequestService()
        {
            if (db == null) db = new BabaturanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.FriendRequests.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.FriendRequests.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<FriendRequest> FindByKeyword(string Keyword)
        {
            var data = from x in db.FriendRequests
                       where x.UserName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<FriendRequest> GetAllData()
        {
            return db.FriendRequests.OrderBy(x => x.Id).ToList();
        }

        public FriendRequest GetDataById(object Id)
        {
            return db.FriendRequests.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(FriendRequest data)
        {
            try
            {
                db.FriendRequests.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(FriendRequest data)
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
            return db.FriendRequests.Max(x => x.Id);
        }
    }

}
/*











 */
