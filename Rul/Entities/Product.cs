//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rul.Entities
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Order = new HashSet<Order>();
        }
        public string ProductArticleNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public string ProductImage { get; set; }
        public string ProductManufacturer { get; set; }
        public decimal ProductCost { get; set; }
        public Nullable<byte> ProductDiscountAmount { get; set; }
        public int ProductQuantityInStock { get; set; }
        public string ProductStatus { get; set; }
        public string Unit { get; set; }
        public byte MaxDiscountAmount { get; set; }
        public string Supplier { get; set; }
        public Nullable<int> CountInPack { get; set; }
        public Nullable<int> MinCount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }

        public string ImgPath
        {
            get
            {
                var path = "pack://application:,,,/Resources/" + this.ProductImage;
                return path;
            }
        }
        public string Background
        {
            get
            {
                if (this.ProductDiscountAmount > 15)
                    return "#7fff00";
                return "#fff";
            }
        }
    }
}
