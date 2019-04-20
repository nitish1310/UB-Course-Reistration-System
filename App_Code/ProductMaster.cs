using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RazorpaySampleApp
{
    public class ProductMaster
    {
        public int pId { get; set; }

        public string prodName { get; set; }

        public string shortDescription { get; set; }

        public string description { get; set; }

        public Nullable<int> price { get; set; }

        public string prodImg { get; set; }

        public string prodDetailImg { get; set; }

        public string tagline { get; set; }

        public byte[] mobImg { get; set; }

        public DateTime createDate { get; set; }


    }
}