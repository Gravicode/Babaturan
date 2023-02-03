using Microsoft.EntityFrameworkCore;
using Babaturan.Data;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class PictureAlbumService : ICrud<PictureAlbum>
    {
        BabaturanDB db;

        public PictureAlbumService()
        {
            if (db == null) db = new BabaturanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.PictureAlbums.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.PictureAlbums.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<PictureAlbum> FindByKeyword(string Keyword)
        {
            var data = from x in db.PictureAlbums
                       where x.Name.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<PictureAlbum> GetAllData()
        {
            return db.PictureAlbums.OrderBy(x => x.Id).ToList();
        }

        public PictureAlbum GetDataById(object Id)
        {
            return db.PictureAlbums.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(PictureAlbum data)
        {
            try
            {
                db.PictureAlbums.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(PictureAlbum data)
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
            return db.PictureAlbums.Max(x => x.Id);
        }
    }

}
/*











 */
