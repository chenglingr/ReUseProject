using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReUse.ViewModels
{
    public class GoodsIndexViewModel
    {
        public List<Models.Goods> NewGoodss { get; set; }
        public List<Models.Goods> HotGoodss { get; set; }
    }
}