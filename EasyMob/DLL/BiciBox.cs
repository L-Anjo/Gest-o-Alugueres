using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BO;

namespace DL
{
    [Serializable]
    public class BiciBox
    {
        const int maxv = 100;
        static List<Veiculo> variosv;
        static int totalv = 0;

        #region Construtores

        /// <summary>
        /// Construtor da Bicibox static
        /// </summary>
        static BiciBox()
        {
            variosv = new List<Veiculo>() ;
        }
        #endregion

        #region Propriedades
        public static int Totalv
        {
            get { return totalv; }
            set { totalv = value; }
        }
        public static int Maxv
        {
            get { return maxv; }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Show- Serve para Mostrar todos os aluguers referentas á bicibox
        /// </summary>
        /// <returns></returns>
        public static string ShowV()
        {
            string s = "";
            int i = 0;
            foreach (Object v in variosv)
            {
                s += String.Format(v.ToString()+"\n");
            }
            return s;
        }


        /// <summary>
        /// Adiciona Veiculo á Bicibox
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static bool AdicionarVeiBic(Veiculo v)
        {
            variosv.Add(v);
            totalv++;
            return true;
        }

   

        /// <summary>
        /// Existe Veiculo na Bicibox
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool ExisteVeiBic(int id)
        {
            foreach (Veiculo v in variosv)
            {
                if (v.Cod == id)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Copia Lista Veiculos para uma nova
        /// </summary>
        /// <returns></returns>
        public static List<Veiculo> Veiculos()
        {
            List<Veiculo> aux = new List<Veiculo>();
            aux = new List<Veiculo>(variosv);
           return aux ;
        }


        /// <summary>
        /// Guarda a estrutura num ficheiro Binário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool SaveAll(string fileName)
        {
            try
            {
                Stream stream = File.Open(fileName, FileMode.OpenOrCreate);
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, variosv);
                stream.Close();
                return true;
            }
            catch (IOException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Carrega o Ficheiro Binário para uma estrutura
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool LoadAll(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    Stream stream = File.Open(fileName, FileMode.Open);
                    BinaryFormatter bin = new BinaryFormatter();
                    variosv = (List<Veiculo>)bin.Deserialize(stream);
                    stream.Close();
                    return true;
                }
                catch (IOException e)
                {
                    throw e;
                }
                catch (Exception e)
                {

                }
            }
            return false;
        }

        #endregion
    }
}
