using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreFront3.DATA.EF//.Metadata
{
    #region ProductsMetaData

    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    { }

    public class ProductMetadata
    {

        public int ProductID { get; set; }

        //[Key]
        [Required]
        [StringLength(50)]
        [Display(Name = "Product")]
        public string ProductName { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "*")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Display(Name = "Price")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "*")]
        public int StockStatusID { get; set; }

        [Required(ErrorMessage = "*")]
        [UIHint("MultilineText")]
        public string Description { get; set; }


        [UIHint("MultilineText")]
        [DisplayFormat(NullDisplayText = "-N/A-")]
        [Display(Name = "Image")]
        public string ProductImage { get; set; }
    }


    #endregion

    #region StockStatusMetadata

    [MetadataType(typeof(StockStatusMetadata))]
    public partial class StockStatus { }

    public class StockStatusMetadata
    {
        
        [Required(ErrorMessage = "*")]
        public int StockStatusID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Stock Status")]
        [StringLength(50, ErrorMessage = "Must be atleast 50 characters or less.")]
        public string StockStatus1 { get; set; }
    }


    #endregion

    #region CategoriesMetadata

    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category { }

    public class CategoryMetadata
    {
        [Display(Name = "Category")]
        [Required(ErrorMessage = "*")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Category")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string CategoryName { get; set; }
    }


    #endregion

    #region DepartmentsMetadata

    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department { }

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



    #endregion

    #region EmployeesMetadata

    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee { }

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
        public int DirectReportID { get; set; }

        [Required(ErrorMessage = "*")]
        public int DepartmentID { get; set; }



    }


    #endregion


}
