//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomeBuilding
{
    using System;
    using System.Collections.Generic;
    
    public partial class Receipt
    {
        public System.Guid Id { get; set; }
        public System.Guid ConstructionContractId { get; set; }
        public string ReceiptNumber { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public decimal Total { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.Guid> CreatedById { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.Guid> UpdatedById { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public Nullable<System.Guid> DeletedById { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> Round { get; set; }
    }
}
