using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace BO
{
    public enum Estado
    {
        Disponivel = 1,
        Avariado = 2,
        Manutencao = 3,
        Ocupado = 4,
    }
    public enum Tipo
    {
        Bicicletael = 1,
        Trotineteel = 2,
    }
    public enum Funcao
    {
        Utilizador = 1,
        Funcionario = 2,
        Mecanico = 3,
    }

    [Serializable]
    public class Veiculo : IComparable
    {
        int cod;
        Estado estado;

        #region Construtores
        /// <summary>
        /// Construtor do Veiculo
        /// </summary>
        public Veiculo(int cod)
        {
            this.cod = cod;
            this.estado = Estado.Disponivel;
        }

        #endregion

        #region Propriedades
        public int Cod
        {
            get { return cod; }
            set { if (value > 0) cod = value; }
        }
        public Estado Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Alterar Estado do veiculo se for funcionario ou mecanico
        /// </summary>
        /// <param name="u"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool AlterarEstadoV(Utilizador u, Estado e)
        {
            if (u.Funcao != Funcao.Utilizador)
            {
                Estado = e;
                return true;
            }
            return false;
        }

 

        /// <summary>
        /// Atribui valor 
        /// </summary>
        /// <returns>custo</returns>
        public virtual double Custo()
        {
            double custo = 0.15;
            return custo;
        }


        public override string ToString()
        {
            string outStr = String.Format("Cod: {0} | Estado: {1} | TipoV: Bicicleta", cod, estado);
            return outStr;
        }

        /// <summary>
        /// Função Usada no Sort para ordenar uma Lista
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int CompareTo(Object obj)
        {
            if (obj is Veiculo || obj is VeiculoE)
            {
                Veiculo aux = obj as Veiculo;
                if (this.cod == aux.cod) return 0;
                if (this.cod > aux.cod) return 1;
                return -1;
            }
            return -1;
        }



        #endregion

    }


    [Serializable]
    public class VeiculoE : Veiculo
    {
        Tipo tipo;
        int autonomia;

        #region Construtores
        /// <summary>
        /// Construtor do Veiculo Eletrico
        /// </summary>
        public VeiculoE(Tipo tipo, int autonomia, int cod) : base(cod)
        {
            this.tipo = tipo;
            this.autonomia = autonomia;
        }
        #endregion

        #region Propriedades
        public Tipo Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public int Autonomia
        {
            get { return autonomia; }
            set { autonomia = value; }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Atribui valor constante conforme o tipo de Veiculo
        /// </summary>
        /// <returns>custo</returns>
        public override double Custo()
        {
            double custo;
            if (Tipo == Tipo.Bicicletael)
            {
                custo = 0.6;
                return (custo);
            }
            else
            {
                custo = 0.3; return custo;
            }

        }

        public override string ToString()
        {
            string outStr = String.Format("Cod: {0} | Estado: {2} | TipoV: {1} | Autonomia: {3}", Cod, Tipo, Estado, autonomia);
            return outStr;
        }
        #endregion
    }


    public class Utilizador
    {
        int cod;
        string nome;
        double saldo;
        Funcao funcao;

        #region Construtor


        /// <summary>
        /// Construtor do Utilizador
        /// </summary>
        public Utilizador(int cod, string nome, Funcao funcao)
        {
            this.cod = cod;
            this.nome = nome;
            this.funcao = funcao;
            this.saldo = 0;
        }
        #endregion

        #region Propriedades
        public int Cod
        {
            get { return cod; }
            set { cod = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public Funcao Funcao
        {
            get { return funcao; }
            set { funcao = value; }
        }
        public double Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Adiciona Saldo ao Utilizador
        /// </summary>
        /// <param name="val"> Valor a Adicionar ao Saldo</param>
        /// <returns></returns>
        public bool AdicionaSaldo(double val)
        {
            Saldo = saldo + val;
            return true;
        }

        /// <summary>
        /// Devolve Saldo ao Utilizador do Aluguer
        /// </summary>
        /// <param name="val"> Valor a Devolver ao Saldo</param>
        /// <returns></returns>
        public bool DevolveSaldo(double val)
        {
            Saldo = saldo - val;
            return true;
        }

        #endregion

    }



    [Serializable]
    public class Aluguer
    {
        Utilizador u;
        Veiculo v;
        DateTime d;
        int horas;
        bool ativo;
        [NonSerialized]
        double preco;

        #region Construtor
        /// <summary>
        /// Construtor do aluguer
        /// </summary>
        public Aluguer(Utilizador u, Veiculo v, int horas)
        {
            this.u = u;
            this.v = v;
            this.horas = horas;
            this.ativo = true;
            this.d = DateTime.Now;
          
        }
        #endregion

        #region Propriedades
        public Veiculo V
        {
            get { return v; }
        }
        public Utilizador U
        {
            get { return u; }
        }
        public DateTime D
        {
            get { return d; }
        }
        public int Horas
        {
            get { return horas; }
            set { if (value > 0) horas = value; }
        }
        public double Preco
        {
            get { return preco; }
            set { preco = value; }
        }

        public bool Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }
        #endregion

        #region Metodos 

        #endregion


    }
}

