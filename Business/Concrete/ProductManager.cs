using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
	public class ProductManager : IProductService
	{
		//Dependency Injection

		private IProductDal _productDal;

		public ProductManager(IProductDal productDal)
		{
			_productDal = productDal;
		}

		//Cross Cutting Concerns --> Validation, Cache, Log, Performance, Auth, Transaction
		//AOP --> Aspect Orianted Programming

		[ValidationAspect(typeof(ProductValidator))]
		[CacheRemoveAspect("IProductService.Get")]
		public IResult Add(Product product)
		{

			_productDal.Add(product);
			return new SuccessResult(Messages.ProductAdded);
		}

		public IResult Update(Product product)
		{
			_productDal.Update(product);
			return new SuccessResult(Messages.ProductUpdated);
		}

		public IResult Delete(Product product)
		{
			_productDal.Delete(product);
			return new SuccessResult(Messages.ProductDeleted);
		}

		public IDataResult<Product> GetById(int productId)
		{
			return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId == productId));
		}

		public IDataResult<List<Product>> GetList()
		{
			return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
		}

		[CacheAspect(10)]
		public IDataResult<List<Product>> GetListByCategory(int categoryId)
		{
			return new SuccessDataResult<List<Product>>(_productDal.GetList(p=>p.CategoryId == categoryId).ToList());
		}

		[TransactionScopeAspect]
		public IResult TransactionalOperation(Product product)
		{
			_productDal.Update(product);
			_productDal.Add(product);
			return new SuccessResult(Messages.ProductUpdated);
		}
	}
}
