using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RESTServices.Domain;
using RESTServices.BLL.DTOs;

namespace RESTServices.BLL.Profiles
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<Category, CategoryDTO>().ReverseMap();
			CreateMap<CategoryCreateDTO, Category>();
			CreateMap<CategoryUpdateDTO, Category>();
			CreateMap<Article, ArticleDTO>().ReverseMap();
			CreateMap<ArticleCreateDTO, Article>();
			CreateMap<ArticleUpdateDTO, Article>();
			CreateMap<Role, RoleDTO>().ReverseMap();
			CreateMap<RoleCreateDTO, Role>();
			CreateMap<LoginDTO, User>();
		}
	}
}
