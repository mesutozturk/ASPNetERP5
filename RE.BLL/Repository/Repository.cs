using RE.Models.Entities;

namespace RE.BLL.Repository
{
    public class ProductRepo : RepositoryBase<Product, int> { }
    public class CategoryRepo : RepositoryBase<Category, int> { }
    public class EmployeeRepo : RepositoryBase<Employee, int> { }
    public class ShipperRepo : RepositoryBase<Shipper, int> { }
    public class SupplierRepo : RepositoryBase<Supplier, int> { }
    public class OrderDetailRepo : RepositoryBase<Order_Detail, int> { }
    public class CustomerRepo : RepositoryBase<Customer, string> { }
}