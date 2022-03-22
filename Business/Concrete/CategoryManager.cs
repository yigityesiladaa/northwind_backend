using Business.Abstract;
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
	public class CategoryManager : ICategoryService
	{
		private ICategoryDal _categoryDal;

		public CategoryManager(ICategoryDal categoryDal)
		{
			_categoryDal = categoryDal;
		}
		public IDataResult<Category> GetById(int categoryId)
		{
			return new SuccessDataResult<Category>(_categoryDal.Get(p => p.CategoryId == categoryId));
		}

		public IDataResult<List<Category>> GetList()
		{
			return new SuccessDataResult<List<Category>>(_categoryDal.GetList().ToList());
		}
	}
}
