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
    
    public partial class ConstructionContract
    {
        public System.Guid Id { get; set; }
        public string ContractNumber { get; set; }
        public System.DateTime ContractDate { get; set; }
        public string MadeAt { get; set; }
        public string OwnerName { get; set; }
        public string OwnerAddress { get; set; }
        public string OwnerTel { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerType { get; set; }
        public string OwnerNumber { get; set; }
        public string ContractorName { get; set; }
        public string ContractorAddress { get; set; }
        public string ContractorTel { get; set; }
        public string ContractorEmail { get; set; }
        public string ContractorLicenseNumber { get; set; }
        public string DescriptionOfWork { get; set; }
        public string WorkSite { get; set; }
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
    }
}
