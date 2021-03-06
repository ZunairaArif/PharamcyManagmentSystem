//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PharmacyManagmentSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class stock
    {
        public stock()
        {
            this.borrowlendstocks = new HashSet<borrowlendstock>();
            this.employerstocks = new HashSet<employerstock>();
            this.stocksales = new HashSet<stocksale>();
        }
    
        public int stockId { get; set; }
        public int orderDetailId { get; set; }
        public int quantity { get; set; }
        public string batchNO { get; set; }
        public System.DateTime expiryDate { get; set; }
        public double sellingPricePrItem { get; set; }
        public Nullable<int> itemSold { get; set; }
        public Nullable<int> expireDays { get; set; }
    
        public virtual ICollection<borrowlendstock> borrowlendstocks { get; set; }
        public virtual ICollection<employerstock> employerstocks { get; set; }
        public virtual orderdetail orderdetail { get; set; }
        public virtual ICollection<stocksale> stocksales { get; set; }
    }
}
