﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace OOADProject.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Database1Entities : DbContext
    {
        public Database1Entities()
            : base("name=Database1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CarCatalog> CarCatalog { get; set; }
        public virtual DbSet<CarCompany> CarCompany { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<DR_Car> DR_Car { get; set; }
        public virtual DbSet<DR_CarStation> DR_CarStation { get; set; }
        public virtual DbSet<DR_RentalCompany> DR_RentalCompany { get; set; }
        public virtual DbSet<SearchHistoryLog> SearchHistoryLog { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
