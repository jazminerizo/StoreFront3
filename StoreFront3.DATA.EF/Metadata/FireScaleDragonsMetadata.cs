using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreFront3.DATA.EF//.Metadata
{
    #region ProductsMetaData

    public class ProductMetadata
    {

        public int ProductID { get; set; }

        //[Key]
        [Required]
        [StringLength(50)]
        [Display(Name = "Product")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "*")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Display(Name = "Price")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "*")]
        public bool StockStatusID { get; set; }

        [Required(ErrorMessage = "*")]
        [UIHint("MultilineText")]
        public string Description { get; set; }


        [UIHint("MultilineText")]
        [DisplayFormat(NullDisplayText = "-N/A-")]
        [Display(Name = "Image")]
        public string ProductImage { get; set; }
    }

    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    { }

    #endregion

    #region StockStatusMetadata

    public class StockStatusMetadata
    {
        
        [Required(ErrorMessage = "*")]
        public int StockStatusID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Stock Status")]
        [StringLength(50, ErrorMessage = "Must be atleast 50 characters or less.")]
        public string StockStatus1 { get; set; }
    }

    [MetadataType(typeof(StockStatusMetadata))]
    public partial class StockStatus { }

    #endregion

    #region CategoriesMetadata

    public class CategoryMetadata
    {
        
        [Required(ErrorMessage = "*")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Category")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string CategoryName { get; set; }
    }

    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category { }

    #endregion

    #region DepartmentsMetadata

    public class DepartmentMetadata
    {
        [Key]
        [Required(ErrorMessage = "*")]
        public int DepartmentID { get; set; }

        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        [Display(Name = "Department")]
        [UIHint("MultilineText")]
        public string DepartmentName { get; set; }
    }

    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department { }


    #endregion

    #region EmployeesMetadata

    public class EmployeeMetadata
    {
        
        [Required(ErrorMessage = "*")]
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "Must be atleast 50 characters")]
        [Display(Name = "Name")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "*")]
        public int DepartmentID { get; set; }



    }

    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee { }

    #endregion


}
