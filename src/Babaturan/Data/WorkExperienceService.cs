using Microsoft.EntityFrameworkCore;
using Babaturan.Data;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class WorkExperienceService : ICrud<WorkExperience>
    {
        BabaturanDB db;

        public WorkExperienceService()
        {
            if (db == null) db = new BabaturanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.WorkExperiences.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.WorkExperiences.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<WorkExperience> FindByKeyword(string Keyword)
        {
            var data = from x in db.WorkExperiences
                       where x.Company.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<WorkExperience> GetAllData()
        {
            return db.WorkExperiences.OrderBy(x => x.Id).ToList();
        }

        public WorkExperience GetDataById(object Id)
        {
            return db.WorkExperiences.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(WorkExperience data)
        {
            try
            {
                db.WorkExperiences.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(WorkExperience data)
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
            return db.WorkExperiences.Max(x => x.Id);
        }
    }

}
/*











 */
