using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoletoNet
{
    public enum EnumEspecieDocumento_Safra
    {
        DuplicataMercantil,
        NotaPromissoria,
        DuplicataDeServicos
    }

    class EspecieDocumento_Safra : AbstractEspecieDocumento, IEspecieDocumento
    {
        public EspecieDocumento_Safra()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public EspecieDocumento_Safra(string codigo)
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

        public static EnumEspecieDocumento_Safra GetEnumEspecieByCodigo(string codigo)
        {
            switch (codigo)
            {
                case "01": return EnumEspecieDocumento_Safra.DuplicataMercantil;
                case "02": return EnumEspecieDocumento_Safra.NotaPromissoria;
                case "09": return EnumEspecieDocumento_Safra.DuplicataDeServicos;
                default: return EnumEspecieDocumento_Safra.DuplicataMercantil;
            }
        }

        public string GetCodigoEspecieByEnum(EnumEspecieDocumento_Safra especie)
        {
            switch (especie)
            {
                case EnumEspecieDocumento_Safra.DuplicataMercantil: return "01";
                case EnumEspecieDocumento_Safra.NotaPromissoria: return "02";
                case EnumEspecieDocumento_Safra.DuplicataDeServicos: return "09";
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
                    case EnumEspecieDocumento_Safra.DuplicataMercantil:
                        this.Codigo = GetCodigoEspecieByEnum(EnumEspecieDocumento_Safra.DuplicataMercantil);
                        this.Especie = "Duplicata Mercantil";
                        this.Sigla = "DM";
                        break;
                    case EnumEspecieDocumento_Safra.NotaPromissoria:
                        this.Codigo = GetCodigoEspecieByEnum(EnumEspecieDocumento_Safra.NotaPromissoria);
                        this.Especie = "Nota Promissória";
                        this.Sigla = "NP";
                        break;
                    case EnumEspecieDocumento_Safra.DuplicataDeServicos:
                        this.Codigo = GetCodigoEspecieByEnum(EnumEspecieDocumento_Safra.DuplicataDeServicos);
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
            EspecieDocumento_Safra ed = new EspecieDocumento_Safra();

            foreach (EnumEspecieDocumento_Safra item in Enum.GetValues(typeof(EnumEspecieDocumento_Safra)))
                especiesDocumento.Add(new EspecieDocumento_Safra(ed.GetCodigoEspecieByEnum(item)));

            return especiesDocumento;
        }

        public override IEspecieDocumento DuplicataMercantil()
        {
            return new EspecieDocumento_Safra(GetCodigoEspecieByEnum(EnumEspecieDocumento_Safra.DuplicataMercantil));
        }
    }
}
