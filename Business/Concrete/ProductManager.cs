﻿using Business.Abstract;
using Business.Constants;
using Business.CSS;
using Business.ValidationRules.FluentValidation;
using Core.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService

    {
        IProductDal _productdal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal ,ICategoryService categoryService)
        {
            _productdal = productDal;
            _categoryService = categoryService;
        }


        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId), 
                CheckIfProductNameExists(product.ProductName),CheckIfCategoryLimitExceded());
            if (result!=null)
            {
                return result;
            }
            _productdal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
            

        }
            
        
        


        public IDataResult<List<Product>> GetAll()
        {
           // if (DateTime.Now.Hour==16)
           //{
           //     return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);

           // }
            return new SuccessDataResult<List<Product>>(_productdal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
           return new SuccessDataResult<List<Product>>(_productdal.GetAll(p=>p.CategoryId == id));
        }

        public IDataResult<Product>  GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productdal.Get(p=>p.ProductId==productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productdal.GetAll(p=>p.UnitPrice>=min &&  p.UnitPrice<=max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            
            return new SuccessDataResult<List<ProductDetailDto>>(_productdal.GetProductDetails());
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryid)
        {
            var result = _productdal.GetAll(p => p.CategoryId == categoryid).Count;
            if (result>15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string  productname)
        {
            var result = _productdal.GetAll(p => p.ProductName == productname).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }

}