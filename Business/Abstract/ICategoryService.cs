using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
	public interface ICategoryService
	{
		IDataResult<Category> GetById(int productId);
		IDataResult<List<Category>> GetList();
	}
}
