using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class TinhTrangDAO
    {
        public List<TinhTrang> GetAll()
        {
            Model context = new Model();
            return context.TinhTrangs.ToList();
        }
    }
}
