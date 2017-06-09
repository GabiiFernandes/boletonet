using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoletoNet {
    public class EspecieDocumento_Cecred : AbstractEspecieDocumento, IEspecieDocumento {

        #region Enum
        public enum EnumEspecieDocumento_Cecred {
            DuplicataMercantil = 1,
            NotaFiscal = 2,
            Recibo = 5,
            Cheque = 10,
            DuplicataServico = 12
        }
        #endregion

        public EspecieDocumento_Cecred() {
        }

        public EspecieDocumento_Cecred(string codigo) {
            try {
                this.carregar(codigo);
            } catch (Exception ex) {
                
                throw new Exception("Erro ao carregar objecto", ex);
            }
        }

        public static string getCodigoEspecieByEnum(EnumEspecieDocumento_Cecred especie) {
            return ((int)especie).ToString();
        }

        public static EnumEspecieDocumento_Cecred getEnumEspecieByCodigo(string codigo) {
            switch (codigo) {
                case "1": return EnumEspecieDocumento_Cecred.DuplicataMercantil;
                case "2": return EnumEspecieDocumento_Cecred.NotaFiscal;
                case "5": return EnumEspecieDocumento_Cecred.Recibo;
                case "10": return EnumEspecieDocumento_Cecred.Cheque;
                case "12": return EnumEspecieDocumento_Cecred.DuplicataServico;
                default: return EnumEspecieDocumento_Cecred.DuplicataMercantil;
            }
        }

        private void carregar(string idCodigo) {
            try {
                this.Banco = new Banco_Cecred();

                switch (getEnumEspecieByCodigo(idCodigo))
                {
                    case EnumEspecieDocumento_Cecred.DuplicataMercantil: default:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Cecred.DuplicataMercantil);
                        this.Especie = "Duplicata Mercantil";
                        this.Sigla = "DM";
                        break;
                    case EnumEspecieDocumento_Cecred.Recibo:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Cecred.Recibo);
                        this.Especie = "Recibo";
                        this.Sigla = "RC";
                        break;
                    case EnumEspecieDocumento_Cecred.Cheque:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Cecred.Cheque);
                        this.Especie = "Cheque";
                        this.Sigla = "C";
                        break;
                    case EnumEspecieDocumento_Cecred.DuplicataServico:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Cecred.DuplicataServico);
                        this.Especie = "Duplicata de Serviço";
                        this.Sigla = "DS";
                        break;
                    case EnumEspecieDocumento_Cecred.NotaFiscal:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Cecred.NotaFiscal);
                        this.Especie = "Nota Fiscal";
                        this.Sigla = "NF";
                        break;
                }
            } catch (Exception ex) {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public static EspeciesDocumento CarregaTodas() {
            EspeciesDocumento especiesDocumento = new EspeciesDocumento();

            foreach (EnumEspecieDocumento_Cecred item in Enum.GetValues(typeof(EnumEspecieDocumento_Cecred)))
                especiesDocumento.Add(new EspecieDocumento_Cecred(getCodigoEspecieByEnum(item)));

            return especiesDocumento;
        }

        public override IEspecieDocumento DuplicataMercantil() {
            return new EspecieDocumento_Cecred(getCodigoEspecieByEnum(EnumEspecieDocumento_Cecred.DuplicataMercantil));
        }
    }
}
