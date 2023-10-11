using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BenhNhanDAO
    {
        public List<BenhNhan> GetAll()
        {
            Model context = new Model();
            return context.BenhNhans.ToList();
        }
        public BenhNhan FindTheoTruyVet(string id)
        {
                Model context = new Model();
                return context.BenhNhans.FirstOrDefault(p => p.BNTXG == id);
        }

        public List<BenhNhan> TruyVet(string id)
        {
            Model context = new Model();
            List<BenhNhan> a = new List<BenhNhan>();
            BenhNhan item = FindTheoTruyVet(id);
            while (item != null)
            {
                a.Add(item);
                id = item.MaBN;
                item = FindTheoTruyVet(id);
            }

            return a;
        }
        public int TinhF(string id)
        {
                try
                {
                    Model context = new Model();
                    int i = -1;
                    BenhNhan a = FindByID(id);
                    while (a != null)
                    {
                        id = a.BNTXG;
                        a = FindByID(id);
                        i++;
                    }
                    return i;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        

        public BenhNhan FindByID(string id)
        {
            Model context = new Model();
            return context.BenhNhans.FirstOrDefault(p => p.MaBN == id);
        }

        public void InsertUpdate(BenhNhan a)
        {
            Model context = new Model();
            context.BenhNhans.AddOrUpdate(a);
            context.SaveChanges();
        }

    }
}
