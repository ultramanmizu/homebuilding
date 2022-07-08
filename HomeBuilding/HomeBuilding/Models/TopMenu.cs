using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeBuilding.Models
{
    public class MenuView
    {
        public List<TopMenu> TopMenus { get ; set; }
    }

    public class TopMenu
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public int Seq { get; set; }
    }
}