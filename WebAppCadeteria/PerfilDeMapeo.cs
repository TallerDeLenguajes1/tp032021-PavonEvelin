﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models;
using WebApp_Cadeteria.Models.ViewModels;

namespace WebApp_Cadeteria
{
    public class PerfilDeMapeo : Profile
    {
        public PerfilDeMapeo()
        {
            //CreateMap<List<Cadete>, List<CadeteViewModel>>().ReverseMap();
            CreateMap<Cadete, CadeteViewModel>().ReverseMap();
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
            CreateMap<Cadete, Usuario>().ReverseMap();

            CreateMap<Pedidos, PedidoViewModel>().ReverseMap();

            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
        }
    }
}
