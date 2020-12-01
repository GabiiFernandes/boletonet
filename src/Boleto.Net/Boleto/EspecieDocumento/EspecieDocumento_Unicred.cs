using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoletoNet
{
    public enum EnumEspecieDocumento_Unicred
    {
        DuplicataMercantil,
        NotaPromissoria,
        DuplicataDeServicos
    }
    class EspecieDocumento_Unicred : AbstractEspecieDocumento, IEspecieDocumento
    {
        public EspecieDocumento_Unicred()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public EspecieDocumento_Unicred(string codigo)
        {
            try
            {
                this.Carregar(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public static EnumEspecieDocumento_Unicred GetEnumEspecieByCodigo(string codigo)
        {
            switch (codigo)
            {
                case "01": return EnumEspecieDocumento_Unicred.DuplicataMercantil;
                case "02": return EnumEspecieDocumento_Unicred.NotaPromissoria;
                case "09": return EnumEspecieDocumento_Unicred.DuplicataDeServicos;
                default: return EnumEspecieDocumento_Unicred.DuplicataMercantil;
            }
        }

        public string GetCodigoEspecieByEnum(EnumEspecieDocumento_Unicred especie)
        {
            switch (especie)
            {
                case EnumEspecieDocumento_Unicred.DuplicataMercantil: return "01";
                case EnumEspecieDocumento_Unicred.NotaPromissoria: return "02";
                case EnumEspecieDocumento_Unicred.DuplicataDeServicos: return "09";
                default: return "01";
            }
        }

        private void Carregar(string idCodigo)
        {
            try
            {
                this.Banco = new Banco_Sicredi();

                switch (GetEnumEspecieByCodigo(idCodigo))
                {
                    case EnumEspecieDocumento_Unicred.DuplicataMercantil:
                        this.Codigo = GetCodigoEspecieByEnum(EnumEspecieDocumento_Unicred.DuplicataMercantil);
                        this.Especie = "Duplicata Mercantil";
                        this.Sigla = "DM";
                        break;
                    case EnumEspecieDocumento_Unicred.NotaPromissoria:
                        this.Codigo = GetCodigoEspecieByEnum(EnumEspecieDocumento_Unicred.NotaPromissoria);
                        this.Especie = "Nota Promissória";
                        this.Sigla = "NP";
                        break;
                    case EnumEspecieDocumento_Unicred.DuplicataDeServicos:
                        this.Codigo = GetCodigoEspecieByEnum(EnumEspecieDocumento_Unicred.DuplicataDeServicos);
                        this.Especie = "Duplicata de Serviços";
                        this.Sigla = "DS";
                        break;
                    default:
                        this.Codigo = "0";
                        this.Especie = "( Selecione )";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public static EspeciesDocumento CarregaTodas()
        {
            EspeciesDocumento especiesDocumento = new EspeciesDocumento();
            EspecieDocumento_Unicred ed = new EspecieDocumento_Unicred();

            foreach (EnumEspecieDocumento_Unicred item in Enum.GetValues(typeof(EnumEspecieDocumento_Unicred)))
                especiesDocumento.Add(new EspecieDocumento_Unicred(ed.GetCodigoEspecieByEnum(item)));

            return especiesDocumento;
        }
        public override IEspecieDocumento DuplicataMercantil()
        {
            throw new NotImplementedException();
        }
    }
}
