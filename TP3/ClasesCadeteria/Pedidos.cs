﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadeteria
{
    class Pedidos
    {
        private int numero;
        private string observacion;
        private string estado;
        private Cliente cliente;

        public int Numero { get => numero; set => numero = value; }
        public string Observacion { get => observacion; set => observacion = value; }
        public string Estado { get => estado; set => estado = value; }
        internal Cliente Cliente { get => cliente; set => cliente = value; }

        public Pedidos(int numero, string observacion, string estado, int id, string nombre, string direccion, string telefono)
        {
            this.numero = numero;
            this.observacion = observacion;
            this.estado = estado;
            this.cliente = new Cliente(id, nombre, direccion, telefono);
        }

    }
}