using BlossmAPI.Attributes;
using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Patterns.Singleton;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;

namespace BlossmAPI.Repositories.Services
{
    
    public class ProductServices: IProductServices, IProductVariantServices
    {
        private readonly BlossmContext _context;
        
        public ProductFilterView _filter;

        public ProductServices(BlossmContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
				products = await _context.Products
                    .Include(p => p.IdCategoryNavigation)
                    .ToListAsync();

                foreach(var product in products)
                {
                    product.IdCategoryNavigation.Products = null;
                }
                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return products;
        }
        public async Task<IEnumerable<ProductVariant>> GetVariantsByProductId(int id)
        {
            var rs = await _context.ProductVariants
                .Include(v => v.IdUnitNavigation)
                .Include(v => v.IdColorNavigation)
                .Include(v => v.IdSizeNavigation)
                .Where(v => v.IdProduct == id)
                .ToListAsync();
            foreach (var v in rs)
            {
                v.IdUnitNavigation.ProductVariants = null;
                v.IdSizeNavigation.ProductVariants = null;
                v.IdColorNavigation.ProductVariants = null;
            }
            return rs;
        }
        public async Task<ProductVariant> GetProductVariantById(int id)
        {
            var rs = await _context.ProductVariants
                .FirstOrDefaultAsync(v => v.Id == id);
            if(rs != null)
            {
                return rs;
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<ProductVariant> GetVariants(ProductFilterView _filter)
        {
            #region Filter data
            //IQueryable cho phép sử dụng Linq để tiếp tục lọc data
            IQueryable<ProductVariant> variants = _context.ProductVariants;
            if(_filter.name != "string")
            {
                variants = variants
                    .Include(v =>
                        v.IdProductNavigation)
                .Where(v =>
                        v.IdProductNavigation.Name.ToUpper().Contains(_filter.name.ToUpper()));
                foreach (var variant in variants)
                {
                    variant.IdProductNavigation.ProductVariants = null;
                    
                }
            }
            if (_filter.size_id != 0)
            {
                variants = variants
                    .Where(v => 
                        v.IdSize == _filter.size_id);
            }
            if (_filter.unit_id != 0)
            {
                variants = variants
                    .Where(v => 
                        v.IdUnit == _filter.unit_id);
            }
            if (_filter.color_id != 0)
            {
                variants = variants
                    .Where(v => 
                        v.IdColor == _filter.color_id);
            }
            if (_filter.cate_id != 0)
            {
                variants = variants
                    .Include(v => 
                        v.IdProductNavigation)
                .Where(v => 
                        v.IdProductNavigation.IdCategory == _filter.cate_id);
                foreach (var variant in variants)
                {
                    variant.IdProductNavigation.ProductVariants = null;
                }
            }
            var finals = variants.Skip((_filter.page_num - 1) * _filter.page_size)
                .Take(_filter.page_size)
                .Include(v => v.Images)
                .ToList();

            foreach(var final in finals)
            {
                foreach(var imgage in final.Images)
                {
                    imgage.IdProductVariantNavigation = null;
                }
            }

            #endregion
            return finals;
        }
        public async Task<IEnumerable<ProductVariant>> GetVariantsByBranch(int branchId)
        {
            var rs = await _context.ProductVariants
                .Include(v => v.BranchProductVariants)
                .Where(v => v.BranchProductVariants.Where(b => b.IdBranch == branchId).Any())
                .ToListAsync();
			if (rs != null)
			{
				return rs;
			}
			else
			{
				return null;
			}
		}
        public async Task<bool> CreateVariant(ProductView new_variant)
        {
            ProductVariant variant = await bindVariant(new_variant);
            try
            {
                _context.ProductVariants.Add(variant);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateVariant(ProductView updated_product)
        {
            var variant =  await GetProductVariantById(updated_product.variant_id);

            variant.SellingPrice = updated_product.product_selling_price;
            variant.PurchasePrice = updated_product.product_purchase_price;
            variant.Publish = updated_product.publish;
            variant.IdUnit = updated_product.unit_id;
            variant.IdColor = updated_product.color_id;
            variant.IdSize = updated_product.size_id;
            variant.UpdateDate = DateTime.Now;
            
            try
            {
                _context.ProductVariants.Update(variant);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> CreateProduct(ProductView new_product)
        {
            #region Create product
            Product product = await bindMVProduct(new_product);
            if (product != null)
            {
                try
                {
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
            #endregion
        }
        public async Task<bool> UpdateProduct(ProductView updated_product)
        {
            Product prod = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == updated_product.product_id);
            prod.Name = updated_product.product_name;
            prod.IdCategory = updated_product.cate_id;
            prod.ShortDescription = updated_product.short_des;
            prod.MainDescription = updated_product.main_des;
            if (prod != null)
            {
                try
                {
                    _context.Products.Update(prod);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }
        public async Task DeleteProduct(Product current_product)
        {
            if(current_product != null)
            {
                try
                {
                    _context.Products.Remove(current_product);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private async Task<Product> bindMVProduct(ProductView new_product)
        {
            Product product = new Product();
            var variant = await bindVariant(new_product);

            product.Name = new_product.product_name;
            product.IdCategory = new_product.cate_id;

            foreach (var img_item in new_product.Images)
            {
                Image image = new Image();
                image.Images = img_item;
                image.Avatar = false;
                variant.Images.Add(image);
            }

            if(variant.Id == 0)
            {
                product.ProductVariants.Add(variant);
            }

            return product;
        }
        private async Task<ProductVariant> bindVariant(ProductView new_product)
        {
            ProductVariant variant = new ProductVariant();

            variant.Id = new_product.variant_id;
            if (variant.Id != 0)
            {
                variant = await GetProductVariantById(variant.Id);
            }
            else
            {
                variant.CreationDate = DateTime.Now;
            }
            variant.IdProduct = new_product.product_id;
            variant.SellingPrice = new_product.product_selling_price;
            variant.PurchasePrice = new_product.product_purchase_price;
            variant.IdUnit = new_product.unit_id;
            variant.IdColor = new_product.color_id;
            variant.IdSize = new_product.size_id;
            variant.UpdateDate = DateTime.Now;
            variant.Publish = new_product.publish;

            return variant;
        }
    }
}
